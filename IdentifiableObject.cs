using System.Collections.Generic;
using System.Linq;

namespace Iteration
{
    public class IdentifiableObject
    {
        private List<string> _identifiers = new List<string>();

        public IdentifiableObject(string[] indents)
        {
            foreach (string s in indents)
            {
                AddIdentifier(s);
            }
        }
        public bool AreYou(string id)
        {
            return _identifiers.Contains(id.ToLower());
        }
        public string FirstId
        {
            get { return _identifiers.FirstOrDefault(); }
        }
        public void AddIdentifier(string id)
        {
            _identifiers.Add(id.ToLower());
        }
    }
}
