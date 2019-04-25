using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] private float m_delayBeforeDestroyInSeconds = 0;

    private float m_timer;

    private void Awake()
    {
        m_timer = m_delayBeforeDestroyInSeconds;
    }

    private void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer <= 0)
            Destroy(gameObject);
    }
}
