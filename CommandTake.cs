using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class CommandTake : Command
    {
        public CommandTake() : base(new string[] { "take" })
        {

        }

        public override string Execute(Player p, string[] text)
        {
            //pickup paper
            //pickup paper from room

            //pickup [thing]
            //pickup [thing] from [container]
            //take paper
            //take paper from bag

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
                    if (text[2] != "from")
                    {
                        return "Where do you want to look from?";
                    }
                    if (text[3] == "room")
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

            return TakeItem(p, itemId, container);
        }
        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        private string TakeItem(Player p, string thingId, IHaveInventory container)
        {
            // fetch item from your inventory or bag
            Item selectedItem = container.Locate(thingId) as Item;

            if (selectedItem == null)
            {
                return $"I cannot find the {thingId}";
            }
            else
            {
                if (selectedItem == container as Item)
                {
                    return "You can't put an item into itself";
                }
                p.Put(container.Take(thingId) as Item);
                return $"You have taken {thingId} from the {container.Name}";
            }
        }
    }
}
