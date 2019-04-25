using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : ACustomTrigger
{
    [SerializeField] private GameObject m_toEvaluate = null;
    [SerializeField] private float m_distance = 0;

    protected override bool Condition()
    {
        if (m_toEvaluate != null)
        {
            return (Vector2.Distance(m_toEvaluate.transform.position, transform.position) <= m_distance);
        }

        return false;
    }
}
