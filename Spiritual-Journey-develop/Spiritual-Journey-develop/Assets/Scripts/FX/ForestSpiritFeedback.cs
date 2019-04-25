using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpiritFeedback : MonoBehaviour
{
    private List<ParticleManager> m_particlesEmitter = new List<ParticleManager>();
    private PlayerBase m_player;

    private uint m_currentForestSpirits;

    private void Awake()
    {
        foreach (var particleManager in transform.GetComponentsInChildren<ParticleManager>())
            m_particlesEmitter.Add(particleManager);

        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null)
            m_player = playerGameObject.GetComponent<PlayerBase>();
    }

    private void Start()
    {
        if (m_player != null)
        {
            ListenToPlayer();
        }
    }

    private void ListenToPlayer()
    {
        m_player.AddForestSpiritEvent.AddListener(AddForestSpirit);
        m_player.RemoveForestSpiritEvent.AddListener(RemoveForestSpirit);
        m_player.ResetForestSpiritsEvent.AddListener(ResetForestSpirits);
        m_player.DeathEvent.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        DesactivateParticleEmitters();
    }

    private void DesactivateParticleEmitters()
    {
        foreach (var particleEmitter in m_particlesEmitter)
            particleEmitter.EnableEmission(false);
    }

    private void UpdateParticles()
    {
        DesactivateParticleEmitters();

        for (uint i = 0; i < m_currentForestSpirits; ++i)
        {
            m_particlesEmitter[(int)i].EnableEmission(true);
        }
    }

    private void SetForestSpirits(uint p_points)
    {
        if (m_currentForestSpirits != p_points)
        {
            m_currentForestSpirits = p_points;
            UpdateParticles();
        }
    }

    private void AddForestSpirit()
    {
        SetForestSpirits(m_currentForestSpirits + 1);
    }

    private void RemoveForestSpirit()
    {
        SetForestSpirits(m_currentForestSpirits - 1);
    }

    private void ResetForestSpirits()
    {
        SetForestSpirits(m_player.GetInitialForestSpirits());
    }
}
