//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

using UnityEngine.Events;


public class Ligne : Pion
{

    [SerializeField] protected Pion pion;
    [SerializeField] protected Code code;
    [SerializeField] protected Marque marque;
    [SerializeField] private GameObject plateau;

    private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE * Globales.NB_LIGNE_MAX];
    private int[] TabCouleurPions = new int[Globales.NB_PION_LIGNE];
    private int indicePion = 0, nombreElementDansLigne = 0;

    private int LigneEnCours = 0;


    private GameObject[] marques = new GameObject[Globales.NB_PION_LIGNE * Globales.NB_LIGNE_MAX];
    //private int[] TabReponse= new int[Globales.NB_PION_LIGNE];

    private Vector3 pos= new Vector3(0,5,0);
    private int profondeur= Globales.POSITION_LIGNE_BASE_Z;
    private Vector3 plateauPosition;

    public UnityEvent eventGagne;

    /**/
    private void Start()
    {
        plateauPosition = plateau.transform.position;
        //plateauPosition =  new Vector3(0,0,0);

    }

    public bool Ajoute_Pion_Ligne(int couleur)
    {
        bool valid;
        // On ne doit plus pouvoir ajouter de pion lorsque le nombre maximum de coups
        //  est atteint!
        if (LigneEnCours >= Globales.NB_LIGNE_MAX)
        {
            return false;
        }

        // L'élément suivant le dernier élément du tableau est l'élément 0 (tableau circulaire)
        if (indicePion >= Globales.NB_PION_LIGNE)
        {
            indicePion = 0;

        }

        pos = new Vector3(-2 * indicePion, 5, 0);

        if(pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE] != null)
        { // un pion existe il faut d'abord le détruire
            Destroy(pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE]);
            pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE] = pion.CreationPion(pos, couleur);
        }
        else
        {
            pions[indicePion + LigneEnCours * Globales.NB_PION_LIGNE] = pion.CreationPion(pos, couleur);
        }

        TabCouleurPions[indicePion] = couleur;
        indicePion++;

        nombreElementDansLigne++;

        valid = nombreElementDansLigne >= Globales.NB_PION_LIGNE;
        //Debug.Log("nombreElementDansLigne " + nombreElementDansLigne + " valid " + valid + " indicePion" + indicePion);

        //if(nombreElementDansLigne== )

        return ( valid );
    }

    public int[] CheckResult()
    {
        //int k = 0;
        float x, y;
        int[] tcp = new int[Globales.NB_PION_LIGNE];
        int[] TabReponse = new int[Globales.NB_PION_LIGNE];

        nombreElementDansLigne = 0;
        indicePion = 0;
        // POSER LA LIGNE AVEC LES MARQUES

        // On pose la ligne
        for (int i=0; i<Globales.NB_PION_LIGNE; i++)
        {
            x = 9 - 2 * i;
            y = 1;// 1f + (float)(LigneEnCours) / 3;
            Vector3 positionPion = plateauPosition + new Vector3(x, y, profondeur);
            pions[i + LigneEnCours * Globales.NB_PION_LIGNE].transform.localPosition= positionPion;
        }

        // Initialisation des tableaux
        for (int i = 0; i < Globales.NB_PION_LIGNE; tcp[i]= TabCouleurPions[i], i++); // copie des tableaux

        TabReponse = code.compareTableau(tcp);

        //for(int i = 0; i < TabReponse[i]; Debug.Log("Retour de compare: " + TabReponse[i]), i++);

        // Affichons le tableau des marques
        for (int u = 0; (u < Globales.NB_PION_LIGNE) && (TabReponse[u] != -1); u++)
        {
            x = -9 + 2 * u;
            pos = plateauPosition + new Vector3(x, 1, profondeur); // new Vector3(2 * indicePion, 5, 0);
            marques[u] = marque.CreationMarque(pos, TabReponse[u]);
            //Debug.Log("<Ligne.CheckResult> Dans le for, élément de TabReponse: " + TabReponse[u]);
        }

        // As t on gagné
        int nbMarqueNoire = 0;
        for (int i= 0; i < Globales.NB_PION_LIGNE;i++)
        { 
            if (TabReponse[i] == Globales.BLACK_COLOR) nbMarqueNoire++;
        }
        if (nbMarqueNoire >= Globales.NB_PION_LIGNE)
        {
            Globales.SCORE = 16;
            eventGagne.Invoke();
        }

        // On décale pour la ligne suivante
        profondeur += 2;

        LigneEnCours++;

        //nombreElementDansLigne= 0;

        return TabReponse;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
