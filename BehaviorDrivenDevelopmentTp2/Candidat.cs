using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorDrivenDevelopmentTp2
{
    public class Candidat
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
    }
}
