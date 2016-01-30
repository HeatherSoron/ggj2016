using UnityEngine;
using System.Collections;

public class PlayerKillController : MonoBehaviour
{

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
        PlayerId = GetComponent<GestureScript>().Player;
    }

    void Update()
    {

    }

    [SerializeField]
    GameObject m_swastika;

    [SerializeField]
    GameObject m_weapon;

}
