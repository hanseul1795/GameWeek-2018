using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicManager : MonoBehaviour
{
    [SerializeField] private float m_delayBeforeRestart;

    private GameObject m_player;
    private PlayerBase m_playerBase;

    private float m_restartTimer;

    private bool m_playerIsDead;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_playerBase = m_player.GetComponent<PlayerBase>();

        m_playerBase.DeathEvent.AddListener(OnPlayerDeath);
    }

    private void Update()
    {
        if (m_playerIsDead)
        {
            m_restartTimer -= Time.deltaTime;
            if (m_restartTimer <= 0.0f)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnPlayerDeath()
    {
        m_playerIsDead = true;
        m_restartTimer = m_delayBeforeRestart;
    }
}
