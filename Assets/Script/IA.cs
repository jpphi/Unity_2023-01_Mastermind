using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IA : MonoBehaviour
{
    // Start is called before the first frame update
 
    public delegate void IADecode(int score); //int score
    public event IADecode OnIADecode;
    [SerializeField] public Code codeSecret;
    [SerializeField] public Ligne ligneSecret;
    [SerializeField] public GestionPartie gp;
    //public GestionPartie gp;

    //public GestionPartie gp = new GestionPartie();

    public static List<int[]> univers= new List<int[]>();


    private void Start()
    {
        // Initialisation de l'univers des possibles

        OnIADecode += new IADecode(gp.recup);

    }
    public void lancerIA()
    {

        //int indiceUniversTmp = 0;

        int[] proposition = new int[Globales.NB_PION_LIGNE];
        List<int[]> tableauDesPropositions = new List<int[]>();
        int[] reponse = new int[Globales.NB_PION_LIGNE];
        int[] rechReponse = new int[Globales.NB_PION_LIGNE];
        int[,] couleurDisponible = new int[,] { { Globales.BLUE_COLOR, -1 }, {Globales.CYAN_COLOR, -1 },
            { Globales.GREEN_COLOR, -1 }, {Globales.PURPLE_COLOR, -1}, { Globales.RED_COLOR, -1}, { Globales.YELLOW_COLOR, -1} };

        int[] numCouleurTrouve = new int[Globales.NB_PION_LIGNE];

        List<bool>[] placement = new List<bool>[Globales.NB_COULEURS];

        bool bouclePrincipale = true;
        int iu = 1;

        for (int i = 0; i < Globales.NB_COULEURS; i++)
        {
            placement[i] = new List<bool> { true, true, true, true };
        }
        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            numCouleurTrouve[i] = -1;
        }

        int numCouleur = couleurDisponible[0, 0]; //, numCouleurPrecedente= -1, numCouleurPrecedentePrecedente = -1, numCouleurPrecedentePrecedentePrecedente = -1;
        int nbValeurTrouve = 0;
        int N, B, nbPasse = 1;

        //Debug.Log("<Decode.lancerIA> ");

        // 1ère proposition
        proposition = construitProposition(couleurDisponible, placement, "Première initialisation");

        while (true)
        {
            construitLigne(proposition);
            reponse = ligneSecret.CheckResult();

            // Calcul du nombre de pions bien placé et mal placé
            N = B = 0;
            for (int i= 0; i< reponse.Length; i++)
            {
                if (reponse[i]== Globales.BLACK_COLOR)
                {
                    N++;
                }
                else if(reponse[i] == Globales.WHITE_COLOR)
                {
                    B++;
                }

            }
            //Debug.Log("<IA.LancerIA> : N, B : " + N + ", " + B);

            if (N == Globales.NB_PION_LIGNE) // 4 N
            {
                Debug.Log("<IA.LancerIA> :Code cassé ! Nombre de passe: " + nbPasse);

                break;
            }

            else if ( (N + B == Globales.NB_PION_LIGNE) && (N != 0) )
            {
                Debug.Log("<IA.LancerIA> : Toutes les couleurs sont déterminées : passe numéro " + nbPasse);

                // On récupère la proposition faite pour l'éliminer ensuite
                //tableauDesPropositions.Add(proposition);

                // on ordonne les propositions par ordre croissant pour mieux gérer les doublons
                //proposition = ordonneProposition(proposition);
                // On génère l'univers des possibles
                if(bouclePrincipale) Permute(proposition, 0, 3);

                bouclePrincipale = false;

                // On retire la proposition précédente de l'univers
                //univers.Remove(proposition);

                //affiche(univers);

                //int nligne = 0;
                //foreach(var ligne in tableauDesPropositions)
                //{
                //    Debug.Log("Ligne: " + nligne);
                //    for(int i= 0; i< ligne.Length; i++)
                //    {
                //        Debug.Log(" - " + ligne[i]);
                //    }
                //}
                //tableauDesPropositions.Add(proposition);

                //break;
            }


            else if ((N == 0) && (B == 4))
            {
                Debug.Log("<IA.LancerIA> : (N== 0) && (B==4). nbpasse= " + nbPasse);

 
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B +
                            ". nbValeur ne devrait pas être = 0. nbValeur= " + nbValeurTrouve);
                    break;
                }


                else if (nbValeurTrouve == 1)
                {   // Une valeur avait été trouvé elle n'est pas à la bonne place
                    //  la nouvelle valeur trouvé est en triple
                    // Mais on devrait avoir 2 valeurs bien placé !!!! 
                    
                    //nbValeurTrouve = 3;
                    //couleurDisponible[numCouleur, 1] = 2;

                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B +
                        ". nbValeur ne devrait pas être = 1. nbValeur= " + nbValeurTrouve + " On devrait avoir au mois 2 N ");
                    break;
                }



                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient été trouvé et sont à la mauvaise place
                    // 1 nouvelle valeur est trouvé et est en double. Elle est mal placé
                    //  La nouvelle valeur est unique
                    nbValeurTrouve = 4;
                    couleurDisponible[numCouleur, 1] = 2;
                    numCouleurTrouve[2] = numCouleur;
                }


                else if (nbValeurTrouve == 3)// la nouvelle valeur proposé n'est pas dans la combinaison
                {   // Deux valeurs avaient été trouvé et sont à la mauvaise place
                    // 1 nouvelle valeur est trouvé et est unique. Elle est mal placé
                    nbValeurTrouve = 4;
                    couleurDisponible[numCouleur, 1] = 1;
                    numCouleurTrouve[3] = numCouleur;
                    //Debug.Log("<IA.LancerIA> : N== 0 && B == 4 : nbValeurTrouve == 3 passe numéro " + nbPasse +
                    //            " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " +
                    //            couleurDisponible[numCouleur, 1]);
                }










                else if (nbValeurTrouve == 4)
                {   // La nouvelle valeur proposé n'est pas dans la combinaison
                    nbValeurTrouve = 4;
                    couleurDisponible[numCouleur, 1] = 0;
                    //Debug.Log("<IA.LancerIA> : N== 0 && B == 3 : nbValeurTrouve == 3 passe numéro " + nbPasse +
                    //            " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " +
                    //            couleurDisponible[numCouleur, 1]);
                }

                //placement = B4N0(numCouleurTrouve, proposition, placement);

                //affiche(numCouleurTrouve, "Num couleur trouvé ");
                //for (int i = 0; i < Globales.NB_PION_LIGNE; i++) // i< proposition.Length // nCT[i] != -1
                //{
                //    //Debug.Log(">>>>> i= " + i + "prop[i]= " + prop[i]);
                //    if (numCouleurTrouve[i]!= -1)
                //    {
                //        placement[numCouleurTrouve[i]][i] = false;
                //    }
                //    //affiche(placement,"test");
                //}

                for (int i = 0; (i < proposition.Length) && (numCouleurTrouve[i] != -1); i++)
                {
                    placement[proposition[i]][i] = false;
                    Debug.Log("-------> i: " + i + "numCouleurTrouve[i] = " + numCouleurTrouve[i] + " proposition[i]= " + 
                        proposition[i]);
                }






                // Toute les couleurs ayant été trouvé, on met à 0 les valeur n'ayant pas été testées
                for (int i= numCouleur+1; i < Globales.NB_COULEURS; i++)
                {
                    couleurDisponible[i, 1] = 0;
                }


            }

            else if ((N == 0) && (B == 0)) // Aucune couleur ,n'a été trouvé
            {   
                couleurDisponible[numCouleur, 1] = 0;
                //Debug.Log("<IA.LancerIA> : N== 0 La couleur proposé n'est pas dans le code : passe numéro " + nbPasse +
                //    " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);

            }

            //-------- Zone 1: N1B0 N0B1

            else if ( (N == 1) && (B == 0) ) // Les informations de position sont traité
            {
                if (nbValeurTrouve == 0)
                { // La couleur existe dans la combinaison et est unique
                    nbValeurTrouve = 1;
                    couleurDisponible[numCouleur, 1] = 1;

                    // On ne peut pas déterminé la place de la couleur
                    numCouleurTrouve[0] = numCouleur;

                    //Debug.Log("<IA.LancerIA> : (N == 1) && (B == 0) nbValeurTrouve == 0: passe numéro " + nbPasse +
                    //   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1] +
                    //   "  numCouleurTrouve[0]" + numCouleurTrouve[0]);
                }
                else // la nouvelle valeur proposé n'est pas dans la combinaison
                {
                    couleurDisponible[numCouleur, 1] = 0;
                    //placement[numCouleurTrouve[0]][0] = new List<bool> { true, false, false, false };
                    //Debug.Log("<IA.LancerIA> : (N == 1) && (B == 0) nbValeurTrouve != 0: La nouvelle valeur n'est pas " +
                    //    "dans la combinaison. La valeur précédemment trouvé est à la bonne place. passe numéro= " + nbPasse +
                    //   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
            }

            else if ((N == 0) && (B == 1)) // Les informations de position sont traité
            {
                // Debug.Log("----> EN COURS DE DEV (N == 0) && (B == 1)");
                //Debug.Log("< IA.LancerIA > : N == 0 && B == 1 La couleur proposé n'est pas dans le code : passe numéro " + nbPasse +
                //    " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1] +
                //    " nbValeurTrouve (doit être = 1) = " + nbValeurTrouve);
                // nbValeurTrouve ne doit pas avoir une valeur différente de 1
                if (nbValeurTrouve == 1)
                {
                    couleurDisponible[numCouleur, 1] = 0;

                    placement[numCouleurTrouve[0]][0] = false; // new List<bool> { false, true, true, true };
                    //placement[numCouleurTrouve[0]][0] = false; //new List<bool> { false, true, true, true };
                    //Debug.Log("Couleur " + numCouleurTrouve[0] + " n'est pas à la bonne place !" +
                    //    "placement[numCouleurTrouve[0]][0]" + placement[numCouleurTrouve[0]][0]);

                }
                else
                {
                    Debug.Log("< IA.LancerIA > : N == 0 && B == 1 ERREUR nbValeurTrouve != 1!!!! passe numéro " + nbPasse +
                        " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1] +
                        " nbValeurTrouve (doit être = 1) = " + nbValeurTrouve);

                    break;

                }
            }

            //-------- Zone 2: N2B0 N1B1 N0B2

            // (N == 1) && (B == 1) ET  (N == 2) && (B == 0) 
            else if ((N == 1) && (B == 1)) 
            {
                //Debug.Log("<IA.LancerIA> : N== 1 && B == 1 : passe numéro " + nbPasse +
                //   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);

                // nbValeurTrouve == 0 impossible compte tenu de la construction des propositions
                if(nbValeurTrouve== 0)
                {
                    Debug.Log("< IA.LancerIA > : N == 1 && B == 1 ERREUR nbValeurTrouve != 0!!!! passe numéro " + nbPasse +
                                " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1] +
                                " nbValeurTrouve (doit être != 0) = " + nbValeurTrouve);
                    break;

                }
                if (nbValeurTrouve == 1)
                {   // Une valeur avait précédemment été trouvé. Elle n'est pas à la bonne place !
                    //placeC1[0] = false;
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 1;
                    numCouleurTrouve[1] = numCouleur;

                }
                else if (nbValeurTrouve == 2)
                {   // La nouvelle valeur proposée n'est pas dans le code
                    // 1 couleur trouvé est en bonne position, l'autre pas !
                    couleurDisponible[numCouleur, 1] = 0;

                }
            }

            else if ((N == 2) && (B == 0))
            {
                if (nbValeurTrouve == 0)
                {   // La couleur existe dans la combinaison et est double
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 2;
                    numCouleurTrouve[0] = numCouleur;
                    //Debug.Log("<IA.LancerIA> : N== 2 && B == 0 : nb_valeur= 0 passe numéro " + nbPasse +
                    //   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait été trouvé, une nouvelle vient d'être trouvé et elle est unique
                    // On ne peut rien déduire sur la couleur ayant la bonne position
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 1;
                    numCouleurTrouve[1] = numCouleur;
                    //Debug.Log("<IA.LancerIA> : N== 2 && B == 0 : nb_valeur= 1 passe numéro " + nbPasse +
                    //   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);

                }
                else
                {   
                    // nbValeurTrouve était déjà à 2 => la nouvelle valeur proposé n'est pas dans la combinaison
                    couleurDisponible[numCouleur, 1] = 0;

                    affiche(placement, "Placement N= " + N + " B= " + B + " nbValeurTrouve= " + nbValeurTrouve +
                        " numCouleurTrouve[0]= " + numCouleurTrouve[0] + " numCouleurTrouve[1]= " + numCouleurTrouve[1]);

                    // Soit 1 couleur double, soit 2 couleurs simple. Toutes les couleurs sont à la bonne place.
                    if (couleurDisponible[numCouleurTrouve[0], 1] == 2) // 1 couleur en double
                    {
                        placement[numCouleurTrouve[0]][0] = true;// new List<bool> { true, true, false, false };
                        placement[numCouleurTrouve[0]][1] = true;// new List<bool> { true, true, false, false };
                    }
                    else if (couleurDisponible[numCouleurTrouve[0], 1] == 1) // 2 couleurs simples
                    {
                        placement[numCouleurTrouve[0]][0] = true;// new List<bool> { true, false, false, false };
                        placement[numCouleurTrouve[1]][1] = true; // new List<bool> { false, true, false, false };
                    }
                    //Debug.Log("<IA.LancerIA> : N== 2 && B == 0 : nb_valeur= 2 passe numéro " + nbPasse +
                    //   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
            }

            else if ((N == 0) && (B == 2))
            {
                //Debug.Log("<IA.LancerIA> : N== 0 && B == 2 : passe numéro " + nbPasse +
                //   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B + 
                        ". nbValeur ne devrait être = 0. nbValeur= " + nbValeurTrouve);
                    break;
                    //nbValeurTrouve = 2;
                    //couleurDisponible[numCouleur, 1] = 2;
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait été trouvé elle n'est pas à la bonne place
                    //  La nouvelle valeur trouvé n'est pas à la bonne place
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 1;
                    numCouleurTrouve[1] = numCouleur;
                    //Debug.Log("<IA.LancerIA> : N== 0 && B == 2 : nbValeurTrouve == 1 passe numéro " + nbPasse +
                    //           " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + 
                    //           couleurDisponible[numCouleur, 1]);

                    placement[numCouleurTrouve[0]][0] = false;// new List<bool> { true, true, false, false };
                    placement[numCouleurTrouve[1]][1] = false;// new List<bool> { true, true, false, false };
 
                    // ------------------> tableau des places
                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient été trouvé et ne sont pas à la bonne place
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    //  La nouvelle valeur n'est pas dans la combinaison
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 0;

                    //placement[numCouleurTrouve[0]][0] = false;// new List<bool> { true, true, false, false };
                    //placement[numCouleurTrouve[1]][1] = false;// new List<bool> { true, true, false, false };

                    //int pos = 0;
                    for(int i = 0; numCouleurTrouve[i] != -1; i++) // i< proposition.Length
                    {
                        placement[proposition[i]][i] = false;
                    //        pos++;
                    }
                    //}
                    ////Debug.Log("<IA.LancerIA> : N== 0 && B == 2 : nbValeurTrouve == 2 passe numéro " + nbPasse +
                    //           " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " +
                    //           couleurDisponible[numCouleur, 1]);

// ------------------> tableau des places
                
                }
                else // la nouvelle valeur proposé n'est pas dans la combinaison
                {   // nbValeurTrouve était déjà à 2
                    couleurDisponible[numCouleur, 1] = 0;
                }
            }

            //-------- Zone 3: N3B0 N2B1 N1B2 N0B3

            // N= 3
            else if ((N == 3) && (B == 0))
            {
                //Debug.Log("<IA.LancerIA> : N== 3 && B == 0 : passe numéro " + nbPasse +
                //   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                if (nbValeurTrouve == 0)
                {   // La couleur existe dans la combinaison et est triple et bien placé
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 3;
                    numCouleurTrouve[0] = numCouleur;
                    placement[numCouleurTrouve[0]] = new List<bool> { true, true, true, false };
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait été trouvé, une nouvelle vient d'être trouvé et elle est double
                    // La première valeur trouvé est à la bonne place
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 2;
 
                    numCouleurTrouve[1] = numCouleur;
                    placement[numCouleurTrouve[0]] = new List<bool> { true, false, false, false };
                    placement[numCouleurTrouve[1]] = new List<bool> { false, true, true, false };

                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avait été trouvé, une nouvelle vient d'être trouvé et elle est unique
                    // Les 2 valeurs 
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 1;
                    numCouleurTrouve[2] = numCouleur;
                    placement[numCouleurTrouve[2]][2] = true;

                }
                else // la nouvelle valeur proposé n'est pas dans la combinaison
                {   // nbValeurTrouve était déjà à 3
                    couleurDisponible[numCouleur, 1] = 0;
                }
            }

            else if ((N == 2) && (B == 1))
            { 
                //Debug.Log("<IA.LancerIA> : N== 2 && B == 1 : passe numéro " + nbPasse +
                //    " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B +
                            ". nbValeur ne devrait pas être = 0. nbValeur= " + nbValeurTrouve);
                    break;
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait été trouvé elle n'est pas à la bonne place
                    //  la nouvelle valeur trouvé est en double
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 2;
// ------------------> tableau des places
                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient été trouvé
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    //  La nouvelle valeur est unique
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 1;

