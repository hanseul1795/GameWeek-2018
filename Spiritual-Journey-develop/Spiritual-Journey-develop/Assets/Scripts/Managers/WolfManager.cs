using UnityEngine;
using UnityEngine.Events;

public class WolfManager : MonoBehaviour
{
    [SerializeField] private float m_lifetime = 0;
    [SerializeField] private GameObject m_spirit = null;
    [SerializeField] private Color m_deathColor = Color.black;
    [SerializeField] private float m_particlesRateOverTime = 0;

    private Color m_startColor;

    private static float m_currentLifetime;
    private static bool m_dead;

    private SpriteRenderer m_spriteRenderer;
    private SpriteRenderer m_playerSpriteRenderer;
    private ParticleSystem m_particleSystem;
    private PlayerBase m_playerBase;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_particleSystem = GetComponent<ParticleSystem>();
        m_playerSpriteRenderer = m_spirit.GetComponent<SpriteRenderer>();
        m_playerBase = m_spirit.GetComponent<PlayerBase>();

        m_startColor = m_spriteRenderer.color;
        m_currentLifetime = m_lifetime;
        m_dead = false;
    }

    private void Update()
    {
        float progress = 1 - m_currentLifetime / m_lifetime;
        var particleSystemMain = m_particleSystem.main;
        var particleSystemEmission = m_particleSystem.emission;

        m_spriteRenderer.color = Color.Lerp(m_startColor, m_deathColor, progress);

        if (m_playerSpriteRenderer)
            m_playerSpriteRenderer.color = Color.Lerp(m_startColor, m_deathColor, progress / 2.0f);

        particleSystemEmission.rateOverTime = m_particlesRateOverTime * progress;

        if (m_playerSpriteRenderer)
            m_playerSpriteRenderer.color = new Color(m_playerSpriteRenderer.color.r, m_playerSpriteRenderer.color.g, m_playerSpriteRenderer.color.b, (1 - progress));

        particleSystemMain.startColor = m_spriteRenderer.color;

        if (!m_dead)
        {
            m_currentLifetime -= Time.deltaTime;
            if (m_currentLifetime <= 0)
            {
                m_dead = true;
                m_playerBase.Kill();
                Destroy(gameObject);
            }
        }
    }

    public static void AddLifetime(float p_lifetime)
    {
        m_currentLifetime += p_lifetime;
    }

    public static void RemoveLifetime(float p_lifetime)
    {
        m_currentLifetime -= p_lifetime;
    }
}
