using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsm
{
    class State : IEquatable<State>
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return "ID: " + Id + "   Name: " + Name;
        }

        public State():this(0, "")
        {
        }

        public State(int id, string name = "")
        {
            Id = id;
            Name = name;
            Link = new Dictionary<char, State>();
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            State objAsState = obj as State;
            if (objAsState == null) return false;
            else return Equals(objAsState);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public bool Equals(State other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }
        
        public static bool operator ==(State a, State b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return (a.Name.Equals(b.Name));
        }

        public static bool operator !=(State a, State b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return false;
            }
            if (((object)a == null) || ((object)b == null))
            {
                return true;
            }
            return (!a.Name.Equals(b.Name));
        }

        public Dictionary<string, State> Link;
    }

}

    class Link
    {

    }

}
