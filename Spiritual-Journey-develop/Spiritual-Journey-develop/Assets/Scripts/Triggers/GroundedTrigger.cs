using UnityEngine;
using UnityEngine.Events;

public class GroundedTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask m_whatIsGround;
    [HideInInspector] public UnityEvent GroundedEvent = new UnityEvent();
    [HideInInspector] public UnityEvent NotGroundedEvent = new UnityEvent();

    private uint m_collisionCounter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
            if (LayerTools.IsInLayerMask(m_whatIsGround, other.gameObject.layer))
                ++m_collisionCounter;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.isTrigger)
            if (LayerTools.IsInLayerMask(m_whatIsGround, other.gameObject.layer))
                --m_collisionCounter;
    }

    private void FixedUpdate()
    {
        if (m_collisionCounter > 0)
            GroundedEvent.Invoke();
        else
            NotGroundedEvent.Invoke();
    }
}
