using System;
using System.Collections.Generic;
using System.Text;

namespace Iteration
{
    public class Path : GameObject
    {
        public Locations Destination { get; private set; }
        public Path(string[] id, string name, string desc, Locations des) : base(id, name, desc)
        {
            Destination = des;
        }
        public override string FullDescription
        {
            get { return $"You Travel through {base.FullDescription}, " +
                    $"and exit from the {this.FirstId}."; }
        }
    }
}
