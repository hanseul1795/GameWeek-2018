using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [Header("GENERAL SETTINGS")]
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] private List<Sprite> m_storyTexts;
    [SerializeField] private float m_textDurationInSeconds;
    [SerializeField] private float m_appearDurationInSeconds;
    [SerializeField] private float m_disappearDurationInSeconds;
    [SerializeField] private string m_sceneToLoadOnEnd;
    [SerializeField] private float m_fastForwardCoefficient;

    [Header("INPUTS")]
    [SerializeField] private string m_fastForwardInput;

    private Queue<Sprite> m_storyTextsQueue;
    private float m_timer;
    private float m_defaultTimeScale;
    private bool m_ended = false;

    private void Awake()
    {
        InitQueue();
        ResetTimer();
        GoToNextImage();
    }

    private void Start()
    {
        m_defaultTimeScale = Time.timeScale;
    }

    private void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer >= m_textDurationInSeconds)
            GoToNextImage();
        else
            SetSpriteAlpha(CalculateProgress());

        if (!m_ended)
            Time.timeScale = Input.GetButton(m_fastForwardInput) ? m_fastForwardCoefficient : 1.0f;
    }

    private void ResetTimeScale()
    {
        Time.timeScale = m_defaultTimeScale;
    }

    private float CalculateProgress()
    {
        if (m_timer < m_appearDurationInSeconds)
            return m_timer / m_appearDurationInSeconds;
        else if (m_timer > m_textDurationInSeconds - m_disappearDurationInSeconds)
            return (m_textDurationInSeconds - m_timer) / m_disappearDurationInSeconds;

        return 255.0f;
    }

    private void SetSpriteAlpha(float p_value)
    {
        Color currentColor = m_spriteRenderer.color;
        currentColor.a = p_value;

        m_spriteRenderer.color = currentColor;
    }

    private void InitQueue()
    {
        if (m_storyTextsQueue == null)
            m_storyTextsQueue = new Queue<Sprite>();
        else
            m_storyTextsQueue.Clear();

        foreach (var sprite in m_storyTexts)
            m_storyTextsQueue.Enqueue(sprite);
    }

    private void ResetTimer()
    {
        m_timer = 0;
    }

    private void GoToNextImage()
    {
        if (m_storyTextsQueue.Count == 0)
        {
            End();
        }
        else
        {
            m_spriteRenderer.sprite = m_storyTextsQueue.Dequeue();
            ResetTimer();
        }
    }

    private void End()
    {
        m_ended = true;
        ResetTimeScale();
        SceneManager.LoadScene(m_sceneToLoadOnEnd);
    }
}
