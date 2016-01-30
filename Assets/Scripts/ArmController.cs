using UnityEngine;
using System.Collections;
using System;

public class ArmController : MonoBehaviour
{
    public enum ESide
    {
        Left,
        Right
    }

    public enum EDirection
    {
        Down,
        Forward,
        HalfOutward,
        Outward
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
            Direction = EDirection.Forward;
        }
		else if (GetComponentInParent<GestureScript>().GestureStart("Middle"))
        {
            Direction = EDirection.HalfOutward;
        }
		else if (GetComponentInParent<GestureScript>().GestureStart("Wide"))
        {
            Direction = EDirection.Outward;
        }
		else if (GetComponentInParent<GestureScript>().GestureStart("Down"))
        {
            Direction = EDirection.Down;
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
            case EDirection.Down:
                m_targetRotation = Quaternion.identity;
                break;

            case EDirection.Forward:
                m_targetRotation.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
                break;

            case EDirection.HalfOutward:
                m_targetRotation.eulerAngles = m_side == ESide.Left ? new Vector3(270.0f, 45.0f, 00.0f) : new Vector3(90.0f, 315.0f, 00.0f);
                break;

            case EDirection.Outward:
                m_targetRotation.eulerAngles = m_side == ESide.Left ? new Vector3(270.0f, 0.0f, 00.0f) : new Vector3(90.0f, 0.0f, 00.0f);
                break;
        }
    }

    [SerializeField]
    ESide m_side = ESide.Left;

    [SerializeField]
    float m_velocity = 360.0f;

    EDirection m_direction = EDirection.Down;

    Quaternion m_targetRotation = Quaternion.identity;

}
