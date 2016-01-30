using UnityEngine;
using System.Collections;
using System;

public class HeadController : MonoBehaviour
{
    public enum EDirection
    {
        Forward,
        Left,
        Right,
        Up
    }

    public EDirection Direction
    {
        get { return m_direction; }
        set
        {
            m_direction = value;

            UpdateTargetRotation();
        }
    }

    void Start()
    {
        UpdateTargetRotation();
    }

    void Update()
    {
		if (GetComponentInParent<GestureScript>().GestureStart("Forward"))
        {
            Direction = EDirection.Up;
        }
		else if (GetComponentInParent<GestureScript>().GestureStart("Middle"))
        {
            Direction = EDirection.Left;
        }
		else if (GetComponentInParent<GestureScript>().GestureStart("Wide"))
        {
            Direction = EDirection.Right;
        }
		else if (GetComponentInParent<GestureScript>().GestureStart("Down"))
        {
            Direction = EDirection.Forward;
        }

        UpdateRotation();
    }

    void UpdateRotation()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation  , m_targetRotation, m_velocity * Time.deltaTime);
    }

    void UpdateTargetRotation()
    {
        switch (m_direction)
        {
            case EDirection.Forward:
                m_targetRotation = Quaternion.identity;
                break;

            case EDirection.Left:
                m_targetRotation.eulerAngles = new Vector3(0.0f, -30.0f, 0.0f);
                break;

            case EDirection.Right:
                m_targetRotation.eulerAngles = new Vector3(0.0f, 30.0f, 0.0f);
                break;

            case EDirection.Up:
                m_targetRotation.eulerAngles = new Vector3(0.0f, 0.0f, 30.0f);
                break;
        }
    }

    [SerializeField]
    float m_velocity = 360.0f;

    EDirection m_direction      = EDirection.Forward;
    Quaternion m_targetRotation = Quaternion.identity;

}