// ------------------> tableau des places
                }
                else if (nbValeurTrouve == 3)// la nouvelle valeur proposé n'est pas dans la combinaison
                {   // nbValeurTrouve était déjà à 3
                    couleurDisponible[numCouleur, 1] = 0;
                }
                else
                {
                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B +
                            ". nbValeur à une valeur anormale. nbValeur= " + nbValeurTrouve);
                    break;
                }
            }

            else if ((N == 1) && (B == 2))
            {
                //Debug.Log("<IA.LancerIA> : N== 2 && B == 1 : passe numéro " + nbPasse +
                //    " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B +
                            ". nbValeur ne devrait pas être = 0. nbValeur= " + nbValeurTrouve);
                    break;
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait été trouvé elle n'est pas à la bonne place
                    //  la nouvelle valeur trouvé est en double
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 2;
// ------------------> tableau des places
                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient été trouvé
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    //  La nouvelle valeur est unique
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 1;

                    // ------------------> tableau des places
                }
                else if (nbValeurTrouve == 3)// la nouvelle valeur proposé n'est pas dans la combinaison
                {   // nbValeurTrouve était déjà à 3
                    couleurDisponible[numCouleur, 1] = 0;
                }
                else
                {
                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B +
                            ". nbValeur à une valeur anormale. nbValeur= " + nbValeurTrouve);
                    break;
                }
            }

            else if ((N == 0) && (B == 3))
            {
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B +
                            ". nbValeur ne devrait pas être = 0. nbValeur= " + nbValeurTrouve);
                    break;
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait été trouvé elle n'est pas à la bonne place
                    //  la nouvelle valeur trouvé est en double
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 2;
                    numCouleurTrouve[1] = numCouleur;
                    // ------------------> tableau des places

                    //    Debug.Log("<IA.LancerIA> : N== 0 && B == 3 : nbValeurTrouve == 1 passe numéro " + nbPasse +
                    //        " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient été trouvé
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    //  La nouvelle valeur est unique
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 1;
                    for(int i = 0; i < nbValeurTrouve; i++)
                    {
                        if(numCouleurTrouve[i]== -1)
                        {
                            numCouleurTrouve[i] = numCouleur;
                            break;
                        }
                    }

                    //Debug.Log("<IA.LancerIA> : N== 0 && B == 3 : nbValeurTrouve == 2 passe numéro " + nbPasse +
                    //            " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + 
                    //            couleurDisponible[numCouleur, 1]);

                    // ------------------> tableau des places

                }
                else if (nbValeurTrouve == 3)// la nouvelle valeur proposé n'est pas dans la combinaison
                {   // nbValeurTrouve était déjà à 3
                    couleurDisponible[numCouleur, 1] = 0;
                    //Debug.Log("<IA.LancerIA> : N== 0 && B == 3 : nbValeurTrouve == 3 passe numéro " + nbPasse +
                    //            " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " +
                    //            couleurDisponible[numCouleur, 1]);
                }
                else
                {
                    Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B +
                            ". nbValeur à une valeur anormale. nbValeur= " + nbValeurTrouve);
                    break;
                }

                //Debug.Log("----> 3B : numCouleurTrouve[0] : " + numCouleurTrouve[0]);
                for (int i = 0; numCouleurTrouve[i] != -1; i++)
                {
                    placement[proposition[i]][i] = false;
                    //Debug.Log("-------> 3B : numCouleurTrouve[i] : " + numCouleurTrouve[i] + " i= " + i);
                }

            }

            //-------- ZONE INTERDITE

            else
            {
                // On ne devrait jamais être ici !!!!
                Debug.Log("----> On ne devrait jamais être ici ! N= " + N + " B= " + B);
                break;
            }

            //-------- FIN DU TOUR

            // Affiche les placements possible
            //Debug.Log("-----> Passe= " + nbPasse);
            //for (int i = 0; i < placement.Length; i++)
            //{
            //    for (int j = 0; j < proposition.Length; j++)
            //    {
            //        Debug.Log("couleur: " + i + " place j " + j + " place[p[i]][j] " + placement[i][j]);
            //    }
            //}

            nbPasse++;
            if(nbPasse>= Globales.NB_LIGNE_MAX)
            {
                Debug.Log("<IA.LancerIA> Nombre de coups max atteint : " + nbPasse);
                break;
            }
            if(numCouleur < Globales.NB_COULEURS - 1) numCouleur++;

            if(bouclePrincipale)
            {
                proposition = construitProposition(couleurDisponible, placement, ("N :" + N + " B : " + B + " np Passe= " + nbPasse));
                affiche(proposition, "proposition: ");

            }
            else
            {
                univers[iu].CopyTo(proposition,0);
                iu++;
            }

        }

        OnIADecode.Invoke(51165);
    }

    private void construitLigne(int[] proposition)
    {

        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            ligneSecret.Ajoute_Pion_Ligne(proposition[i]);
        }
    }
    private int[] construitProposition(int[,] cd, List<bool>[] place, string debug)
    {
        int[] p = new int[Globales.NB_PION_LIGNE];
        int[] pp = new int[] {-1,-1,-1,-1};
        int ip = 0;

        Debug.Log("Dans construction de la proposition: " + debug);
        affiche(place, "Place ??? ");

        //affiche(cd);
        //while(ip< Globales.NB_PION_LIGNE)
        for (int i = 0; i < Globales.NB_COULEURS; i++)
        {
            if (cd[i, 1] == 0) continue; // La couleur n'est pas dans le code

            // La couleur n'a pas été essayées, on complète la proposition avec cette valeur
            if(cd[i, 1] == -1)
            {
                while(ip < Globales.NB_PION_LIGNE)
                {
                    p[ip++] = cd[i,0];
                }
            }

            // La couleur à déjà été essayées et est présente dans le code.
            //   cd[i,1] donne le nombre de fois ou cette couleur est utilisée.
            else
            {
                for(int j= 0; j < cd[i,1]; j++)
                {
                    p[ip++]= cd[i,0];
                }
                //for(int u= 0; u< Globales.NB_PION_LIGNE; u++)
                //{
                //    Debug.Log("<IA.construitProposition> 'else' 1 ou plusieurs occurence de cette couleur cd[i,0] " + cd[i,0] +
                //    " occurence (cd[i,1]) " + cd[i, 1]);
                //}
            }

        }

        //affiche(p, "Et p ICI ??? ");
        // On place la 1er couleur à la premiere place disponible
        bool couleurPlacee = false;

        for (int i = 0; i < p.Length; i++)
        {
            couleurPlacee = false;

            for (int j = 0; j < p.Length; j++)
            {
                if ((place[p[i]][j] == true) && pp[j] == -1) // p[i] est la i ème couleur de la combinaison j sa place possible
                {
                    //Debug.Log("place[p[" + i + "]][" + j + "]= " + place[p[i]][j] + " pp[" + j + "]= " + pp[j] + " p[" + i + "]= " + p[i]);
                    pp[j] = p[i];
                    couleurPlacee = true;
                    break;
                }
            }

            if(!couleurPlacee)
            {
                break;
            }
        }

        if (!couleurPlacee) // S'il n'a pas été possible de placé toutes les couleurs, on décale simplement la combinaison d'entrée
        {
            int tmp, i;
            tmp= p[0];
            pp[0] = 2;
            pp[2]= tmp;

            for (i = 0; i < p.Length-1; i++)
            {
                pp[i] = p[i+1];
            }
            pp[i] = p[0];

        }
        
        affiche(pp, "Et pp ICI ??? ");

        return pp;

    }


    private int[] ordonneProposition(int[] prop)
    {
        bool permutation = true;

        while(permutation)
        {
            permutation = false;
            for(int i= 0; i< prop.Length - 1; i++)
            {
                if (prop[i] > prop[i+1])
                {
                    int tmp= prop[i];

                    prop[i] = prop[i + 1];
                    prop[i + 1] = tmp;
                    
                    permutation = true;
                }
            }
        }

        return prop;

    }




    private void rechercheCode()
    {
        Debug.Log("<Decode.rechercheCode> ");

    }

    private List<bool>[] B4N0(int[] nCT, int[] prop, List<bool>[] place)
    {
        //Debug.Log("<IA.B4N0> : (N== 0) && (B==4) prop.Length= " + prop.Length);

        for (int i = 0; i < Globales.NB_PION_LIGNE; i++) // i< proposition.Length // nCT[i] != -1
        {
            //Debug.Log(">>>>> i= " + i + "prop[i]= " + prop[i]);
            place[prop[i]][i] = false;
            //        pos++;
        }


        return place;

    }



    static void Permute(int[] tab, int i, int n)
    {
        int j;


        if (i == n)
        {
            //affiche(tab, "Tab");

            bool valid = true;
            int[] tmp = new int[tab.Length];
            tab.CopyTo(tmp, 0);

            foreach(var el in univers)
            {
                if(el.SequenceEqual(tmp))
                {
                    valid = false;
                    break;
                }

            }
            if(valid)
            {
                univers.Add(tmp);
            }
        }
        else
        {
            for (j = i; j <= n; j++)
            {
                Swap(ref tab[i], ref tab[j]);
                Permute(tab, i + 1, n);
                Swap(ref tab[i], ref tab[j]);
            }
        }
    }


    static void Swap(ref int a, ref int b)
    {
        int tmp;
        tmp = a;
        a = b;
        b = tmp;
    }

    static void affiche(int[] t, string com)
    {
        string str = "";
        for (int k = 0; k < 4; k++)
        {
            str += t[k];
        }
        Debug.Log(com + " - " + str);

    }
    static void affiche(List<bool>[] t, string com)
    {
        string str = "";
        for (int k = 0; k < 4; k++)
        {
            for (int kk = 0; kk < 4; kk++)
            {
                str += t[k][kk];
            }
            str += " - ";
        }
        Debug.Log(com + " - " + str);

    }
    static void affiche(List<int[]> l)
    {
        Debug.Log("Affichage de liste: + " + l.Count);
        for (int i = 0; i < l.Count; i++)
        {
            affiche(l[i], "Affiche liste " + i + " - " + l[i][0] + l[i][1] + l[i][2] + l[i][3] + " !!!! ");
        }
    }
    static void affiche(int[,] cd)
    {
        for (int i = 0; i < 4; i++)
        {
            for(int j= 0; j<2 ;j++)
            {
                Debug.Log("cd[" + i + "," + j+"]" + cd[i,j]);   

            }
        }
    }
    //static void affiche(List<bool>[] l)
    //{
    //    Debug.Log("Affichage de liste: + " + l.Length);
    //    for (int i = 0; i < l.Length; i++)
    //    {
    //        affiche(l[i], "Affiche liste " + i + " - " + l[i][0] + l[i][1] + l[i][2] + l[i][3] + " !!!! ");
    //    }
    //}
}




