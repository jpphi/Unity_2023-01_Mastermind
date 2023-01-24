# Unity_2023-01_Mastermind

Janvier 2023

Projet MasterMind.

Fonctions:
- Mode triche: dévoile le code, accessible par la touche 't'
- Flèches pour se déplacer dans la scène.
- Choix d'une couleur par une touche:
    R: Red
    Y: Yellow
    G: Green
    P: Purple
    B: Blue
    C: Cyan

- Quand une couleur est choisi, elle s'affiche au centre de l'écran
- Quand toute les boules sont chosies (organisé en tableau circulaire) La touche "Entrée" ou "Validation" permet de lancer la vérification de la proposition.
- La ligne est générée y compris les marques indiquant qu'une boule est bien placé ou mal placé (voir règle du jeux)
- L'IA permet de tenter de casser le code (Algorithme)

Release en .exe est disponible Ne pas oublier de telechager le .dll et décompresser le .zip
Le .exe le .dll et les 2 dossiers provenant du .zip doivent être au même niveau dans l'arborescence des dossiers.

Amélioration:
- L'IA pourrait aller au delà des 75% actuel:
    Inclure dans le code de la classe IA un ajout des solution déjà tester et les éliminer de l'univers
    Eliminer toutes les solutions dans l'univers tenant compte du placement des pions (
- Un bouton Nouvelle partie serait un plus !
- Une animation fin de partie reste à faire.

Etude des marques possibles:

---- : Aucune des couleurs proposées n'est dans la combinaison
---B
--BB
-BBB
BBBB : Toutes les couleurs sont découvertes mais aucune n'est à la bonne place

---N
--NN
-NNN
NNNN : Partie gagné, toutes les couleurs sont découvertes et sont à leur place

--BN
-BBN
BBBN
-BNN
BBNN
BNNN

Les marques sont ordonnées (N toujours avant B). Les combinaisons de type XXNB sont impossibles.


