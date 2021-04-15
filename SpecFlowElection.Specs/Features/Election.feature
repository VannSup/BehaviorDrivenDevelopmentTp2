Feature: SpecFlowElection

# Répond a : Pour obtenir un vainqueur, le scrutin doit être clôturé
Scenario: Pas de vainqueur si le scrutin n'est pas clôturé
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	And Les differents vote par candidats tour 1
	| Name       | VoteNumbers |
	| candidat1  | 100         |
	| candidat2  | 300         |
	| Vote Blanc | 1           |
	When Recherche du vainqueur
	Then Le résultat devrait être Le scrutin n'est pas fini, pas de vainqueur.

# Répond a : Si un candidat obtient > 50% des voix, il est déclaré vainqueur
Scenario: Un vainqueur si le scrutin est clôturé est qu'un candidat a + 50% des voix
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	| candidat3 |
	And Les differents vote par candidats tour 1
	| Name       | VoteNumbers |
	| candidat1  | 150         |
	| candidat2  | 202         |
	| candidat3  | 50          |
	| Vote Blanc | 1           |
	When Cloture scrutin 1
	When Recherche du vainqueur
	Then Le résultat devrait être candidat2