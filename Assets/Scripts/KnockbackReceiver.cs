using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Entity), typeof(Rigidbody))]

public class KnockbackReceiver : MonoBehaviour
{
    public UnityEvent OnKnockback;
    private Entity entity;
    private Rigidbody rigidBody;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        rigidBody = GetComponent<Rigidbody>();
        entity.OnTakeDamage.AddListener((Entity entity, DamageData damageData) => ApplyKnockback(damageData));
    }

    private void ApplyKnockback(DamageData data)
    {
        OnKnockback.Invoke();
        Vector3 hitDirection = new Vector3(transform.position.x - data.hitPoint.x, 1, transform.position.z - data.hitPoint.z).normalized;
        
        rigidBody.velocity = hitDirection * data.knockbackAmount;
    }
}
