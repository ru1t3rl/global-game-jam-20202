using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    private float _currentHealth;

    protected float MaxHealth { get; set; }
    
    protected float CurrentHealth 
    { 
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Min(MaxHealth, value); }
    }

    public void ApplyDamage(float damage)
    {
        CurrentHealth -= damage;

        OnTakeDamage();

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        OnDeath();
    }

    protected virtual void OnTakeDamage() { }

    protected virtual void OnDeath() { }
}