/*
for (int i = 0; i < Globales.TAILLE_UNIVERS; Debug.Log("univers[i]: " + univers[i,0] + " - " + univers[i,1] + " - " +
    univers[i,2] + " - " + univers[i,3] + " : " + i + " / " + univers.Length ), i++) ;
*/

//int[] propositionInitiale = new int[Globales.NB_PION_LIGNE];


/*
        //List<int>[] univers= null;
        int[,] univers = new int[Globales.TAILLE_UNIVERS, Globales.NB_PION_LIGNE];
        int[,] universTmp = new int[Globales.TAILLE_UNIVERS + 1, Globales.NB_PION_LIGNE];

        // Ce code ne peut s'adapter à un nombre de pions différents
        // RECHERCHER UNE ALTERNATIVE
        for (int i= 0, u= 0; i< Globales.NB_COULEURS; i++)
        {
            for (int j = 0; j < Globales.NB_COULEURS; j++)
            {
                for (int k = 0; k < Globales.NB_COULEURS; k++)
                {

                    for (int l = 0; l < Globales.NB_COULEURS; l++, u++)
                    {
                        //int[] t= { i,j,k,l};
                        univers[u, 0] = i;
                        univers[u, 1] = j;
                        univers[u, 2] = k;
                        univers[u, 3] = l;
                        //univers[u,] = new int[,] {i,j,k,l };
                    }
                }

            }
        }
 
*/



