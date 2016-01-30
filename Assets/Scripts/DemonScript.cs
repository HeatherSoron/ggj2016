using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonScript : MonoBehaviour {

	public GameObject ComboIcon1;
	public GameObject ComboIcon2;
	public GameObject ComboIcon3;

	public GameObject currentGestureIcon;
	public GameObject nextGestureIcon;

	public EGesture currentGesture;
	private EGesture nextGesture;

	public GameObject challengePanel;

	public bool deathChallenge;

	public GameObject[] players;
	public static int playerCount = 4;

	public enum EGesture {
		Wide,
		Middle,
		Forward,
		Down
	};

	public static EGesture[] gestures = new EGesture[3];

	public static int patternRound = 0;

	public static float power = 0.5f;
	public float powerFade = 0.02f;


	public GameObject powerBar;

	public float changeDelay = 4f;
	private float lastChange = 0;

	// Use this for initialization
	void Start () {
		deathChallenge = false;
		lastChange = Time.time;

		do {
			currentGesture = RandomGesture ();
		} while (currentGesture == EGesture.Down);
		do {
			nextGesture = RandomGesture ();
		} while (nextGesture == currentGesture);

		challengePanel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (lastChange + changeDelay < Time.time) {
			patternRound++;
			for (int i = 0; i < gestures.Length; ++i) {
				gestures [i] = RandomGesture ();
			}
			ShowGesture (ComboIcon1, gestures [0]);
			ShowGesture (ComboIcon2, gestures [1]);
			ShowGesture (ComboIcon3, gestures [2]);
			lastChange = Time.time;

			foreach (GameObject p in players) {
				GestureScript playerScript = p.GetComponent<GestureScript> ();
				if (playerScript.GetGesture () != currentGesture) {
					deathChallenge = true;
				}
			}

			if (deathChallenge) {
				challengePanel.SetActive (true);
			}

			currentGesture = nextGesture;
			while (currentGesture == nextGesture) {
				nextGesture = RandomGesture ();
			}
		}

		ShowGesture (currentGestureIcon, currentGesture);
		ShowGesture (nextGestureIcon, nextGesture, (byte)(255 * (Time.time - lastChange) / changeDelay));

		power -= powerFade * Time.deltaTime;

		powerBar.GetComponent<Slider> ().value = power;
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
			return "↑.↑";
		case EGesture.Wide:
			return "←.→";
		case EGesture.Middle:
			return "↖.↗";
		case EGesture.Down:
			return " o ";
		default:
			return "?";
		}
	}

	EGesture RandomGesture(EGesture lastGesture = EGesture.Down) {
		return (EGesture)Random.Range (0, System.Enum.GetValues (typeof(EGesture)).Length);
	}
}
