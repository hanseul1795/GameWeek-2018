using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    [SerializeField] private float m_targetRatio = 0;
    [SerializeField] private float m_animationSpeed = 0;

    private List<Vector3> m_targetScales = new List<Vector3>();
    private Queue<Vector3> m_targetScalesQueue = new Queue<Vector3>();

    private Vector3 m_currentVelocity;

    private void Awake()
    {
        m_targetScales.Add(transform.localScale);

        var targetScale = transform.localScale;
        targetScale.Scale(new Vector3(m_targetRatio, m_targetRatio, 1.0f));

        m_targetScales.Add(targetScale);
    }

    private void ResetQueue()
    {
        m_targetScalesQueue.Clear();

        foreach (var targetScale in m_targetScales)
            m_targetScalesQueue.Enqueue(targetScale);
    }

    private void Update()
    {
        if (m_targetScalesQueue.Count > 0)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, m_targetScalesQueue.Peek(), ref m_currentVelocity, m_animationSpeed);

            if (transform.localScale == m_targetScalesQueue.Peek())
                m_targetScalesQueue.Dequeue();
        }
        else
        {
            ResetQueue();
        }
    }
}
