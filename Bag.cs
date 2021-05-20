using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class Bag : Item, IHaveInventory
    {
        Inventory _inventory;
        public Bag(string[] ids, string name, string desc): base(ids, name, desc)
        {
            _inventory = new Inventory();
        }
        public GameObject Locate(string id)
        {
            if (id == this.FirstId)
            {
                return this;
            }
            else if (_inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            }
            else
            {
                return null;
            }
        }

        public void Put(Item item)
        {
            if(item != this) // don't put bag in bag
            {
                _inventory.Put(item);
            }
        }
        public GameObject Take(string id)
        {
            return _inventory.Take(id);
        }
        public override string FullDescription { 
            get {
                return $"In the {Name} you can see: {_inventory.ItemList}";
            } 
        }
        public Inventory Inventory {  get { return _inventory; } }
    }
}
