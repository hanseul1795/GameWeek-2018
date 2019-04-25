using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerBase : AEntity
{
    [HideInInspector] public UnityEvent RemoveForestSpiritEvent = new UnityEvent();
    [HideInInspector] public UnityEvent AddForestSpiritEvent = new UnityEvent();
    [HideInInspector] public UnityEvent ResetForestSpiritsEvent = new UnityEvent();

    [SerializeField] private uint m_initialForestSpirits;

    private PlayerController m_playerController;
    private uint m_currentForestSpirits;

    private void Awake()
    {
        m_playerController = GetComponent<PlayerController>();

        ListenToPlayerController();
    }

    private void Start()
    {
        ResetForestSpirits();
    }

    private void ListenToPlayerController()
    {
        m_playerController.GroundedEvent.AddListener(ResetForestSpirits);
        m_playerController.BonusJumpEvent.AddListener(OnActionUsed);
        m_playerController.DashEvent.AddListener(OnActionUsed);
    }

    public uint GetCurrentForestSpirits()
    {
        return m_currentForestSpirits;
    }

    public uint GetInitialForestSpirits()
    {
        return m_initialForestSpirits;
    }

    public bool CanUseAction()
    {
        return m_currentForestSpirits > 0;
    }

    private void ResetForestSpirits()
    {
        m_currentForestSpirits = m_initialForestSpirits;
        ResetForestSpiritsEvent.Invoke();
    }

    private void OnActionUsed()
    {
        --m_currentForestSpirits;
        RemoveForestSpiritEvent.Invoke();
    }
}
