using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerKillController : MonoBehaviour
{
	public Image playingIndicator;
	public bool playing;

    public int  PlayerId { get; private set; }
    public bool IsDead   { get; private set; }

    public void Kill()
    {
        int rand = Random.Range(0, 2);

        Debug.LogError(rand);

        if (rand == 0)
        {
            m_swastika.GetComponent<SwastikaDeathController>().Fall(gameObject);
        }
        else if (rand == 1)
        {
            m_weapon.GetComponent<WeaponDeathController>().Drop(gameObject);
        }
    }

    void Start()
    {
		playing = false;
        PlayerId = GetComponent<GestureScript>().Player;
    }

    void Update()
	{
		if (!DemonScript.started) {
			if (Input.GetButtonDown ("P" + PlayerId + "_Forward")) {
				playing = true;
				playingIndicator.color = new Color32 (0, 255, 33, 255);
			}
		}
    }

    [SerializeField]
    GameObject m_swastika;

    [SerializeField]
    GameObject m_weapon;

}
