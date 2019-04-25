using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private CheckPoint m_checkPoint = null;

    public static Vector3 CheckPointPosition { get; set; }
		
	void Update ()
    {
        DestroyReachedCheckPoint();
	}

    void DestroyReachedCheckPoint()
    {
        if(m_checkPoint.IsCheckPointReached())
        {
            CheckPointPosition = m_checkPoint.transform.position;
            Destroy(m_checkPoint);
        }
    }
}