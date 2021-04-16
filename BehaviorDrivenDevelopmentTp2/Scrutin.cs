using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorDrivenDevelopmentTp2
{
    public class Scrutin
    {
        public List<Candidat> Candidats { get; set; }

        private bool? IsFinish { get; set; }

        public Scrutin()
        {
            IsFinish = null;
            Candidats = new List<Candidat>
            {
                // Pour les votes blanc 
                new Candidat(Constants.VOTE_BLANC)
            };
        }

        public void Start()
        {
            IsFinish = false;
        }

        public void Stop()
        {
            IsFinish = true;
        }

        /// <summary>
        ///     null = pas commencé,
        ///     false = en cours,
        ///     true = terminée
        /// </summary>
        /// <returns></returns>
        public bool? GetStatus()
        {
            return IsFinish;
        }

        public int GetNbparticipant()
        {
            int result = 0;

            Candidats.ForEach(
                candidat =>
                {
                    result += candidat.NbVote;
                }
            );
            return result;
        }

        public List<Candidat> GetCandidatsOrderDescendingByNbVote()
        {
            //On trie du plus petit nombre de vote au plus grand
            Candidats.Sort();
            //On retourne la liste pour avoir en premiers les meilleur
            Candidats.Reverse();
            return Candidats;
        }

        #region Pourcentage
        public string GetPourcentageResult()
        {
            string result = "";

            if (IsFinish != true)
                result += "Pas encore clôturé : ";

            Candidats.ForEach(candidat =>
            {
                float pourcentage = ((float)candidat.NbVote / (float)GetNbparticipant()) * 100;
                result += $"    {candidat.Name} : {pourcentage}%.";
            });

            return result;
        }

        public string GetPourcentageResultByCandidat(string name)
        {
            string result = "";

            if (IsFinish != true)
                result += "Pas encore clôturé : ";

            float pourcentage = ((float)FindByName(name).NbVote / (float)GetNbparticipant()) * 100;
            result += $"{name} : {pourcentage}%.";
            return result;
        }
        #endregion

        #region Nombre de vote
        public string GetNbVoteResult()
        {
            string result = "";

            if (IsFinish != true)
                result += "Pas encore clôturé : ";

            Candidats.ForEach(candidat =>
            {
                result += $"{candidat.Name} : {candidat.NbVote} voix.";
            });

            return result;
        }

        public string GetNbVoteResultByCandidat(string name)
        {
            string result = "";

            if (IsFinish != true)
                result += "Pas encore clôturé : ";

            result += $"{name} : {FindByName(name).NbVote} voix.";

            return result;
        }
        #endregion

        public void VoteFor(string name)
        {
            FindByName(name).AddVote();
        }

        public Candidat FindByName(string name)
        {
            return Candidats.Find(candidat => candidat.Name == name);
        }

        public List<Candidat> FindAllCandidatsByNbVote(int NbVote)
        {
            return Candidats.FindAll(candidat => candidat.NbVote == NbVote);
        }
    }
}
