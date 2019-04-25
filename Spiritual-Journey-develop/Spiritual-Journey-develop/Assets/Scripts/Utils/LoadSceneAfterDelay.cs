using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterDelay : MonoBehaviour
{
    [SerializeField] private string m_sceneName;
    [SerializeField] private float m_delayInSeconds;

    [SerializeField] private bool m_needToGetTriggered;

    private float m_timer;
    private bool m_triggered;

    private void Update()
    {
        if (!m_needToGetTriggered || m_triggered)
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_delayInSeconds)
                SceneManager.LoadScene(m_sceneName);
        }
    }

    public void Trigger()
    {
        m_triggered = true;
    }
}
