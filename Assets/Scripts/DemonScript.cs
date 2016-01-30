using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemonScript : MonoBehaviour {

	public GameObject Icon1;
	public GameObject Icon2;
	public GameObject Icon3;

	public int failCount = 0;
	public int winCount = 0;
	public double power = 0;

	public int button = 1;
	public float lastAttack;

	// Use this for initialization
	void Start () {
		lastAttack = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire" + button)) {
			power += 1;
			button %= 3;
			button++;
		}


		if (Time.time > lastAttack + 5) {
			if (power < 10) {
				failCount += 1;
			} else {
				winCount += 1;
			}

			power = 0;
			lastAttack = Time.time;
		}
	}

	void ShowGesture(GameObject icon, Color32 color) {
		icon.GetComponent<Image>().color = color;
	}
}
