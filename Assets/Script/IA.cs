using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        // Initialisation de l'univers des possibles

        OnIADecode += new IADecode(gp.recup);

    }
    public void lancerIA()
    {
        //int indiceUniversTmp = 0;

        int[] proposition = new int[Globales.NB_PION_LIGNE];
        int[] reponse = new int[Globales.NB_PION_LIGNE];
        int[] rechReponse = new int[Globales.NB_PION_LIGNE];
        int[,] couleurDisponible = new int[,] { { Globales.BLUE_COLOR, -1 }, {Globales.CYAN_COLOR, -1 },
            { Globales.GREEN_COLOR, -1 }, {Globales.PURPLE_COLOR, -1}, { Globales.RED_COLOR, -1}, { Globales.YELLOW_COLOR, -1} };

        bool[] placeC1 = new bool[] { true, true, true, true };
        bool[] placeC2 = new bool[] { true, true, true, true };
        bool[] placeC3 = new bool[] { true, true, true, true };
        bool[] placeC4 = new bool[] { true, true, true, true };
        bool[] placeC5 = new bool[] { true, true, true, true };
        bool[] placeC6 = new bool[] { true, true, true, true };

        int numCouleur = couleurDisponible[0,0];
        int nbValeurTrouve = 0;
        // new List<int>();





        //Debug.Log("<Decode.lancerIA> ");

        // 1�re proposition

        proposition= construitProposition(couleurDisponible); 
        int N, B, nbPasse= 1;
        while(true)
        {
            construitLigne(proposition);
            reponse = ligneSecret.CheckResult();

            // Calcul du nombre de pions bien plac� et mal plac�
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
            Debug.Log("<IA.LancerIA> : N, B : " + N + ", " + B);

            if (N == Globales.NB_PION_LIGNE) // 4 N
            {
                Debug.Log("<IA.LancerIA> :Code cass� ! Nombre de passe: " + nbPasse);

                break;
            }

            else if (N + B == Globales.NB_PION_LIGNE)
            {
                Debug.Log("<IA.LancerIA> : toute les couleurs sont d�termin�es : passe num�ro " + nbPasse);

                break;
            }

            else if ((N == 0) && (B == 0))
            {   // Aucune couleur ,n'a �t� trouv�
                couleurDisponible[numCouleur, 1] = 0;
                Debug.Log("<IA.LancerIA> : N== 0 La couleur propos� n'est pas dans le code : passe num�ro " + nbPasse +
                    " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);

                numCouleur++; // On passe � la couleur suivante
                proposition = construitProposition(couleurDisponible);

            }

            //-------- Zone 1: 1N 1B

            else if ( (N == 1) && (B == 0) ) // 
            {
                if (nbValeurTrouve == 0)
                { // La couleur existe dans la combinaison et est unique
                    nbValeurTrouve = 1;
                    couleurDisponible[numCouleur, 1] = 1;
                    Debug.Log("<IA.LancerIA> : (N == 1) && (B == 0) nbValeurTrouve == 0: passe num�ro " + nbPasse +
                       " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
                else // la nouvelle valeur propos� n'est pas dans la combinaison
                {
                    couleurDisponible[numCouleur, 1] = 0;
                    Debug.Log("<IA.LancerIA> : (N == 1) && (B == 0) nbValeurTrouve != 0: La nouvelle valeur n'est pas " +
                        "dans la combinaison. passe num�ro= " + nbPasse +
                       " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
                numCouleur++;
                proposition = construitProposition(couleurDisponible);
            }

            else if ((N == 0) && (B == 1))
            {
                Debug.Log("----> EN COURS DE DEV (N == 0) && (B == 1)");
                Debug.Log("< IA.LancerIA > : N == 0 && B == 1 La couleur propos� n'est pas dans le code : passe num�ro " + nbPasse +
                    " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1] +
                    " nbValeurTrouve (doit �tre = 1) = " + nbValeurTrouve);
                // nbValeurTrouve ne doit pas avoir une valeur diff�rente de 1
                if (nbValeurTrouve == 1)
                {
                    couleurDisponible[numCouleur, 1] = 0;

                    // A FAIRE
                    // Recherche la couleur d�j� trouv� et signaler qu'elle est mal plac�
                    // 
// ------------------> tableau des places

                }
                else
                {
                    Debug.Log("< IA.LancerIA > : N == 0 && B == 1 ERREUR nbValeurTrouve != 1!!!! passe num�ro " + nbPasse +
                        " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1] +
                        " nbValeurTrouve (doit �tre = 1) = " + nbValeurTrouve);

                }
                numCouleur++;
                proposition = construitProposition(couleurDisponible);

            }

            //-------- Zone 2: N2B0 N1B1 N0B2

            // (N == 1) && (B == 1) ET  (N == 2) && (B == 0) 
            else if ((N == 1) && (B == 1)) 
            {
                Debug.Log("<IA.LancerIA> : N== 1 && B == 1 : passe num�ro " + nbPasse +
                   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                // nbValeurTrouve == 0 impossible compte tenu de la construction des propositions
                if (nbValeurTrouve == 1)
                {   // Une valeur avait pr�c�demment �t� trouv�. Elle n'est pas � la bonne place !
                    //placeC1[0] = false;
                    nbValeurTrouve = 2;
// ------------------> tableau des places
                    couleurDisponible[numCouleur, 1] = 1;

                }
                else if (nbValeurTrouve == 2)
                {   // La nouvelle valeur propos�e n'est pas dans le code
                    // 1 couleur trouv� est en bonne position, l'autre pas !
                    couleurDisponible[numCouleur, 1] = 0;

                }
                numCouleur++;
                proposition = construitProposition(couleurDisponible);
            }


            //FAUX ???
            //else if ((N == 1) && (B == 1))
            //{
            //    Debug.Log("<IA.LancerIA> : N== 1 && B == 1 : passe num�ro " + nbPasse +
            //       " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
            //    if (nbValeurTrouve == 0)
            //    {   // La couleur existe dans la combinaison et est double
            //        nbValeurTrouve = 2;
            //        couleurDisponible[numCouleur, 1] = 2;
            //    }
            //    else if (nbValeurTrouve == 1)
            //    {   // Une valeur avait �t� trouv�, une nouvelle vient d'�tre trouv� et elle est unique
            //        nbValeurTrouve = 2;
            //        couleurDisponible[numCouleur, 1] = 1;

            //    }
            //    else // la nouvelle valeur propos� n'est pas dans la combinaison
            //    {   // nbValeurTrouve �tait d�j� � 2
            //        couleurDisponible[numCouleur, 1] = 0;
            //    }
            //    numCouleur++;
            //    proposition = construitProposition(couleurDisponible);
            //}


            else if ((N == 2) && (B == 0))
            {
                if (nbValeurTrouve == 0)
                {   // La couleur existe dans la combinaison et est double
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 2;
                    Debug.Log("<IA.LancerIA> : N== 2 && B == 0 : nb_valeur= 0 passe num�ro " + nbPasse +
                       " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait �t� trouv�, une nouvelle vient d'�tre trouv� et elle est unique
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 1;
                    Debug.Log("<IA.LancerIA> : N== 2 && B == 0 : nb_valeur= 1 passe num�ro " + nbPasse +
                       " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);

                }
                else // la nouvelle valeur propos� n'est pas dans la combinaison
                {   // nbValeurTrouve �tait d�j� � 2
                    couleurDisponible[numCouleur, 1] = 0;
                    Debug.Log("<IA.LancerIA> : N== 2 && B == 0 : nb_valeur= 2 passe num�ro " + nbPasse +
                       " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
                numCouleur++;
                proposition = construitProposition(couleurDisponible);
            }

            else if ((N == 0) && (B == 2))
            {
                Debug.Log("<IA.LancerIA> : N== 0 && B == 2 : passe num�ro " + nbPasse +
                   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais �tre ici ! N= " + N + " B= " + B + 
                        ". nbValeur ne devrait �tre = 0. nbValeur= " + nbValeurTrouve);
                    //nbValeurTrouve = 2;
                    //couleurDisponible[numCouleur, 1] = 2;
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait �t� trouv� elle n'est pas � la bonne place
                    //  La nouvelle valeur trouv� n'est pas � la bonne place
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 1;
                    Debug.Log("<IA.LancerIA> : N== 0 && B == 2 : nbValeurTrouve == 1 passe num�ro " + nbPasse +
                               " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + 
                               couleurDisponible[numCouleur, 1]);

// ------------------> tableau des places
                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient �t� trouv� et ne sont pas � la bonne place
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    //  La nouvelle valeur n'est pas dans la combinaison
                    nbValeurTrouve = 2;
                    couleurDisponible[numCouleur, 1] = 0;

                    Debug.Log("<IA.LancerIA> : N== 0 && B == 2 : nbValeurTrouve == 2 passe num�ro " + nbPasse +
                               " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " +
                               couleurDisponible[numCouleur, 1]);

// ------------------> tableau des places
                
                }
                else // la nouvelle valeur propos� n'est pas dans la combinaison
                {   // nbValeurTrouve �tait d�j� � 2
                    couleurDisponible[numCouleur, 1] = 0;
                }
                numCouleur++;
                proposition = construitProposition(couleurDisponible);
            }

            //-------- Zone 3: N3B0 N2B1 N1B2 N0B3

            // N= 3
            else if ((N == 3) && (B == 0))
            {
                Debug.Log("<IA.LancerIA> : N== 3 && B == 0 : passe num�ro " + nbPasse +
                   " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                if (nbValeurTrouve == 0)
                {   // La couleur existe dans la combinaison et est triple
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 3;
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait �t� trouv�, une nouvelle vient d'�tre trouv� et elle est double
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 2;

                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avait �t� trouv�, une nouvelle vient d'�tre trouv� et elle est unique
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 1;

                }
                else // la nouvelle valeur propos� n'est pas dans la combinaison
                {   // nbValeurTrouve �tait d�j� � 3
                    couleurDisponible[numCouleur, 1] = 0;
                }
                numCouleur++;
                proposition = construitProposition(couleurDisponible);
            }

            else if ((N == 2) && (B == 1))
            { 
                Debug.Log("<IA.LancerIA> : N== 2 && B == 1 : passe num�ro " + nbPasse +
                    " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais �tre ici ! N= " + N + " B= " + B +
                            ". nbValeur ne devrait pas �tre = 0. nbValeur= " + nbValeurTrouve);
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait �t� trouv� elle n'est pas � la bonne place
                    //  la nouvelle valeur trouv� est en double
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 2;
// ------------------> tableau des places
                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient �t� trouv�
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    //  La nouvelle valeur est unique
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 1;

// ------------------> tableau des places
                }
                else if (nbValeurTrouve == 3)// la nouvelle valeur propos� n'est pas dans la combinaison
                {   // nbValeurTrouve �tait d�j� � 3
                    couleurDisponible[numCouleur, 1] = 0;
                }
                else
                {
                    Debug.Log("----> On ne devrait jamais �tre ici ! N= " + N + " B= " + B +
                            ". nbValeur � une valeur anormale. nbValeur= " + nbValeurTrouve);
                }

                numCouleur++;
                proposition = construitProposition(couleurDisponible);
            }

            else if ((N == 1) && (B == 2))
            {
                Debug.Log("<IA.LancerIA> : N== 2 && B == 1 : passe num�ro " + nbPasse +
                    " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais �tre ici ! N= " + N + " B= " + B +
                            ". nbValeur ne devrait pas �tre = 0. nbValeur= " + nbValeurTrouve);
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait �t� trouv� elle n'est pas � la bonne place
                    //  la nouvelle valeur trouv� est en double
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 2;
// ------------------> tableau des places
                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient �t� trouv�
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    //  La nouvelle valeur est unique
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 1;

                    // ------------------> tableau des places
                }
                else if (nbValeurTrouve == 3)// la nouvelle valeur propos� n'est pas dans la combinaison
                {   // nbValeurTrouve �tait d�j� � 3
                    couleurDisponible[numCouleur, 1] = 0;
                }
                else
                {
                    Debug.Log("----> On ne devrait jamais �tre ici ! N= " + N + " B= " + B +
                            ". nbValeur � une valeur anormale. nbValeur= " + nbValeurTrouve);
                }

                numCouleur++;
                proposition = construitProposition(couleurDisponible);
            }

            else if ((N == 0) && (B == 3))
            {
                if (nbValeurTrouve == 0) // IMPOSSIBLE COMPTE TENU DE L'ALGO
                {
                    Debug.Log("----> On ne devrait jamais �tre ici ! N= " + N + " B= " + B +
                            ". nbValeur ne devrait pas �tre = 0. nbValeur= " + nbValeurTrouve);
                }
                else if (nbValeurTrouve == 1)
                {   // Une valeur avait �t� trouv� elle n'est pas � la bonne place
                    //  la nouvelle valeur trouv� est en double
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 2;

// ------------------> tableau des places

                    Debug.Log("<IA.LancerIA> : N== 0 && B == 3 : nbValeurTrouve == 1 passe num�ro " + nbPasse +
                        " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + couleurDisponible[numCouleur, 1]);
                }
                else if (nbValeurTrouve == 2)
                {   // Deux valeurs avaient �t� trouv�
                    //  SI PERMUTATION DES 2 VALEURS CA DEVRAIT POSITIONNER LES 2 VALEURS A LEUR PLACE
                    //  La nouvelle valeur est unique
                    nbValeurTrouve = 3;
                    couleurDisponible[numCouleur, 1] = 1;

                    Debug.Log("<IA.LancerIA> : N== 0 && B == 3 : nbValeurTrouve == 2 passe num�ro " + nbPasse +
                                " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " + 
                                couleurDisponible[numCouleur, 1]);

// ------------------> tableau des places

                }
                else if (nbValeurTrouve == 3)// la nouvelle valeur propos� n'est pas dans la combinaison
                {   // nbValeurTrouve �tait d�j� � 3
                    couleurDisponible[numCouleur, 1] = 0;
                    Debug.Log("<IA.LancerIA> : N== 0 && B == 3 : nbValeurTrouve == 3 passe num�ro " + nbPasse +
                                " couleur= " + numCouleur + " couleurDisponible[numCouleur,1]= " +
                                couleurDisponible[numCouleur, 1]);
                }
                else
                {
                    Debug.Log("----> On ne devrait jamais �tre ici ! N= " + N + " B= " + B +
                            ". nbValeur � une valeur anormale. nbValeur= " + nbValeurTrouve);
                }

                numCouleur++;
                proposition = construitProposition(couleurDisponible);
            }

            //-------- ZONE INTERDITE

            else
            {
                // On ne devrait jamais �tre ici !!!!
                Debug.Log("----> On ne devrait jamais �tre ici ! N= " + N + " B= " + B);
            }

            //-------- FIN DU TOUR

            nbPasse++;
            if(nbPasse>= Globales.NB_LIGNE_MAX)
            {
                Debug.Log("<IA.LancerIA> Nombre de coups max atteint : " + nbPasse);
                break;
            }
        }



        //for (int i = 0; i < proposition.Length; proposition[i] = i, i++) ;
        //for (int i = 0; i < Globales.NB_PION_LIGNE; Debug.Log("reponse[i]: " + proposition[i]), i++) ;


        OnIADecode.Invoke(51165);

    }

    private void construitLigne(int[] proposition)
    {

        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            ligneSecret.Ajoute_Pion_Ligne(proposition[i]);
        }
    }
    private int[] construitProposition(int[,] cd)
    {
        int[] p = new int[Globales.NB_PION_LIGNE];
        int ip = 0;

        //while(ip< Globales.NB_PION_LIGNE)
        for (int i = 0; i < Globales.NB_COULEURS; i++)
        {
            if (cd[i, 1] == 0) continue; // La couleur n'est pas dans le code
            if(cd[i, 1] == -1)
            {
                while(ip < Globales.NB_PION_LIGNE)
                {
                    p[ip++] = cd[i,0];
                }
            }
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

        return p;

    }


    //    }



    private void rechercheCode()
    {
        Debug.Log("<Decode.rechercheCode> ");

    }



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

        // Ce code ne peut s'adapter � un nombre de pions diff�rents
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
 while (true)
 {
     // L'IA joue !
     for(int i= 0; i<proposition.Length; i++)
     {
         ligneSecret.Ajoute_Pion_Ligne(proposition[i]);
     }
     reponse= ligneSecret.CheckResult();
     //for (int i = 0; i < Globales.NB_PION_LIGNE; Debug.Log("reponse[i]: " + reponse[i]), i++) ;

     // Test de la r�ponse
     // A FAIRE

     // On construit l'univers des possibles

     for(int i= 0, indiceUniversTmp = 0; i < Globales.TAILLE_UNIVERS/Globales.NB_PION_LIGNE; i++)
     {
         bool different = false;
         int[] tmp = { univers[i, 0], univers[i, 1], univers[i, 2], univers[i, 3] };
         rechReponse = codeSecret.compareTableau(tmp);

         for (int j = 0; j < Globales.NB_PION_LIGNE; j++)
         {
             if(rechReponse[j]!= reponse[j])
             {
                 different = true;
                 break;
             }
         }
         if(!different)
         {
             for(int k= 0; k< Globales.NB_PION_LIGNE; k++)
             {
                 universTmp[indiceUniversTmp, k] = tmp[k];
             }
             indiceUniversTmp++;
         }
     }

     univers = universTmp.Clone() as int[,];


     for (int i = 0; i < univers.Length / 4; Debug.Log("univers[i]: " + univers[i,0] + " - " + univers[i,1] + " - " +
          univers[i,2] + " - " + univers[i,3] + " : " + i + " / " + univers.Length ), i++) ;



     break;
 }
 */
