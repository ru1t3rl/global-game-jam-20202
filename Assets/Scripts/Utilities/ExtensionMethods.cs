using System.Collections;
using System.Collections.Generic;
using GGJ.Storage;

namespace GGJ.Utilities
{
    public static class ExtensionMethods
    {
        public static BaseInventoryItem GetItem(this Dictionary<BaseInventoryItem, int> items, BaseInventoryItem item)
        {
            foreach (BaseInventoryItem kItem in items.Keys)
            {
                if (kItem.Name == item.Name)
                    return kItem;
            }
            return null;
        }

        public static BaseInventoryItem GetItem(this Dictionary<BaseInventoryItem, int> items, string itemName)
        {
            foreach (BaseInventoryItem kItem in items.Keys)
            {
                if (kItem.Name == itemName)
                    return kItem;
            }
            return null;
        }
    }
}