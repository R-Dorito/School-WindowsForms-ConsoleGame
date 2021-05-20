using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class CommandPut: Command
    {
        public CommandPut() : base(new string[] { "put", "drop" })
        {

        }

        public override string Execute(Player p, string[] text)
        {
            string containerId = "";
            string itemId = "";
            IHaveInventory container;

            itemId = text[1];
            switch (text.Length)
            {
                case (2):
                    container = p.MyLocation;
                    break;
                case (4):
                    if (text[2] != "in")
                    {
                        return "What do you want to look in?";
                    }
                    if(text[3] == "room")
                    {
                        container = p.MyLocation;
                    }
                    else
                    {
                        containerId = text[3];
                        container = FetchContainer(p, containerId);
                    }
                    break;
                default:
                    return "I don't understand the command";
            }

            if (container == null)
            {
                return "I can't find " + containerId;
            }

            return PutItemIn(p, itemId, container);
        }

        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        private string PutItemIn(Player p, string thingId, IHaveInventory container)
        {
            // fetch item from your inventory or bag
            Item selectedItem = p.Locate(thingId) as Item;
            
            if (selectedItem == null) 
            { 
                return $"You do not have a {thingId}"; 
            }
            else
            {
                if (selectedItem == container as Item)
                {
                    return "You can't put an item into itself";
                }
                if (p.Inventory.HasItem(thingId))
                {
                    container.Put(p.Take(thingId) as Item);
                }
                if (p.MyLocation.Inventory.HasItem(thingId))
                {
                    container.Put(p.MyLocation.Take(thingId) as Item);
                }
                return $"You have put {thingId} in the {container.Name}";
            }
        }
    }
}
