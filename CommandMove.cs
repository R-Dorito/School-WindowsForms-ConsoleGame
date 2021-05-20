using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Iteration
{
    public class CommandMove: Command
    {
        public CommandMove() : base(new string[] { "move" })
        {

        }

        public override string Execute(Player p, string[] text)
        {
            //Requirement: Move south
            //    move south
            //    [0] [1]
            Path path;

            if (!(text.Length == 2))
            {
                return $"I don't know how to move in that way";
            }

            path = FindPath(p, text[1]);
            if (path == null)
            {
                return "That is not an available path";
            }
            else
            {
                return UpdateLocation(p, path);
            }

        }

        private Path FindPath(Player p, string id)
        {
            foreach(Path path in p.MyLocation.Paths)
            {
                if (path.FirstId.Equals(id))
                {
                    return path;
                }
            }
            return null;
        }
        private string UpdateLocation(Player p, Path path)
        {
            p.MyLocation = path.Destination;

            //path.CurrentLocation.AddPath(path);
            return $"You have moved {path.FirstId} to {p.MyLocation.Name}";
        }
    }
}
