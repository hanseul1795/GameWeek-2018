using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float m_distance = 0;
    [SerializeField] private float m_speed = 0;
    [SerializeField] private bool m_up = false;
    [SerializeField] private bool m_down = false;
    [SerializeField] private bool m_left = false;
    [SerializeField] private bool m_right = false;

    private Vector3 m_startPoint;
    private Vector3 m_endPoint;
    private Vector3 m_destination;
    private float step;

    private void Start()
    {
        m_startPoint = transform.position;
        if(m_up && !m_down && !m_left && !m_right)
        {
            m_endPoint = m_startPoint + new Vector3(0, 1, 0) * m_distance; 
        }
        else if (!m_up && m_down && !m_left && !m_right)
        {
            m_endPoint = m_startPoint + new Vector3(0, -1, 0) * m_distance;
        }
        else if (!m_up && !m_down && m_left && !m_right)
        {
            m_endPoint = m_startPoint + new Vector3(-1, 0, 0) * m_distance;
        }
        else if (!m_up && !m_down && !m_left && m_right)
        {
            m_endPoint = m_startPoint + new Vector3(1, 0, 0) * m_distance;
        }
        else
        {
            m_endPoint = m_startPoint;
        }
        m_destination = m_endPoint;
	}
	
	private void Update()
    {
        Move();
	}

    private void Move()
    {
        step = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_destination, step);

        if(Vector3.Distance(transform.position, m_startPoint) <= 0.1f)
        {
            m_destination = m_endPoint;
        }
        else if(Vector3.Distance(transform.position, m_endPoint) <= 0.1f)
        {
            m_destination = m_startPoint;
        }
    }
}