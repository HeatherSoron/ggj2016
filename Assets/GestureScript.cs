using UnityEngine;
using System.Collections;

public class GestureScript : MonoBehaviour {
	public int Player = 1;

	public float minTimeDiff = 0.2f;
	private float lastGestureTime = 0;
	private int patternRound = 0;

	private int currentGesture = 0;

	private DemonScript.EGesture activeGesture = DemonScript.EGesture.Down;

	public GameObject pip;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (patternRound != DemonScript.patternRound) {
			currentGesture = 0;
			patternRound = DemonScript.patternRound;
		}

		foreach (DemonScript.EGesture ges in System.Enum.GetValues(typeof(DemonScript.EGesture))) {
			if (GestureStart ("" + ges)) {
				activeGesture = ges;
			}
		}

		if (GestureStart("" + DemonScript.gestures [currentGesture])) {
			currentGesture++;
		}
		if (currentGesture >= DemonScript.gestures.Length) {
			DemonScript.power += 0.1f;
			currentGesture = 0;
		}
		pip.transform.localPosition = new Vector3((currentGesture - 1) * 100, 0, 0);
	}

	public bool GestureStart (string gesture) {
		bool hasGesture = GestureAllowed() && Input.GetButtonDown ("P" + Player + "_" + gesture);
		if (hasGesture) {
			lastGestureTime = Time.time;
		}
		return hasGesture;
	}

	bool GestureAllowed() {
		// either the last gesture must have been a while ago, OR it must have been this tick
		return lastGestureTime + minTimeDiff < Time.time || lastGestureTime == Time.time;
	}

	public DemonScript.EGesture GetGesture() {
		return activeGesture;
	}
}
