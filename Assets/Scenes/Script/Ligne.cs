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

// Start is called before the first frame update
    public bool Ajoute_Pion_Ligne(int couleur)
    {

        if (nombreElementDansLigne== Globales.NB_PION_LIGNE)
        { // La ligne est compl�te
            if(indicePion== Globales.NB_PION_LIGNE) indicePion = 0;

            // Detruire le pion avant d'en cr�er un nouveau
            Destroy(pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE]);

            //pos = new Vector3(-2 * indicePion, 5, 0);
            pos = new Vector3(-2 * indicePion, 5, 0);
            pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE] = pion.CreationPion(pos, couleur);
            TabCouleurPions[indicePion] = couleur;

            //Debug.Log("4EL ELSE Dans Ligne cr�ation pion couleur: " + couleur);
            //Debug.Log("L'indice est: " + indicePion + " la position est :" + pos);

            indicePion++;

        }
        else
        {
            pos = new Vector3(-2 * indicePion, 5, 0);

            pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE] = pion.CreationPion(pos, couleur);
            TabCouleurPions[indicePion] = couleur;
            //Debug.Log("Dans Ligne cr�ation pion couleur: " + couleur);
            //Debug.Log("L'indice est: " + indicePion + " la position est :" + pos);

            indicePion++;
            nombreElementDansLigne++;
        }

        return (nombreElementDansLigne== Globales.NB_PION_LIGNE);
    }

    public void CheckResult(int[] TabCouleurCode)
    {
        int k = 0;
        int[] tcp = new int[Globales.NB_PION_LIGNE];
        int[] tcc = new int[Globales.NB_PION_LIGNE];

        //Debug.Log("< Fonction CheckResult > " + transform.position);

        // POSER LA LIGNE AVEC LES MARQUES

        // On pose la ligne
        for(int i=0; i<Globales.NB_PION_LIGNE; i++)
        {
            pions[i + LigneEnCours * Globales.NB_PION_LIGNE].transform.position =  plateauPosition + new Vector3(9 - 2 * i, 1, profondeur );
        }

        // Initialisation des tableaux
        for (int i = 0; i < Globales.NB_PION_LIGNE; TabReponse[i] = -1, i++) ; // Init tableau
        for (int i = 0; i < Globales.NB_PION_LIGNE; tcp[i]= TabCouleurPions[i], tcc[i]= TabCouleurCode[i], i++); // copie des tableaux

        // On d�termine les marques noires en comparant la proposition au code
        // Les boucles for imbriqu�e ne permette pas de r�gl� le probl�me 11XX 0111 
        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            if (tcp[i] == tcc[i])
            { // La couleur est-elle bien plac� ? 
                TabReponse[k++] = Globales.BLACK_COLOR;
                tcc[i] = -1; // Le cas est trait�e, on �crase la valeur dans le code
                tcp[i] = -2; // Le cas est trait�e, on �crase la valeur dans le code
            }
        }
        // On a d�termin�e les marque noires, on d�termine les marques blanches
        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        { 
            for(int j= 0; j< Globales.NB_PION_LIGNE;j++)
            {
                if (tcp[i] == tcc[j])
                { // Si on trouve une couleur mal plac� on le note et on sort de la boucle

                    TabReponse[k++] = Globales.WHITE_COLOR;
                    tcc[j] = -1; // Le cas est trait�e, on �crase la valeur dans le code
                    tcp[i] = -2; // Le cas est trait�e, on �crase la valeur dans le code
                    break;
                }
            }
        }

        // Affichons le tableau des marques
        for (int u = 0; (u < Globales.NB_PION_LIGNE) && (TabReponse[u] != -1); u++)
        {
            pos = plateauPosition + new Vector3(-9 + 2 * u, 1, profondeur); // new Vector3(2 * indicePion, 5, 0);
            marques[u] = marque.CreationMarque(pos, TabReponse[u]);
            //Debug.Log("<Ligne.CheckResult> Dans le for, �l�ment de TabReponse: " + TabReponse[u]);
        }

        //Debug.Log("");

        // On d�cale pour la ligne suivante
        profondeur += 2;

        // On incr�mente le nombre de ligne en cours
        LigneEnCours++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
