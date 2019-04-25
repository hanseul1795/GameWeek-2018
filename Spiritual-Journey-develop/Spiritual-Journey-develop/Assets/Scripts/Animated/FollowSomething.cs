using UnityEngine;

public class FollowSomething : MonoBehaviour
{
    private enum SmoothMode
    {
        LERP,
        SMOOTH_DAMP
    }

    [SerializeField] private Vector3 m_offset = new Vector3(0, 0, 0);
    [SerializeField] private SmoothMode m_smoothMode = SmoothMode.LERP;
    [SerializeField] private float m_lerpCoefficient = 0;
    [SerializeField] private float m_smoothDampDuration = 0;
    [SerializeField] private GameObject m_toFollow = null;
    [SerializeField] private bool m_lockRange = false;
    [SerializeField] private Vector2 m_xAxisRange = new Vector2(0, 0);
    [SerializeField] private Vector2 m_yAxisRange = new Vector2(0, 0);

    private Vector3 m_targetPosition;
    private Vector3 m_currentVelocity;

    private void Update()
    {
        if (m_toFollow != null)
        {
            m_targetPosition = m_toFollow.transform.position;
        }

        switch (m_smoothMode)
        {
            case SmoothMode.LERP:
                FollowTargetLerp();
                break;

            case SmoothMode.SMOOTH_DAMP:
                FollowTargetSmoothDamp();
                break;
        }
    }

    private void FollowTargetLerp()
    {
        Vector3 targetPostion = GetTargetPosition();
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPostion, m_lerpCoefficient);
        transform.position = smoothPosition;
    }

    private void FollowTargetSmoothDamp()
    {
        Vector3 targetPosition = GetTargetPosition();
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref m_currentVelocity, m_smoothDampDuration);
        transform.position = smoothPosition;
    }

    private Vector3 GetTargetPosition()
    {
        var targetPosition = m_targetPosition + m_offset;

        if (m_lockRange)
        {
            if (targetPosition.x < m_xAxisRange.x)
                targetPosition.x = m_xAxisRange.x;
            if (targetPosition.x > m_xAxisRange.y)
                targetPosition.x = m_xAxisRange.y;

            if (targetPosition.y < m_yAxisRange.x)
                targetPosition.y = m_yAxisRange.x;
            if (targetPosition.y > m_yAxisRange.y)
                targetPosition.y = m_yAxisRange.y;
        }

        return targetPosition;
    }
}