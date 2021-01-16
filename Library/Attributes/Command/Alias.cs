using System.Collections.Generic;

namespace irsu.Library.Attributes.Command
{
    public class Alias : System.Attribute
    {
        public string[] Aliases { get; }

        public Alias(string[] aliases)
        {
            Aliases = aliases;
        }
    }
}
