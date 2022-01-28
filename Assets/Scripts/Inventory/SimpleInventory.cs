using System.Collections.Generic;
using UnityEngine;
using GGJ.Utilities;

namespace GGJ.Storage
{
    public class SimpleInventory : MonoBehaviour
    {
        Dictionary<string, int> items = new Dictionary<string, int>();

        static SimpleInventory instance;
        public static SimpleInventory Instance => instance;

        private void Awake()
        {
            if (instance != null)
                Destroy(gameObject);

            instance = this;
        }

        public void AddItem(BaseInventoryItem item)
        {
            if (items.ContainsKey(item.Name))
            {
                items[item.Name] += item.Count;
                items.GetItem(item.Name).SetAmount(items[item.Name]);
            }
            else
                items.Add(item.Name, item.Count);
        }

        public void AddItem(BaseInventoryItem item, int amount)
        {
            if (items.ContainsKey(item.name))
            {
                items[item.name] += amount;
                items.GetItem(item).SetAmount(items[item.name]);
            }
            else
                items.Add(item.name, item.Count);
        }

        public void Removeitem(BaseInventoryItem item)
        {
            if (items.ContainsKey(item.name))
                items.Remove(item.name);
            else
                Debug.LogError($"<b>[{gameObject.name}]</b> Doesn't contain an item with the name {item.name}");
        }

        public void UseItem(BaseInventoryItem item)
        {
            items.GetItem(item).Use();
        }

        public void UseItem(BaseInventoryItem item, int amount)
        {
            items.GetItem(item).Use(amount);
        }

        public void UseItem(string name)
        {
            foreach (BaseInventoryItem kItem in items.Keys)
            {
                if (name == kItem.name)
                    kItem.Use();
            }
        }

    }
}