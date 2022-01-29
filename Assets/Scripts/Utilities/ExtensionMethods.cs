using System.Collections;
using System.Collections.Generic;
using GGJ.Storage;

namespace GGJ.Utilities
{
    public static class ExtensionMethods
    {
        public static Item GetItem(this Dictionary<Item, int> items, BaseInventoryItem item)
        {
            foreach (Item kItem in items.Keys)
            {
                if (kItem.name == item.Id)
                    return kItem;
            }
            return null;
        }

        public static Item GetItem(this Dictionary<Item, int> items, string itemName)
        {
            foreach (Item kItem in items.Keys)
            {
                if (kItem.name == itemName)
                    return kItem;
            }
            return null;
        }

        public static bool ContainsKey(this Dictionary<Item, int> items, string itemName)
        {
            foreach (Item item in items.Keys)
            {
                if (item.name == itemName)
                    return true;
            }

            return false;
        }
    }
}