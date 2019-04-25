using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport m_destination = null;
    [SerializeField] private float m_cooldown = 2.0f;

    private Vector3 m_defaultScale;
    private float m_timer;

    private void Awake()
    {
        m_defaultScale = transform.localScale;
        m_timer = 0.0f;
    }

    private void Update()
    {
        if (m_timer > 0.0f)
        {
            m_timer -= Time.deltaTime;
        }

        float cooldownProgress = m_timer / m_cooldown;
        transform.localScale = Vector3.Lerp(Vector3.zero, m_defaultScale, 1 - cooldownProgress);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_timer <= 0.0f)
        { 
            m_destination.StartCooldown();
            StartCooldown();
            collision.transform.position = new Vector3(m_destination.transform.position.x, m_destination.transform.position.y, collision.transform.position.z);
        }
    }

    private void StartCooldown()
    {
        m_timer = m_cooldown;
    }
}
