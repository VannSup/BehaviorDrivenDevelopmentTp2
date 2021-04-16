Feature: SpecFlowElection

# Répond a : Pour obtenir un vainqueur, le scrutin doit être clôturé
Scenario: Pas de vainqueur si le scrutin n'est pas clôturé
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	When Ouvrir scrutin
	Given Les differents vote par candidats
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
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat1  | 150         |
	| candidat2  | 202         |
	| candidat3  | 50          |
	| Vote Blanc | 1           |
	When Cloture scrutin
	When Recherche du vainqueur
	Then Le résultat devrait être candidat2

# Répond a : Si aucun candidat n'a plus de 50%, alors on garde les 2 candidats
# correspondants aux meilleurs pourcentages et il y aura un deuxième tour
# de scrutin
# +
# Sur le dernier tour de scrutin, le vainqueur est le candidat ayant le
# pourcentage de vote le plus élevé
# +
# Si aucun candidat n'a plus de 50%, alors on garde les 2 candidats
# correspondants aux meilleurs pourcentages et il y aura un deuxième tour
# de scrutin
Scenario: Un vainqueur au deuxieme tour
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	| candidat3 |
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat1  | 30          |
	| candidat2  | 40          |
	| candidat3  | 50          |
	| Vote Blanc | 10          |
	When Cloture scrutin
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat2  | 40          |
	| candidat3  | 50          |
	| Vote Blanc | 10          |
	When Cloture scrutin
	When Recherche du vainqueur
	Then Le résultat devrait être candidat3

# On veut pouvoir afficher le nombre de votes pour chaque candidat et le
# pourcentage correspondant à la clôture du scrutin.
Scenario: Affichage des données d'un scrutin en cours puis fini
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat1  | 100         |
	| candidat2  | 300         |
	| Vote Blanc | 1           |
	When Consultation des scrutin
	| Name      | TourId |
	| candidat2 | 0      |
	When Cloture scrutin
	When Consultation des scrutin
	| Name      | TourId |
	| candidat2 | 0      |
	Then Le résultat devrait être Pas encore clôturé : candidat2 : 300 voix. Pas encore clôturé : candidat2 : 74.812965%.candidat2 : 300 voix. candidat2 : 74.812965%.

# Si on a une égalité sur un dernier tour, on ne peut pas déterminer de vainqueur
Scenario: Egalité sur le dernier tour
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	| candidat3 |
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat1  | 30          |
	| candidat2  | 40          |
	| candidat3  | 50          |
	| Vote Blanc | 10          |
	When Cloture scrutin
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat2  | 50          |
	| candidat3  | 50          |
	| Vote Blanc | 10          |
	When Cloture scrutin
	When Recherche du vainqueur
	Then Le résultat devrait être Pas de vainqueur

# Il ne peut y avoir que deux tours de scrutins maximums
Scenario: Essayer de démarrer un troisieme tour
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	| candidat3 |
	When Ouvrir scrutin
	When Cloture scrutin
	When Ouvrir scrutin
	When Cloture scrutin
	When Ouvrir scrutin
	Then Le résultat devrait être Pas plus de deux tours possible

# Gestion du vote blanc
Scenario: Gestion des votes blancs
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	| candidat3 |
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat1  | 30          |
	| candidat2  | 40          |
	| candidat3  | 50          |
	| Vote Blanc | 10          |
	When Cloture scrutin
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat2  | 40          |
	| candidat3  | 50          |
	| Vote Blanc | 100          |
	When Cloture scrutin
	When Recherche du vainqueur
	Then Le résultat devrait être Vote Blanc

# Gestion des égalités sur le 2ème et 3ème candidat sur un premier tour
Scenario: Un vainqueur au deuxieme tour du candidat trois qui était en égalité avec le candidat deux au tours 2
	Given Les differents candidats
	| Name      |
	| candidat1 |
	| candidat2 |
	| candidat3 |
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat1  | 60          |
	| candidat2  | 50          |
	| candidat3  | 50          |
	| Vote Blanc | 10          |
	When Cloture scrutin
	When Ouvrir scrutin
	Given Les differents vote par candidats
	| Name       | VoteNumbers |
	| candidat1  | 50          |
	| candidat2  | 50          |
	| candidat3  | 100         |
	| Vote Blanc | 10          |
	When Cloture scrutin
	When Recherche du vainqueur
	Then Le résultat devrait être candidat3