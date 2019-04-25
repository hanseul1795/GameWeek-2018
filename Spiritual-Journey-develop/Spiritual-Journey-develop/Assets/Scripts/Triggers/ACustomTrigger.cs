using UnityEngine;
using UnityEngine.Events;

public abstract class ACustomTrigger : MonoBehaviour
{
    public UnityEvent TriggerEvent = new UnityEvent();

    private void Update()
    {
        if (Condition())
        {
            TriggerEvent.Invoke();
        }
    }

    protected abstract bool Condition();
}
