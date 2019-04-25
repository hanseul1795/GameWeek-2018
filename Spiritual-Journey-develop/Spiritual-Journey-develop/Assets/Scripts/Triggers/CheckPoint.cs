using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool m_checkPointReached = false;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_checkPointReached = true;
        }
    }
    public bool IsCheckPointReached()
    { 
        return m_checkPointReached;
    }
}