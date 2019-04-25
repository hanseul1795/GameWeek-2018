using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField] private float m_slowTimer = 0;
    [SerializeField] private float m_slowingRate = 0;

    private float m_timer;
    private bool m_startTimer = false;
	void Start ()
    {
        m_timer = m_slowTimer;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        SlowTime();
	}

    void SlowTime()
    {
        if(Input.GetKeyDown(KeyCode.T) && !m_startTimer)
        {
            m_startTimer = true;
        }
        if(m_startTimer)
        {
            m_timer -= Time.deltaTime;
            if(Time.timeScale == 1.0f)
            {
                Time.timeScale = Time.timeScale / m_slowingRate;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            if(m_timer <= 0 && Time.timeScale != 1.0f)
            {
                m_startTimer = false;
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                m_timer = m_slowTimer;                
            }
        }       
    }
}
