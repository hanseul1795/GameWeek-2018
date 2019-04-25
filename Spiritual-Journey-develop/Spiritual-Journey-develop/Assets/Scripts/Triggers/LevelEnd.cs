using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private string m_creditSceneName;
    [SerializeField] private float m_delayInSeconds;

    private float m_timer;

    private bool m_reached;
    private bool m_end;


    private void Update()
    {
        if (m_reached && !m_end)
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_delayInSeconds)
            {
                SceneManager.LoadScene(m_creditSceneName);
                m_end = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_reached = true;
    }
}
