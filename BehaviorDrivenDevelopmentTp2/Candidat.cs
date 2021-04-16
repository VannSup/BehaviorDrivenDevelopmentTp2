using System;

namespace BehaviorDrivenDevelopmentTp2
{
    public class Candidat : IEquatable<Candidat>, IComparable<Candidat>
    {
        public string Name { get; set; }

        public int NbVote { get; set; }

        public Candidat(string name)
        {
            Name = name;
            NbVote = 0;
        }

        public void AddVote()
        {
            NbVote += 1;
        }

        #region Comparable
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Candidat objAsPart = obj as Candidat;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        // Default comparer for Part type.
        public int CompareTo(Candidat comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.NbVote.CompareTo(comparePart.NbVote);
        }
        public override int GetHashCode()
        {
            return NbVote;
        }
        public bool Equals(Candidat other)
        {
            if (other == null) return false;
            return (this.NbVote.Equals(other.NbVote));
        }
        // Should also override == and != operators.
        #endregion
    }
}
