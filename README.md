# ford_algorithm

Résolution du problème de cheminement optimal avec l'algorithme de Ford (cas du plus long chemin) 

Le programme a été fait avec Visual Studio 2012.

Fondamentalement, il se base sur une structure permettant de récolter des informations sur un arc
(prédecesseur, successeur et la pondération ou étiquette de l'arc). En précisant le nombre de sommmet
du graphe, le programme génère automatiquement les noms des sommets (0, 1, 2, 3, etc.). Donc il est 
impossible de nommer les sommets, il faut une correspondance entre le nombre généré et le sommet 
correspondant dans votre graphe.
Ici, nous résolvons le problème qui est de trouver le plus long chemin, mais le programme peut être 
modifié pour trouver le plus court chemin en changeant quelques signes de supériorité et en remplancant 
le MinValue par MaxValue.
