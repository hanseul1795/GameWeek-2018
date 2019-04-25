using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlate : MonoBehaviour
{
    [SerializeField] private float m_speed = 0;
    [SerializeField] private float m_distance = 0;
    [SerializeField] private bool m_upFirst = false;

    private Vector3 m_startingPosition;
    private Vector3 m_endingPosition;
    private bool m_onCollision;

    private void Start()
    {
        m_startingPosition = transform.position;
        if (m_upFirst)
        {
            m_endingPosition = m_startingPosition + new Vector3(0, 1, 0) * m_distance;
        }
        else
        {
            m_endingPosition = m_startingPosition + new Vector3(0, -1, 0) * m_distance;
        }

        m_onCollision = false;
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null || (player.IsGrounded() && player.IsGroundedWithConsistency()))
            m_onCollision = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_onCollision = false;
    }

    private void Move()
    {   
        if(m_onCollision)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_endingPosition, Time.deltaTime * m_speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, m_startingPosition, Time.deltaTime * m_speed);
        }
    }
}
