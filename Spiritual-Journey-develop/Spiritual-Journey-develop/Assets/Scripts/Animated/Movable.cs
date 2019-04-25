using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movable : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveUp(float p_speed)
    {
        m_rigidbody.velocity = Vector3.up * p_speed;
    }

    public void MoveRight(float p_speed)
    {
        m_rigidbody.velocity = Vector3.right * p_speed;
    }
}
