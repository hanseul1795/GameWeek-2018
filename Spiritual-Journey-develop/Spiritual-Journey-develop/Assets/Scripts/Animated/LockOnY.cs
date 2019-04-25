using UnityEngine;
using UnityEngine.Events;

public class LockOnY : MonoBehaviour
{
    [SerializeField] private bool m_ifSuperior;
    [SerializeField] private bool m_ifInferior;

    [SerializeField] private float m_lockValue;

    public UnityEvent LockedEvent = new UnityEvent();

    private void Update()
    {
        if (m_ifSuperior && transform.position.y > m_lockValue)
        {
            transform.position = new Vector3(transform.position.x, m_lockValue, transform.position.z);
            LockedEvent.Invoke();
        }

        if (m_ifInferior && transform.position.y < m_lockValue)
        {
            transform.position = new Vector3(transform.position.x, m_lockValue, transform.position.z);
            LockedEvent.Invoke();
        }
    }
}
