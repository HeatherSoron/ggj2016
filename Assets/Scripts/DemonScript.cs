using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonScript : MonoBehaviour {

	public GameObject[] challengeIcons;
	public static EGesture[] challengeGestures;


	public GameObject currentGestureIcon;
	public GameObject nextGestureIcon;

	public EGesture currentGesture;
	private EGesture nextGesture;

	public GameObject challengePanel;
	public GameObject normalPanel;

	public static bool deathChallenge;

	public GameObject[] players;
	private int livingPlayers;

	public enum EGesture {
		Wide,
		Middle,
		Forward,
		Down
	};


	public static int patternRound = 0;

	public GameObject powerBar;

	public float changeDelay = 4f;
	private float lastChange = 0;

	// Use this for initialization
	void Start () {
		deathChallenge = false;
		lastChange = Time.time;

		currentGesture = RandomGesture ();
		nextGesture = RandomGesture (currentGesture);
		ShowGesture (currentGestureIcon, currentGesture);

		challengeGestures = new EGesture[challengeIcons.Length];

		livingPlayers = 0;
		foreach (GameObject p in players) {
			if (p.activeInHierarchy) {
				livingPlayers += 1;
			}
		}

		challengePanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (deathChallenge) {
			HandleDeathMode ();
		} else {
			HandleNormalMode ();
		}
	}

	void HandleDeathMode() {
		int successfulPlayers = 0;
		foreach (GameObject p in players) {
			if (p.activeInHierarchy) {
				if (p.GetComponent<GestureScript> ().FinishedChallenge ()) {
					successfulPlayers += 1;
				}
			}
		}

		if (successfulPlayers == livingPlayers - 1) {
			foreach (GameObject p in players) {
				if (p.activeInHierarchy) {
					if (! (p.GetComponent<GestureScript> ().FinishedChallenge ())) {
						p.SetActive (false);
						livingPlayers -= 1;
					}
				}
			}
			deathChallenge = false;
			challengePanel.SetActive (false);
			normalPanel.SetActive (true);
			lastChange = Time.time;
		}
	}

	void HandleNormalMode() {
		if (lastChange + changeDelay < Time.time) {
			patternRound++;
			lastChange = Time.time;

			foreach (GameObject p in players) {
				if (p.activeInHierarchy) {
					GestureScript playerScript = p.GetComponent<GestureScript> ();
					if (playerScript.GetGesture () != currentGesture) {
						deathChallenge = true;
					}
				}
			}

			if (deathChallenge) {
				challengePanel.SetActive (true);
				normalPanel.SetActive (false);
				foreach (GameObject p in players) {
					p.GetComponent<GestureScript> ().StartDeathChallenge ();
				}
				for (int i = 0; i < challengeGestures.Length; ++i) {
					EGesture lastGesture = i == 0 ? currentGesture : challengeGestures [i - 1];
					challengeGestures [i] = RandomGesture (lastGesture);
					ShowGesture (challengeIcons [i], challengeGestures [i]);
				}
			}

			currentGesture = nextGesture;
			nextGesture = RandomGesture (currentGesture);
			ShowGesture (currentGestureIcon, currentGesture);
		}

		// update this every frame so that we can fade it in (with alpha)
		ShowGesture (nextGestureIcon, nextGesture, (byte)(255 * (Time.time - lastChange) / changeDelay));
	}

	void ShowGesture(GameObject icon, EGesture gesture, byte alpha = 255) {
		icon.GetComponent<Image>().color = GetGestureColor(gesture, alpha);
		icon.GetComponentInChildren<Text> ().text = GetGestureText (gesture);
		icon.GetComponentInChildren<Text> ().color = new Color32 (0, 0, 0, alpha);
	}

	Color32 GetGestureColor(EGesture gesture, byte alpha = 255) {
		switch (gesture) {
		case EGesture.Forward:
			return new Color32 (0, 255, 33, alpha);
		case EGesture.Wide:
			return new Color32 (255, 33, 0, alpha);
		case EGesture.Middle:
			return new Color32 (0, 0, 255, alpha);
		case EGesture.Down:
			return new Color32 (200, 160, 0, alpha);
		default:
			return new Color32 (0, 0, 0, alpha);
		}
	}

	string GetGestureText(EGesture gesture) {
		switch (gesture) {
		case EGesture.Forward:
			return "A";
		case EGesture.Wide:
			return "B";
		case EGesture.Middle:
			return "X";
		case EGesture.Down:
			return "Y";
		default:
			return "?";
		}
	}

	EGesture RandomGesture(EGesture lastGesture = EGesture.Down) {
		EGesture gesture;
		do {
			gesture = (EGesture)Random.Range (0, System.Enum.GetValues (typeof(EGesture)).Length);
		} while (gesture == lastGesture);
		return gesture;
	}
}
