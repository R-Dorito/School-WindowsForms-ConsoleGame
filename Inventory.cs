using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class Inventory
    {
        List<Item> _items;
        public Inventory()
        {
            _items = new List<Item>();
        }
        public bool HasItem(string id) 
        {
            if (Fetch(id) != null) 
                return true;
            else 
                return false;
        }
        public void Put(Item item)
        {
            _items.Add(item);
        }
        public Item Take(string id)
        {
            Item fetched = Fetch(id);
            _items.Remove(fetched);
            return fetched;
        }
        public Item Fetch(string id)
        {
            foreach(Item i in _items)
            {
                if (i.AreYou(id)) {
                    return i;
                }
            }
            return null;
        }
        public string ItemList { 
            get {
                string itemsList = "";
                if(_items.Count == 0)
                {
                    return "\n\tNothing";
                }
                foreach(Item i in _items)
                {
                    itemsList += "\n\t" + i.ShortDescription ;
                }
                return itemsList; 
            } 
        }
    }
}
