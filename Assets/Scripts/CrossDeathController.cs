using UnityEngine;
using System.Collections;

public class CrossDeathController : MonoBehaviour
{

    public void Fall(GameObject playerObject)
    {
        m_playerObject = playerObject;

        transform.position = m_playerObject.transform.position + new Vector3(0.0f, 10.0f, 0.0f);

        m_running = true;
    }

    void Start()
    {
        Debug.LogError(Input.GetJoystickNames().Length);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fall(GameObject.Find("Demon"));
        }

        if (!m_running)
            return ;

        UpdatePosition();
    }

    void UpdatePosition()
    {
        var position = m_playerObject.transform.position;
        position.y = 0;

        transform.position = Vector3.MoveTowards(transform.position, position, m_velocity * Time.deltaTime);

        if ((transform.position - position).magnitude < 0.5)
        {
            m_running = false;

            Instantiate(m_hitParticles, m_playerObject.transform.position, m_playerObject.transform.rotation);

            m_hitSound.Play();
            m_hitSound2.Play();

            m_playerObject.SetActive(false);
        }
    }

    [SerializeField]
    AudioSource m_hitSound;

    [SerializeField]
    AudioSource m_hitSound2;

    [SerializeField]
    GameObject m_hitParticles;

    [SerializeField]
    float m_velocity = 10.0f;

    GameObject m_playerObject;

    bool m_running;

}
