using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOverTime : MonoBehaviour
{
    [SerializeField] private Vector3 m_direction;
    [SerializeField] private float m_speed;

    private bool m_locked = false;

    private void Update()
    {
        if (!m_locked)
            transform.position += m_direction * m_speed * Time.deltaTime;
    }

    public void Lock(bool p_state)
    {
        m_locked = p_state;
    }
}
