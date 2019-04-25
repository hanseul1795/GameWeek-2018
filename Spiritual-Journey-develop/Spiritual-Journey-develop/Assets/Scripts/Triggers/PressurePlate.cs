using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Sprite m_offSprite = null;
    [SerializeField] private Sprite m_onSprite = null;

    [SerializeField] private bool m_stayOn = false;

    private ParticleSystem m_particleSystem;
    private ParticleSystem.EmissionModule m_emissionModule;

    public UnityEvent ActivationEvent = new UnityEvent();
    public UnityEvent DesactivationEvent = new UnityEvent();

    private SpriteRenderer m_spriteRenderer;

    private uint m_collisionCounter = 0;

    private bool m_state;
    //private bool m_somethingOn;

    private void Awake()
    {
        m_state = false;
       // m_somethingOn = false;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_particleSystem = GetComponent<ParticleSystem>();
        m_emissionModule = m_particleSystem.emission;
        UpdateEmissionModule();
    }

    private void SetState(bool p_newState)
    {
        if (p_newState != m_state)
        {
            m_state = p_newState;

            if (m_state)
            {
                SetSprite(m_onSprite);
                ActivationEvent.Invoke();
            }
            else
            {
                SetSprite(m_offSprite);
                DesactivationEvent.Invoke();
            }

            UpdateEmissionModule();
        }
    }

    private void UpdateEmissionModule()
    {
        m_emissionModule.enabled = m_state;
    }

    private void SetSprite(Sprite p_toSet)
    {
        m_spriteRenderer.sprite = p_toSet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ++m_collisionCounter;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!m_stayOn)
            --m_collisionCounter;
    }

    private void FixedUpdate()
    {
        SetState(m_collisionCounter > 0);
    }
}
