using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonScript : MonoBehaviour {

	public GameObject Icon1;
	public GameObject Icon2;
	public GameObject Icon3;

	public enum EGesture {
		Wide,
		Middle,
		Forward,
		Down
	};

	public static EGesture[] gestures = new EGesture[3];

	public static float power = 0.5f;
	public float powerFade = 0.02f;


	public GameObject powerBar;

	public float changeDelay = 4f;
	private float lastChange = 0;

	// Use this for initialization
	void Start () {
		lastChange = Time.time;
		gestures [0] = EGesture.Wide;
		gestures [1] = EGesture.Forward;
		gestures [2] = EGesture.Middle;

		ShowGesture (Icon1, gestures [0]);
		ShowGesture (Icon2, gestures [1]);
		ShowGesture (Icon3, gestures [2]);
	}
	
	// Update is called once per frame
	void Update () {
		if (lastChange + changeDelay < Time.time) {
			for (int i = 0; i < gestures.Length; ++i) {
				gestures [i] = RandomGesture ();
			}
			ShowGesture (Icon1, gestures [0]);
			ShowGesture (Icon2, gestures [1]);
			ShowGesture (Icon3, gestures [2]);
			lastChange = Time.time;
		}

		power -= powerFade * Time.deltaTime;

		powerBar.GetComponent<Slider> ().value = power;
	}

	void ShowGesture(GameObject icon, EGesture gesture) {
		icon.GetComponent<Image>().color = GetGestureColor(gesture);
		icon.GetComponentInChildren<Text> ().text = GetGestureText (gesture);
	}

	Color32 GetGestureColor(EGesture gesture) {
		switch (gesture) {
		case EGesture.Forward:
			return new Color32 (0, 255, 33, 255);
			break;
		case EGesture.Wide:
			return new Color32 (255, 33, 0, 255);
			break;
		case EGesture.Middle:
			return new Color32 (0, 0, 255, 255);
			break;
		case EGesture.Down:
			return new Color32 (200, 160, 0, 255);
			break;
		default:
			return new Color32 (0, 0, 0, 255);
			break;
		}
	}

	string GetGestureText(EGesture gesture) {
		switch (gesture) {
		case EGesture.Forward:
			return "A";
			break;
		case EGesture.Wide:
			return "B";
			break;
		case EGesture.Middle:
			return "X";
			break;
		case EGesture.Down:
			return "Y";
			break;
		default:
			return "?";
			break;
		}
	}

	EGesture RandomGesture() {
		return (EGesture)Random.Range (0, System.Enum.GetValues (typeof(EGesture)).Length);
	}
}
