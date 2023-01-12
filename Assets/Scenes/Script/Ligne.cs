using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Ligne : Pion
{
    [SerializeField] protected Pion pion;
    [SerializeField] protected Marque marque;
    [SerializeField] private GameObject plateau;

    private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE * Globales.NB_LIGNE_MAX];
    private int[] TabCouleurPions = new int[Globales.NB_PION_LIGNE];
    private int indicePion = 0, nombreElementDansLigne = 0;

    private int LigneEnCours = 0;


    private GameObject[] marques = new GameObject[Globales.NB_PION_LIGNE * Globales.NB_LIGNE_MAX];
    private int[] TabReponse= new int[Globales.NB_PION_LIGNE];

    private Vector3 pos= new Vector3(0,5,0);
    private int profondeur= Globales.POSITION_LIGNE_BASE_Z;
    private Vector3 plateauPosition;

    private void Start()
    {
        plateauPosition = plateau.transform.position;
    }

    public bool Ajoute_Pion_Ligne(int couleur)
    {
        // On ne doit plus pouvoir ajouter de pion lorsque le nombre maximum de coups
        //  est atteint!
        if (LigneEnCours >= Globales.NB_LIGNE_MAX)
        {
            return false;
        }

        if (nombreElementDansLigne== Globales.NB_PION_LIGNE)
        { // La ligne est complète !

            // L'élément suivant le dernier élément du tableau est l'élément 0 (tableau circulaire)
            if (indicePion== Globales.NB_PION_LIGNE) indicePion = 0; 

            // Detruire le pion existant avant d'en créer un nouveau
            Destroy(pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE]);

            pos = new Vector3(-2 * indicePion, 5, 0);
            pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE] = pion.CreationPion(pos, couleur);
            TabCouleurPions[indicePion] = couleur;

            //Debug.Log("4EL ELSE Dans Ligne création pion couleur: " + couleur);
            //Debug.Log("L'indice est: " + indicePion + " la position est :" + pos);

            indicePion++;
        }
        else
        {
            pos = new Vector3(-2 * indicePion, 5, 0);

            pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE] = pion.CreationPion(pos, couleur);
            TabCouleurPions[indicePion] = couleur;
            //Debug.Log("Dans Ligne création pion couleur: " + couleur);
            //Debug.Log("L'indice est: " + indicePion + " la position est :" + pos);

            indicePion++;
            nombreElementDansLigne++;
        }

        return (nombreElementDansLigne== Globales.NB_PION_LIGNE);
    }

    public void CheckResult(int[] TabCouleurCode)
    {
        int k = 0;
        float x, y, z;
        int[] tcp = new int[Globales.NB_PION_LIGNE];
        int[] tcc = new int[Globales.NB_PION_LIGNE];

        //Debug.Log("< Fonction CheckResult > " + transform.position);

        // POSER LA LIGNE AVEC LES MARQUES

        // On pose la ligne
        for(int i=0; i<Globales.NB_PION_LIGNE; i++)
        {
            x = 9 - 2 * i;
            y = 1;// 1f + (float)(LigneEnCours) / 3;
            z= 0;
            Vector3 positionPion = plateauPosition + new Vector3(x, y, profondeur);
            pions[i + LigneEnCours * Globales.NB_PION_LIGNE].transform.position = positionPion;
        }

        // Initialisation des tableaux
        for (int i = 0; i < Globales.NB_PION_LIGNE; TabReponse[i] = -1, i++) ; // Init tableau
        for (int i = 0; i < Globales.NB_PION_LIGNE; tcp[i]= TabCouleurPions[i], tcc[i]= TabCouleurCode[i], i++); // copie des tableaux

        // On détermine les marques noires en comparant la proposition au code
        // Les boucles for imbriquée ne permette pas de réglé le problème généré par les combinaisons
        //   code / proposition 11XX X11X 
        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            if (tcp[i] == tcc[i])
            { // La couleur est-elle bien placé ? 
                TabReponse[k++] = Globales.BLACK_COLOR;
                tcc[i] = -1; // Le cas est traitée, on écrase la valeur dans le code
                tcp[i] = -2; // Le cas est traitée, on écrase la valeur dans la proposition faite
            }
        }
        // On a déterminée les marques noires, on détermine les marques blanches
        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            if (tcp[i] == -2) continue;
            for(int j= 0; j< Globales.NB_PION_LIGNE;j++)
            {
                if (tcp[i] == tcc[j])
                { // Si on trouve une couleur mal placé on le note et on sort de la boucle

                    TabReponse[k++] = Globales.WHITE_COLOR;
                    tcc[j] = -1; // Le cas est traitée, on écrase la valeur dans le code
                    tcp[i] = -2; // Le cas est traitée, on écrase la valeur dans la proposition faite
                    break;
                }
            }
        }

        // Affichons le tableau des marques
        for (int u = 0; (u < Globales.NB_PION_LIGNE) && (TabReponse[u] != -1); u++)
        {
            x = -9 + 2 * u;
            y = 1;
            pos = plateauPosition + new Vector3(x, 1, profondeur); // new Vector3(2 * indicePion, 5, 0);
            marques[u] = marque.CreationMarque(pos, TabReponse[u]);
            //Debug.Log("<Ligne.CheckResult> Dans le for, élément de TabReponse: " + TabReponse[u]);
        }

        //Debug.Log("");

        // On décale pour la ligne suivante
        profondeur += 2;

        LigneEnCours++;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
