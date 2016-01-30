using UnityEngine;
using System.Collections;
using System;

public class WeaponDeathController : MonoBehaviour
{

    public event Action EventDroppedListener = delegate { };

    public GameObject WeaponObject
    {
        get; set;
    }

    public void Drop(GameObject playerObject)
    {
        m_playerObject = playerObject;

        int random = UnityEngine.Random.Range(0, m_weapons.Length);

        foreach (var weapon in m_weapons)
        {
            weapon.SetActive(false);
        }

        WeaponObject = m_weapons[random];
        WeaponObject.SetActive(true);

        transform.rotation = Quaternion.identity;

        m_targetRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x,  transform.rotation.eulerAngles.y, 75);

        m_running = true;
    }

    void Start()
    {
        // Empty
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Drop(GameObject.Find("Player1"));
        }

        if (!m_running)
            return ;

        UpdateRotation();
    }

    void UpdateRotation()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, m_targetRotation, m_velocity * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, m_targetRotation) < 1.0f)
        {
            var position = m_playerObject.transform.position;
            position.y = 0.5f;

            Instantiate(m_impactParticle, position,                                Quaternion.identity);
            Instantiate(m_bloodParticle,  position + new Vector3( 0,    0,     0), Quaternion.identity);
            Instantiate(m_bloodParticle,  position + new Vector3( 0,    0,  0.5f), Quaternion.identity);
            Instantiate(m_bloodParticle,  position + new Vector3( 0,    0, -0.5f), Quaternion.identity);
            Instantiate(m_bloodParticle,  position + new Vector3( 0.5f, 0,     0), Quaternion.identity);
            Instantiate(m_bloodParticle,  position + new Vector3(-0.5f, 0,     0), Quaternion.identity);

            m_running = false;
            m_hitSound.Play();

            EventDroppedListener();
        }
    }

    [SerializeField]
    AudioSource m_hitSound;

    [SerializeField]
    GameObject m_impactParticle;

    [SerializeField]
    GameObject m_bloodParticle;

    [SerializeField]
    GameObject[] m_weapons;

    [SerializeField]
    float m_velocity = 360.0f;

    GameObject m_playerObject;
    Quaternion m_targetRotation = Quaternion.identity;
    bool       m_running;

}
