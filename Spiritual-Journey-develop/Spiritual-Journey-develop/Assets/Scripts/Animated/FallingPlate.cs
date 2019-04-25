using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlate : MonoBehaviour
{
    [SerializeField] private float m_speed = 0;
    [SerializeField] private bool m_OnTrigger = false;
    [SerializeField] private float m_timeToDestroy;
    [SerializeField] private float m_timeBeforeFalling;
    [SerializeField] private float m_shakingIntensity = 0;
    private float m_ShakingTime;

    private bool m_onTrig;
    private bool m_onCollision = false;

    private void Start()
    {
        m_ShakingTime = m_timeBeforeFalling;
        m_onTrig = !m_OnTrigger;
    }
	
	private void FixedUpdate ()
    {
        Shake();
        Fall();
        if (m_onTrig && m_OnTrigger)
        {
            Falling();
        }
	}

    private void Fall()
    {
        if(!m_OnTrigger)
        {
            if(m_onCollision)
            {
                Falling();
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!m_OnTrigger)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (collision.gameObject.tag == "Player" && player != null && player.IsGroundedWithConsistency())
                m_onCollision = true;
                
        }
    }

    private void Shake()
    {
        if (m_onCollision)
        {
            m_ShakingTime -= Time.deltaTime;
            if (m_ShakingTime > 0.0f)
                this.transform.position += new Vector3(Random.insideUnitCircle.x /m_shakingIntensity, 0 , 0);
        }
    }

    public void Falling()
    {
            m_timeBeforeFalling -= Time.deltaTime;
            if (m_timeBeforeFalling <= 0.0f)
            {
                transform.position -= new Vector3(0, 1, 0) * m_speed * Time.deltaTime;
                m_timeToDestroy -= Time.deltaTime;
                if (m_timeToDestroy <= 0.0f)
                {
                    gameObject.SetActive(false);
                }
            }
    }

    public void Triggered()
    {
        if(!m_onTrig)
            m_onTrig = true;
    }
}
