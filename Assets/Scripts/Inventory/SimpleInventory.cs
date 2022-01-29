using System.Collections.Generic;
using UnityEngine;
using GGJ.Utilities;

namespace GGJ.Storage
{
    public class SimpleInventory : MonoBehaviour
    {
        Dictionary<Item, int> items = new Dictionary<Item, int>();

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
            if (items.ContainsKey(item.Id))
            {
                items[items.GetItem(item)] += item.Count;
                items.GetItem(item).item.SetAmount(items[items.GetItem(item)]);
            }
            else
                items.Add(new Item(item.Id, item), item.Count);
        }

        public void AddItem(BaseInventoryItem item, int amount)
        {
            if (items.ContainsKey(item.Id))
            {
                items[items.GetItem(item)] += amount;
                items.GetItem(item).item.SetAmount(items[items.GetItem(item)]);
            }
            else
                items.Add(new Item(item.Id, item), amount);
        }

        public void Removeitem(BaseInventoryItem item)
        {
            if (items.ContainsKey(item.Id))
                items.Remove(items.GetItem(item));
            else
                Debug.LogError($"<b>[{gameObject.name}]</b> Doesn't contain an item with the name {item.name}");
        }

        public void UseItem(BaseInventoryItem item)
        {
            items.GetItem(item).item.Use();
            items[items.GetItem(item)] = items.GetItem(item).item.Count;
        }

        public void UseItem(BaseInventoryItem item, int amount)
        {
            items.GetItem(item).item.Use(amount);
            items[items.GetItem(item)] = items.GetItem(item).item.Count;
        }

        public void UseItem(string name)
        {
            items.GetItem(name)?.item.Use();
            items[items.GetItem(name)] = items.GetItem(name).item.Count;
        }
    }

    [System.Serializable]
    public class Item
    {
        public string name;

        public BaseInventoryItem item;

        public Item(string name, BaseInventoryItem item)
        {
            this.name = name;
            this.item = item;
        }
    }
}