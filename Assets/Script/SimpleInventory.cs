using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Storage
{
    public class SimpleInventory : MonoBehaviour
    {
        Dictionary<BaseInventoryItem, int> items = new Dictionary<BaseInventoryItem, int>();

        public void AddItem(BaseInventoryItem item)
        {
            if (items.ContainsKey(item))
            {
                items[item] += item.Count;

                
            }
            else
                items.Add(item, item.Count);
        }

        public void AddItem(BaseInventoryItem item, int amount)
        {
            if (items.ContainsKey(item))
            {
                items[item] += amount;
                items[item]++;
            }
            else
                items.Add(item, item.Count);
        }

        public void Removeitem(BaseInventoryItem item)
        {
            if (items.ContainsKey(item))
                items.Remove(item);
            else
                Debug.LogError($"<b>[{gameObject.name}]</b> Doesn't contain an item with the name {item.name}");
        }

        public void UseItem(BaseInventoryItem item)
        {
            foreach (BaseInventoryItem kItem in items.Keys)
            {
                if (item.Name == kItem.name)
                    kItem.Use();
            }
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