using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AEntity))]
public class LifeSystem : MonoBehaviour
{
    [SerializeField] private float m_maxLife = 0;
    [SerializeField] private float m_invincibilityDurationInSeconds = 0;

    private AEntity m_entityBase;
    private float m_currentLife;
    private float m_invincibilityTimer;
    private bool m_invincible;

    public UnityEvent DamagesReceivedEvent = new UnityEvent();

    private void Awake()
    {
        m_entityBase = GetComponent<AEntity>();
        m_currentLife = m_maxLife;
        m_invincible = false;
    }

    private void Update()
    {
        if (m_invincible)
        {
            m_invincibilityTimer -= Time.deltaTime;
            if (m_invincibilityTimer <= 0)
                m_invincible = false;
        }
    }

    public void RemoveLife(float p_value = 1)
    {
        if (!m_invincible)
        {
            DamagesReceivedEvent.Invoke();
            m_currentLife -= p_value;
            SetInvincible(m_invincibilityDurationInSeconds);
            if (m_currentLife <= 0)
            {
                m_currentLife = 0;
                m_entityBase.Kill();
            }
        }
    }

    private void SetInvincible(float p_duration)
    {
        m_invincibilityTimer = p_duration;
        m_invincible = true;
    }

    public bool IsInvincible()
    {
        return m_invincible;
    }
}
