using System;
using System.Collections.Generic;

namespace BehaviorDrivenDevelopmentTp2
{
    public class Election
    {
        private readonly Scrutin _scrutin1 = new Scrutin();

        private readonly Scrutin _scrutin2 = new Scrutin();

        public List<Candidat> Candidats { get; set; }

        public Election()
        {
            Candidats = new List<Candidat>();
        }

        public void StartScrutinUn()
        {
            _scrutin1.Start();
            _scrutin1.Candidats.AddRange(Candidats);
        }

        public void StartScrutinDeux()
        {
            _scrutin2.Start();
            _scrutin2.Candidats.AddRange(Candidats);
        }

        public void StopScrutinUn()
        {
            _scrutin1.Stop();
            if (GetWin() == "Pas de vainqueur")
                StartScrutinDeux();
        }

        public void StopScrutinDeux()
        {
            _scrutin2.Stop();
        }

        #region Get
        public string GetWin()
        {
            if (_scrutin1.GetStatus() == true)
            {
                 var firstCandidat = _scrutin1.GetCandidatWithMaxVote();
                //On verifie si le candidat avec le meileur scores a + de 50% de votes
                if ((firstCandidat.NbVote) > (_scrutin1.GetNbparticipant() / 2))
                    return firstCandidat.Name;
                else
                    return "Pas de vainqueur";
            }
            else 
            {
                return "Le scrutin n'est pas fini, pas de vainqueur.";
            }
                
        }
        #endregion

        #region Vote
        public string VoteFor(string name = Constants.VOTE_BLANC)
        {
            switch (_scrutin1.GetStatus())
            {
                // En cours
                case false:
                    _scrutin1.VoteFor(name);
                    return $"Vote pour {name} au tour 1";

                // Terminée
                case true:
                    return VoteForSecondTours(name);

                // Pas commencé
                default:
                    return "Les élections n'ont pas commencer";
            }
        }
        private string VoteForSecondTours(string name)
        {
            switch (_scrutin2.GetStatus())
            {
                // En cours
                case false:
                    _scrutin2.VoteFor(name);
                    return $"Vote pour {name} au tour 2";

                // Terminée
                case true:
                    return "Le scrutin est clôturé";

                // Pas commencé
                default:
                    return "Le deuxième tour des élections n'a pas commencé";
            }
        }
        #endregion
    }
}
