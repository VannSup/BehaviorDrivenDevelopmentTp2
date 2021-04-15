using System;
using BehaviorDrivenDevelopmentTp2;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace SpecFlowElection.Specs.Steps
{
    [Binding]
    public sealed class ElectionStepDefinition
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        private readonly Election _election = new Election();

        private string _result;

        public ElectionStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        #region Given
        [Given("Les differents candidats")]
        public void GivenLesDifferentsCandidats(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                _election.Candidats.Add(new Candidat(row["Name"]));
            }
        }

        [Given("Les differents vote par candidats tour 1")]
        public void GivenLesDifferentsVoteParCandidatsUn(Table table)
        {
            _election.StartScrutinUn();
            foreach (TableRow row in table.Rows)
            {
                int nbVote = int.Parse(row["VoteNumbers"]);
                string name = row["Name"];
                for (int i = 0; i < nbVote; i++)
                {
                    _election.VoteFor(name);
                }
            }
        }

        [Given("Les differents vote par candidats tour 2")]
        public void GivenLesDifferentsVoteParCandidatsDeux(Table table)
        {
            _election.StartScrutinUn();
            foreach (TableRow row in table.Rows)
            {
                int nbVote = int.Parse(row["VoteNumbers"]);
                string name = row["Name"];
                for (int i = 0; i < nbVote; i++)
                {
                    _election.VoteFor(name);
                }
            }
        }
        #endregion

        #region When
        [When("Cloture scrutin 1")]
        public void WhenClotureScrutinUn()
        {
            _election.StopScrutinUn();
        }

        [When("Cloture scrutin 2")]
        public void WhenClotureScrutinDeux()
        {
            _election.StopScrutinDeux();
        }

        [When("Recherche du vainqueur")]
        public void WhenRechercheDuVainqueur()
        {
            _result = _election.GetWin();
        }
        #endregion

        #region Then
        [Then("Le résultat devrait être (.*)")]
        public void ThenLeResultatDevraitEtre(string result)
        {
            _result.Should().Be(result);
        }
        #endregion

    }
}
