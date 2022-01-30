using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Spells
{
    public class ProjectileSpell : BaseSpell
    {
        protected override void PerformSpell(Vector3 position, Vector3 target)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            transform.position = position;
            rb.velocity = (target - position) * stats.Speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            onImpact?.Invoke();

            Entity entity = other.GetComponent<Entity>();

            if (entity != null)
            {
                entity.ApplyDamage(new DamageData(stats.Damage, stats.Knockback, gameObject, transform.position));
            }

            if (!stats.Piercing)
            {
                StopAllCoroutines();
                Destroy(gameObject);
            }
        }
    }
}
