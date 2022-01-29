using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    [SerializeField]
    private List<EnemyDrop> enemyDrops;

    protected override void Awake()
    {
        OnDeath.AddListener((Entity entity, DamageData data) => DropItemsOnDeath());
        OnDeath.AddListener((Entity entity, DamageData data) => DestroyOnDeath());
        base.Awake();
    }

    private void DropItemsOnDeath()
    {
        foreach (EnemyDrop drop in enemyDrops)
        {
            if (Random.Range(0f, 1f) <= drop.dropChance)
            {
                int dropAmount = Random.Range(drop.minAmount, drop.maxAmount);

                for (int i = 0; i < dropAmount; i++)
                {
                    float xRotationAngle = Random.Range(-10f, 10f); //Determine some random angles to launch the pickup in.
                    float zRotationAngle = Random.Range(-10f, 10f);

                    Vector3 dropAngle = Quaternion.Euler(xRotationAngle, 0, zRotationAngle) * Vector3.up;

                    GameObject pickup = Instantiate(drop.dropPrefab, transform.position, Quaternion.identity);
                    if (pickup.TryGetComponent(out Rigidbody rigidBody))
                        rigidBody.velocity += dropAngle * 3;
                }
            }
        }
    }

    private void DestroyOnDeath()
    {
        Destroy(gameObject);
    }
}

[Serializable]
public class EnemyDrop
{
    public GameObject dropPrefab;
    public int minAmount;
    public int maxAmount;
    public float dropChance;

    public EnemyDrop(GameObject dropPrefab, int minAmount, int maxAmount, float dropChance)
    {
        this.dropPrefab = dropPrefab;
        this.minAmount = minAmount;
        this.maxAmount = maxAmount;
        this.dropChance = dropChance;
    }
}
