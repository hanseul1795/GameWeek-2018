using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class APickup : MonoBehaviour
{
    [SerializeField] private ParticleManager m_particuleEffectOnPickup = null;

    protected PlayerBase m_playerBaseScript;
    protected ParticleSystem m_particleSystem;
    protected Sprite m_sprite;
    protected Color m_averageColor;
    protected abstract void Collect();

    private void Awake()
    {
        m_sprite = GetComponent<SpriteRenderer>().sprite;
        m_averageColor = ColorTools.CalculateAverageColor(m_sprite);
        m_particleSystem = GetComponent<ParticleSystem>();
        var particleSystemMain = m_particleSystem.main;
        particleSystemMain.startColor = m_averageColor;

        var player = GameObject.Find("Player");
        if (player)
            m_playerBaseScript = GetComponent<PlayerBase>();
        else
            Debug.LogError("Player needed to init pickup");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
        if (m_particuleEffectOnPickup != null)
        {
            var gameObject = Instantiate(m_particuleEffectOnPickup, transform.position, Quaternion.identity);
            gameObject.GetComponent<ParticleManager>().SetStartColor(m_averageColor);
        }
        else
            Debug.LogError("No feedback prefab for this pickup");
        Destroy(gameObject);
    }
}
