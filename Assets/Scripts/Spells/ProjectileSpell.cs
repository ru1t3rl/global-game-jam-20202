using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Spells
{
    public class ProjectileSpell : BaseSpell
    {
        float offset = 0.5f;

        protected override void BeginSpell(Transform origin)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            transform.position = origin.position + origin.forward * offset;
            Vector3 target = transform.position + origin.forward * stats.Range;

            rb.velocity = (target - transform.position) * stats.Speed;

            StartVisualEffect();
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
