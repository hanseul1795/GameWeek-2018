using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dangerous : MonoBehaviour
{
    [SerializeField] private float m_damagesOnContact = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollideWith(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollideWith(collision.gameObject);
    }

    private void OnCollideWith(GameObject p_target)
    {
        var targetLifeSystem = p_target.GetComponent<LifeSystem>();

        if (targetLifeSystem != null)
        {
            targetLifeSystem.RemoveLife(m_damagesOnContact);
        }
    }
}
