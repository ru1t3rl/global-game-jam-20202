using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using GGJ.Storage;
using DG.Tweening;

namespace GGJ.Spells
{
    public class BaseSpell : MonoBehaviour
    {
        [SerializeField] ParticleSystem visualEffect;
        [SerializeField] VisualEffect vfx;
        [SerializeField] SpellStats stats;

        public UnityEvent onImpact;

        Vector3 origin;

        Tweener tweener;

        public bool Active => tweener.active;

        public void Attack()
        {
            if (!HasAllResources)
                return;

            origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            StartRoutine();
            StartVisualEffect();
        }

        public void Attack(Vector3 position)
        {
            if (!HasAllResources)
                return;

            transform.position = position;
            origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            StartRoutine();
            StartVisualEffect();
        }

        public void Attack(Vector3 position, Vector3 target)
        {
            if (!HasAllResources)
                return;

            transform.position = position;
            origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            StartRoutine(target);
            StartVisualEffect();
        }

        void StartVisualEffect()
        {
            if (visualEffect)
                visualEffect.Play();

            if (vfx)
                vfx.Play();
        }

        void StartRoutine()
        {
            if (tweener != null)
                tweener.Complete();

            tweener = transform.DOMove(transform.position + transform.forward * stats.Range, stats.Range / stats.Speed);
            tweener.onComplete += () => tweener = null;
        }

        void StartRoutine(Vector3 target)
        {
            if (tweener != null)
                tweener.Complete();

            tweener = transform.DOMove(target, stats.Range / stats.Speed);
            tweener.onComplete += () => tweener = null;
        }

        public void StopSpell()
        {
            if (tweener != null)
                tweener.Complete();

            tweener = null;
        }

        public bool HasAllResources
        {
            get
            {
                for (int i = 0; i < stats.Elements.Length; i++)
                {
                    if (stats.Elements[i].amount > SimpleInventory.Instance.Count(stats.Elements[i].element.name))
                        return false;
                }
                return true;
            }
        }

        void UseResources()
        {
            for (int i = 0; i < stats.Elements.Length; i++)
            {
                SimpleInventory.Instance.UseItem(stats.Elements[i].element, stats.Elements[i].amount);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            onImpact?.Invoke();

            Entity entity = other.GetComponent<Entity>();

            if (entity != null)
            {
                entity.ApplyDamage(stats.Damage);
            }
        }
    }
}