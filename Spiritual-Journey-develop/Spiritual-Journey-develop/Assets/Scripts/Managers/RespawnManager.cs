using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    GameObject m_player;
	void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        Respawn(); 	
	}
    void Respawn()
    {
        if(m_player == null)
        {
            m_player.transform.position = CheckPointManager.CheckPointPosition;
        }
    }
}
