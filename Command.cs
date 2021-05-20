using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Iteration
{
    public abstract class Command : IdentifiableObject
    {
        public Command(string[] ids) : base(ids) 
        {

        }
        public abstract string Execute(Player p, string[] text);
    }
}
