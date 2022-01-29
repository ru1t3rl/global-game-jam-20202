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
        [SerializeField] protected string id;
        [SerializeField] protected string description;
        [SerializeField] protected int amount = 0;

        [Header("Base Events")]
        [SerializeField] protected UnityEvent onAdd, onSubtract, onUse;


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
            Subtract(amount);
            onUse?.Invoke();
        }

        public virtual void SetAmount(int amount) { this.amount = amount; }

        #region Properties
        public int Count => amount;
        public string Id => id;
        public string Description => description;
        public Sprite Icon => icon;
        #endregion
    }
}