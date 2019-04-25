using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private float m_degreesPerSeconds = 0;
    [SerializeField] private Vector3 m_rotationAxis = new Vector3(0,0,0);

    private void Update()
    {
        transform.Rotate(m_rotationAxis * m_degreesPerSeconds * Time.deltaTime);
    }
}
