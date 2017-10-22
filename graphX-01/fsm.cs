using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fsm
{
    using Link = Dictionary<string, string>;

    /// <summary>
    /// Класс, реализующий конечный автомат для распознования цепочки символов
    /// </summary>
    class Parser
    {
        private HashSet<string> states;         // Множество состояний
        private Dictionary<string, Link> links; // Словарь переходов из состояния ri в состояние rj по символу xk
        private string initialState;            // ИД начального состояния
        private string finalState;              // ИД конечного состояния

        public Parser(HashSet<string> States, Dictionary<string, Link> Links, string InitialState, string FinalState) {
            states = States;
            links = Links;
            initialState = InitialState;
            finalState = FinalState;
        }

        /// <summary>
        /// ParseString - функция, проверяющая строку Str на соответсвие правилам
        /// </summary>
        /// <param name="Str"> Строка для распознования ... </param>
        /// <returns> Если строка соответствует правилам, то true, иначе false </returns>
        public bool ParseString(string Str) {
            string currState = initialState;
            foreach (char C in Str) { // Перебираем все символы в строке
                if (links[currState].ContainsKey(C.ToString())) // Если из текущего состояния по ткущему символу есть 
                    currState = links[currState][C.ToString()]; // переход в другое состояние, то переходим туда,
                else                                            // если нет, то цепочка не соответствует правилам:
                    return false;                               // выходим в результатом false
             }
            // Если после завершения перебора символов мы оказались в конечном состояниия, то цепочка верная,
            return currState == finalState ? true : false;      // если нет - цепочка не соответствует правилам
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

