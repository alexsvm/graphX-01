using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsm
{
    using Link = Dictionary<string, string>;

    /// <summary>
    /// class Parser here....
    /// </summary>
    class Parser
    {
        private HashSet<string> states;
        private Dictionary<string, Link> links;
        private string initialState;
        private string finalState;

        public Parser(HashSet<string> States, Dictionary<string, Link> Links, string InitialState, string FinalState) {
            states = States;
            links = Links;
            initialState = InitialState;
            finalState = FinalState;
        }

        public bool ParseString(string Str) {
            string currState = initialState;
            foreach (char C in Str) {
                if (C == 'x') continue;
                if (links[currState].ContainsKey(C.ToString()))
                    currState = links[currState][C.ToString()];
                else
                    return false;
             }
            return currState == finalState ? true : false;
        }
    }

    /// <summary>
    /// ///////
    /// </summary>
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
            Link = new Dictionary<string, State>();
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

