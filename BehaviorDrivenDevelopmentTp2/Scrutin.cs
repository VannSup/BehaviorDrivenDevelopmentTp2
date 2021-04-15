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
                candidat => {
                    result += candidat.NbVote;
                }
            );
            return result;
        }

        public Candidat GetCandidatWithMaxVote()
        {
            int max = 0;
            Candidat result = null;
            Candidats.ForEach(c =>
                {
                    if(c.NbVote > max)
                    {
                        result = c;
                        max = c.NbVote;
                    }
                }
            );
            return result;

        }

        #region Pourcentage
        public string GetPourcentageResult()
        {
            string result = "";

            Candidats.ForEach(
                candidat => {
                    result += GetPourcentageResultByCandidat(candidat.Name) + '\n';
                }
            );
            return result;
        }

        public string GetPourcentageResultByCandidat(string name)
        {
            float result = ((float)FindByName(name).NbVote / (float)GetNbparticipant())*100;
            return $"{name} : {result}";
        }
        #endregion

        #region Nombre de vote
        public string GetNbVoteResult()
        {
            string result = "";

            Candidats.ForEach(
                candidat => {
                    result += GetNbVoteResultByCandidat(candidat.Name) + '\n';
                }
            );
            return result;
        }

        public string GetNbVoteResultByCandidat(string name)
        {
            return $"{name} : {FindByName(name).NbVote}";
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
    }
}
