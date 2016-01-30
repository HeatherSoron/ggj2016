using UnityEngine;
using System.Collections;
using System;

public class RitualDeathController : MonoBehaviour
{

    public event Action EventFinished = delegate { };

    public void Animate(GameObject playerObject)
    {
        m_playerObject = playerObject;
        m_timer        = 0.0f; 
        m_enabled      = true;

        Instantiate(m_ritualParticle, m_playerObject.transform.position, Quaternion.identity);

        var position = m_playerObject.transform.position;
        position.y = 0.5f;

        Instantiate(m_bloodParticle,  position + new Vector3( 0,    0,     0), Quaternion.identity);
        Instantiate(m_bloodParticle,  position + new Vector3( 0,    0,  0.5f), Quaternion.identity);
        Instantiate(m_bloodParticle,  position + new Vector3( 0,    0, -0.5f), Quaternion.identity);
        Instantiate(m_bloodParticle,  position + new Vector3( 0.5f, 0,     0), Quaternion.identity);
        Instantiate(m_bloodParticle,  position + new Vector3(-0.5f, 0,     0), Quaternion.identity);
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Animate(GameObject.Find("Player2"));
        }

        //m_timer += Time.deltaTime;

        if (m_enabled && m_timer > 4.0f)
        {
            OnTimerElasped();
        }
    }

    void OnTimerElasped()
    {
        EventFinished();

        Instantiate(m_bloodParticle, m_playerObject.transform.position, Quaternion.identity);

        m_enabled = false;
    }

    [SerializeField]
    GameObject m_bloodParticle;

    [SerializeField]
    GameObject m_ritualParticle;

    GameObject m_playerObject;

    float m_timer;

    bool m_enabled;

}
