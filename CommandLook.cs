using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class CommandLook : Command
    {
        public CommandLook(): base(new string[] { "look" })
        {
            
        }

        public override string Execute( Player p, string[] text)
        {
            //"Look at pen in bag"
            //  [0] [1][2] [3][4]
            string containerID = "";
            string itemId = "";
            IHaveInventory container;

            if (text.Length == 1 && text[0] == "look")
            {
                return $"{p.MyLocation.FullDescription}";
            }

            if (!(text.Length == 3 ) && !(text.Length == 5))
            {
                return $"I don’t know how to look like that";
            }

            if (!text[1].Equals("at"))
            {
                return $"What do you want to look at?";
            }

            itemId = text[2];
            container = p;

            if (text.Length == 5)
            {
                if (!text[3].Equals("in"))
                {
                    return $"What do you want to look in?";
                }
                containerID = text[4];
                container = FetchContainer(p, containerID);
                if (container == null) return $"I cannot find the {text[4].ToLower()}";
            }
            return LookAtIn(itemId, container);
        }

        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }
        private string LookAtIn(string thingId, IHaveInventory container)
        {
            GameObject foundedObject = container.Locate(thingId);
            if (foundedObject == null) return $"I cannot find the {thingId}";
            else 
                return foundedObject.FullDescription;
        }
    }
}
