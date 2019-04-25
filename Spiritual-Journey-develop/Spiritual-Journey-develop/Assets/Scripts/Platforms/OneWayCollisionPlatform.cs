using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlatformEffector2D))]
public class OneWayCollisionPlatform : MonoBehaviour
{
    private Collider2D m_toIgnore;
    private Collider2D m_collider;
    private float m_ignoreDuration = 0.5f;

    private float m_ignoreTimer;

    private void Awake()
    {
        m_collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        ResetIgnoreTimer();
    }

    private void ResetIgnoreTimer()
    {
        m_ignoreTimer = 0.0f;
    }

    public void Ignore(Collider2D p_toIgnore)
    {
        m_toIgnore = p_toIgnore;
        Physics2D.IgnoreCollision(m_collider, p_toIgnore, true);
        ResetIgnoreTimer();
    }

    private void StopToIgnore()
    {
        Physics2D.IgnoreCollision(m_collider, m_toIgnore, false);
        m_toIgnore = null;
    }

    private void Update()
    {
        if (m_toIgnore)
        {
            m_ignoreTimer += Time.deltaTime;
            if (m_ignoreTimer >= m_ignoreDuration)
            {
                StopToIgnore();
            }
        }
    }
}
