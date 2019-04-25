using UnityEngine;
using UnityEngine.Events;

public abstract class AEntity : MonoBehaviour
{
    [SerializeField] private GameObject m_deathFeedback;

    [HideInInspector] public UnityEvent DeathEvent = new UnityEvent();

    public virtual void Kill()
    {
        DeathEvent.Invoke();

        if (m_deathFeedback != null)
            Instantiate(m_deathFeedback, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
