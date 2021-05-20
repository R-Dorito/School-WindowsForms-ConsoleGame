using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Iteration
{

    public class Locations : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        public Inventory Inventory { get => _inventory;  }
        public List<Path> Paths { get; set; }

        public Locations(string name, string description): base(new string[] { "location", "me", "inventory", "room" }, name, description)
        {
            _inventory = new Inventory();
            Paths = new List<Path>();
        }

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }
            foreach(Path p in Paths)
            {
                if (p.AreYou(id))
                {
                    return p;
                }
            }

            if (_inventory.HasItem(id))
            {
                return _inventory.Fetch(id);
            }

            else
                return null;
        }

        public void MakePathConnection(Path path)
        {
            // if there is a door with the same name (eg: two north doors) 
            if (Paths.FirstOrDefault(x => x.FirstId == path.FirstId) == null)
            {
                Paths.Add(path);
            }
        }

        public string FindExit()
        {
            string exitInfo = "There are possible locations ";

            foreach (Path path in Paths)
            {
                exitInfo += $"{path.FirstId}";

                if (path.Equals(Paths.Last()))
                {
                    exitInfo += ".";
                }
                else if (path.Equals(Paths[Paths.Count - 2]))
                {
                    exitInfo += ", and ";
                }
                else
                {
                    exitInfo += ", ";
                }
            }
            return exitInfo;
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
                return $"You are in the {this.Name}\n" + 
                    $"{FindExit()}\n"+
                    $"This is {base.FullDescription}.\n" + 
                    $"In this room you can see: {Inventory.ItemList}";
            }
        }
    }
}
