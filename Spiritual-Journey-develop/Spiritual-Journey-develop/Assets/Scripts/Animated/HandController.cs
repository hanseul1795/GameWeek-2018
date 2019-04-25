using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    [SerializeField] private float m_alertRadius = 0;
    [SerializeField] private float m_dangerRadius = 0;
    [SerializeField] private float m_rotationSpeed = 0;
    [SerializeField] private float m_fetchingSpeed = 0;
    //[SerializeField] private float m_speedFactor = 0;
    //[SerializeField] private float m_maxSize = 0;

    private Vector3 m_velocity;
    private GameObject m_player;
    //private Quaternion m_initialRotation;
    private Vector3 m_initialPosition;

    private void Awake()
    {
        m_initialPosition = transform.position;
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

	private void Update()
    {
        if (m_player != null)
        {
            float distance = CalculateDistance();

            if (distance <= m_alertRadius && distance > m_dangerRadius)
            {
                // ALERT MODE
                RotateToPlayer();
            }
            else if (distance <= m_dangerRadius)
            {
                // DANGER MODE
                RotateToPlayer();
                FetchPlayer();
            }
            else
            {
                ResetTransform();
            }
        }
        else
        {
            ResetTransform();
        }
	}
    
    private void ResetTransform()
    {
        ResetPosition();
        ResetRotation();
    }

    private void ResetRotation()
    {
        float angle = Mathf.Atan2(0, 0) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, q, m_rotationSpeed * Time.deltaTime);
    }

    private void ResetPosition()
    {
        transform.position = Vector3.SmoothDamp(transform.position, m_initialPosition, ref m_velocity, 1.0f);
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(m_player.transform.position, m_initialPosition); 
    }

    private void RotateToPlayer()
    {
        Vector3 dir = m_player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, m_rotationSpeed * Time.deltaTime);
    }

    private void FetchPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_player.transform.position, m_fetchingSpeed * Time.deltaTime);
    }
}
