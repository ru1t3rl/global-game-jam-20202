using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using GGJ.Storage;
using DG.Tweening;

namespace GGJ.Spells
{
    public abstract class BaseSpell : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem visualEffect;
        [SerializeField] protected VisualEffect vfx;
        [SerializeField] protected SpellStats stats;

        private float lifetime;
        protected float currentLifetime;

        public SpellStats Stats => stats;

        public UnityEvent onImpact;

        private void Awake()
        {
            lifetime = stats.Range / stats.Speed;
        }

        protected virtual void Update()
        {
            currentLifetime += Time.deltaTime;
            if (currentLifetime > lifetime)
                Destroy(gameObject);
        }

        public void TryPerform(Transform origin)
        {
            if (!HasAllResources)
                return;

            BeginSpell(origin);
            StartVisualEffect();
        }

        protected abstract void BeginSpell(Transform origin);

        protected void StartVisualEffect()
        {
            if (visualEffect)
                visualEffect.Play();

            if (vfx)
                vfx.Play();
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

        private void UseResources()
        {
            for (int i = 0; i < stats.Elements.Length; i++)
            {
                SimpleInventory.Instance.UseItem(stats.Elements[i].element, stats.Elements[i].amount);
            }
        }

    }
}