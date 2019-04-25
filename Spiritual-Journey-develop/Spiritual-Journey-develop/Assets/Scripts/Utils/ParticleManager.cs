using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleManager : MonoBehaviour
{
    private ParticleSystem m_particleSystem;

    private void Awake()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetStartColor(Color p_color)
    {
        if (m_particleSystem == null)
            return;

        var mainModule = m_particleSystem.main;
        mainModule.startColor = p_color;
    }
    
    public void SetEmitterRate(float p_newRate)
    {
        if (m_particleSystem == null)
            return;

        var emission = m_particleSystem.emission;
        emission.rateOverTime = p_newRate;
    }

    public void EnableEmission(bool p_enable)
    {
        if (m_particleSystem == null)
            return;

        var emission = m_particleSystem.emission;
        emission.enabled = p_enable;
    }
}
