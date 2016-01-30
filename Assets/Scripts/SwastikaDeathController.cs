using UnityEngine;
using System.Collections;

public class SwastikaDeathController : MonoBehaviour
{

    public void Fall(GameObject playerObject)
    {
        m_playerObject = playerObject;

        transform.position = m_playerObject.transform.position + new Vector3(0.0f, 10.0f, 0.0f);

        m_running = true;
    }

    void Start()
    {
        // Empty
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Fall(GameObject.Find("Player3"));
        }

        if (!m_running)
            return ;

        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_playerObject.transform.position, m_velocity * Time.deltaTime);

        if ((transform.position - m_playerObject.transform.position).magnitude < 0.5)
        {
            m_running = false;

            m_hitSound.Play();
            m_hitSound2.Play();
        }
    }

    [SerializeField]
    AudioSource m_hitSound;

    [SerializeField]
    AudioSource m_hitSound2;

    [SerializeField]
    float m_velocity = 10.0f;

    GameObject m_playerObject;

    bool m_running;


}
