using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ.Storage
{
    public class BaseInventoryItem : MonoBehaviour
    {
        [Header("Base Info")]
        [SerializeField] protected Sprite icon;
        [SerializeField] protected new string name;
        [SerializeField] protected string description;

        [Header("Base Events")]
        [SerializeField] protected UnityEvent onAdd, onSubtract, onUse;
        protected int amount = 0;

        public virtual void Add(int amount = 1)
        {
            this.amount += (this.amount + amount >= 0 ? amount : 0);
            onAdd?.Invoke();
        }

        public virtual void Subtract(int amount = 1)
        {
            this.amount -= (this.amount - amount >= 0 ? amount : 0);
            onSubtract?.Invoke();
        }

        public virtual void Use() { onUse?.Invoke(); }
        public virtual void Use(int amount)
        {
            this.amount -= (this.amount - amount >= 0 ? amount : 0);
            onUse?.Invoke();
        }

        #region Properties
        public int Count => amount;
        public string Name => name;
        public string Description => description;
        public Sprite Icon => icon;
        #endregion
    }
}