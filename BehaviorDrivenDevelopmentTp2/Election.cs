using System.Collections.Generic;

namespace BehaviorDrivenDevelopmentTp2
{
    public class Election
    {
        private List<Scrutin> Scrutins { get; set; }

        public List<string> CandidatsName { get; set; }

        public int CurrentScrutinIndex { get; set; }

        public Election()
        {
            CandidatsName = new List<string>();

            Scrutins = new List<Scrutin>() {
                new Scrutin()
            };
            CurrentScrutinIndex = 0;
        }

        public string StartScrutin()
        {
            if (Scrutins.Count == 2)
            {
                if (Scrutins[1]?.GetStatus() == true)
                    return "Pas plus de deux tours possible";
            }

            Scrutins[CurrentScrutinIndex].Start();

            CandidatsName.ForEach(candidatName =>
            {
                Scrutins[CurrentScrutinIndex].Candidats.Add(new Candidat(candidatName));
            });

            return "";
        }

        public string StopScrutin()
        {
            Scrutins[CurrentScrutinIndex].Stop();
            string resultOfScrutin = GetWin();
            if (resultOfScrutin == "Pas de vainqueur" && CurrentScrutinIndex == 0)
            {
                var candidatsOrderDescendingByNbVote = Scrutins[CurrentScrutinIndex].GetCandidatsOrderDescendingByNbVote();

                var allCandidatWithSameVote = Scrutins[CurrentScrutinIndex].FindAllCandidatsByNbVote(candidatsOrderDescendingByNbVote[1].NbVote);

                //On modifie la liste des candidat pour le deuxieme tours
                CandidatsName = new List<string>() {
                    candidatsOrderDescendingByNbVote[0].Name
                };
                allCandidatWithSameVote.ForEach(c => 
                { 
                    CandidatsName.Add(c.Name); 
                });

                CurrentScrutinIndex++;

                Scrutins.Add(new Scrutin());
            }

            return resultOfScrutin;
        }


        #region Get
        public string GetWin()
        {
            if (Scrutins[CurrentScrutinIndex].GetStatus() == true)
            {

                var candidatsOrderDescendingByNbVote = Scrutins[CurrentScrutinIndex].GetCandidatsOrderDescendingByNbVote();

                //On verifie si le candidat avec le meileur scores a + de 50% de votes
                if ((candidatsOrderDescendingByNbVote[0].NbVote) > (Scrutins[CurrentScrutinIndex].GetNbparticipant() / 2))
                    return candidatsOrderDescendingByNbVote[0].Name;

                // Si on est au deuxieme tour alors le premiere est vainqueur
                else if (CurrentScrutinIndex == 1)
                    if (candidatsOrderDescendingByNbVote[0].NbVote == candidatsOrderDescendingByNbVote[1].NbVote)
                        return "Pas de vainqueur";
                    else
                        return candidatsOrderDescendingByNbVote[0].Name;

                else
                    return "Pas de vainqueur";
            }
            else
                return "Le scrutin n'est pas fini, pas de vainqueur.";

        }

        public string GetInfoByCandidatNameAndScrutinIndex(string name, int index)
        {
            string result = "";

            result += Scrutins[index].GetNbVoteResultByCandidat(name) + " ";
            result += Scrutins[index].GetPourcentageResultByCandidat(name);

            return result;
        }
        #endregion

        #region Vote
        public string VoteFor(string name = Constants.VOTE_BLANC)
        {
            switch (Scrutins[CurrentScrutinIndex].GetStatus())
            {
                // En cours
                case false:
                    Scrutins[CurrentScrutinIndex].VoteFor(name);
                    return $"Vote pour {name} au tour {CurrentScrutinIndex}";

                // Terminée
                case true:
                    return "Le scrutin est cloturée";

                // Pas commencé
                default:
                    return "Les élections n'ont pas commencer";
            }
        }
        #endregion
    }
}
