using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ACustomTrigger))]
public abstract class ATriggerable : MonoBehaviour
{
    [SerializeField] private bool m_repeat = false;

    private uint m_executions;

    private ACustomTrigger m_triggerSystem;

    protected virtual void Awake()
    {
        m_executions = 0;
        m_triggerSystem = GetComponent<ACustomTrigger>();
        m_triggerSystem.TriggerEvent.AddListener(OnTrigger);
    }

    private void OnTrigger()
    {
        if (m_executions == 0 || m_repeat)
        {
            Execute();
            ++m_executions;
        }
    }

    protected abstract void Execute();
}
