using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFruit : APickup
{
    [SerializeField] private float m_timeToAdd = 0;

    protected override void Collect()
    {
        WolfManager.AddLifetime(m_timeToAdd);
    }
}