/*
 


using System;
					
public class Program
{
	public static void Main()
	{
	int[] tab= new int[] {1,2,3,4};
		
	Permute(tab, 0, 3);
	Console.ReadKey();	
	}


// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

	static void Permute(int[] arry, int i, int n)
	{
		int j;
		if (i==n)
		{
			string str = "";
			for( int k= 0; k < 4; k++)
			{
				str+= arry[k];
			}
				
			Console.WriteLine(str);
		}
		else
		{
			for(j = i; j <=n; j++)
			{
				//int tmp= arry[i];
				//arry[i]= arry[j];
				//arry[j]= tmp;
				//Swap2(arry, i, j);
				Swap(ref arry[i], ref arry[j]);
				Permute(arry,i+1,n);
				//tmp= arry[i];
				//arry[i]= arry[j];
				//arry[j]= tmp;
				//Swap2(arry, i, j); //backtrack
				Swap(ref arry[i], ref arry[j]);
			}
		}
	}

	static void Swap2(int[] tab, int a, int b)
	{
		int tmp;
		tmp = tab[a];
		tab[a]= tab[b];
		tab[b] = tmp;
	}	
	
	static void Swap(ref int a, ref int b)
	{
		int tmp;
		tmp = a;
		a= b;
		b = tmp;
	}
	
}



 */