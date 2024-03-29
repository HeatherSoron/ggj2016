﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GestureScript : MonoBehaviour {
	public int Player = 1;

	public float minTimeDiff = 0.2f;
	private float lastGestureTime = 0;
	private int patternRound = 0;
    public GameObject successPrefab;

	private int challengeIndex;

	private DemonScript.EGesture activeGesture = DemonScript.EGesture.Down;

	public GameObject pip;

	// Use this for initialization
	void Start () {
		print (Input.GetAxisRaw ("JoystickTest1"));
		if (false) {
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {

		foreach (DemonScript.EGesture ges in System.Enum.GetValues(typeof(DemonScript.EGesture))) {
			if (GestureStart ("" + ges)) {
				activeGesture = ges;
			}
		}

		if (DemonScript.deathChallenge) {
			if (!FinishedChallenge() && activeGesture == DemonScript.challengeGestures [challengeIndex + 1]) {
				// yay, we've matched the next gesture! Huzzah!
				challengeIndex += 1;

                if (FinishedChallenge()) {
                    CreateSuccessPrefab();
                }
			} else if (challengeIndex >= 0 && activeGesture != DemonScript.challengeGestures [challengeIndex]) {
				// whoops, we fumbled the sequence. Reset our progress on it :(.
				challengeIndex = -1;
			}
		}
		else if (activeGesture == DemonScript.currentGesture && GameObject.Find("Demon") != null)
        {
            CreateSuccessPrefab();
        }
	}

	public void StartDeathChallenge() {
		challengeIndex = -1;
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

	public bool FinishedChallenge() {
		return challengeIndex + 1 >= DemonScript.challengeGestures.Length;
	}

    public void CreateSuccessPrefab() {
        Instantiate(successPrefab, transform.position, Quaternion.identity);
    }
}
