using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    private float _currentHealth;

    public UnityEvent<Entity> OnTakeDamage, OnDeath;

    protected float MaxHealth => _maxHealth;

    protected float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Min(MaxHealth, value); }
    }

    protected virtual void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public void ApplyDamage(float damage)
    {
        CurrentHealth -= damage;

        OnTakeDamage?.Invoke(this);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        OnDeath?.Invoke(this);
    }
}
