using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class Player : GameObject, IHaveInventory
    {
        // a player has access to inventory and location.
        private Inventory _inventory;
        private Locations _location;

        public Player(string name, string desc) : base(new string[] { "me", "inventory" }, name, desc)
        {
            // need to initialize the inventory 
            _inventory = new Inventory();
        }
        public GameObject Locate (string id)
        {
            // AreYou and HasItem should convert id ToLower
            if (AreYou(id))
            {
                return this;
            }
            else if(_inventory.HasItem(id)) {
                return _inventory.Fetch(id);
            }
            //Lastly, checking if the item can be located where they are ( _location, locate "gem")
            else if (MyLocation != null && MyLocation.Inventory.HasItem(id))
            {
                return MyLocation.Inventory.Fetch(id);
            }
            else 
                return null;
        }

        public void Put(Item item)
        {
            _inventory.Put(item);
        }

        public GameObject Take(string id)
        {
            return _inventory.Take(id);
        }

        public override string FullDescription
        {
            get 
            {
                //Return a description on yourself and your inventory
                return $"You are {this.Name}, {base.FullDescription}\nYou are carrying: {_inventory.ItemList}";
            }
        }
        public Inventory Inventory
        {
            get {
                return _inventory; 
            }
        }

        // Players are always in location
        public Locations MyLocation
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}
