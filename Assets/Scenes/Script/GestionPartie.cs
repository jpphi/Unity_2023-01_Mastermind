using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPartie : MonoBehaviour
{
    [SerializeField] protected GameObject boule;
    [SerializeField] protected GameObject marque;
    [SerializeField] protected Code code;
    //[SerializeField] protected Pion pion;
    [SerializeField] protected Ligne ligne;

    //private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE];

    private int indicePion;
    //Vector3 pos;
    bool ligneComplete;
    private int[] CodeCouleur = new int[Globales.NB_PION_LIGNE];


    void Start()
    {
        // Initialisation de La partie
        init_partie();
        ligneComplete = false;

    }
            
    void Update()
    {
        //pos = new Vector3(2 * indicePion, 5, 0);

        if (Input.GetKey(KeyCode.T))
        {
            code.Triche();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ligneComplete= ligne.Ajoute_Pion_Ligne(Globales.RED_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            ligneComplete = ligne.Ajoute_Pion_Ligne(Globales.BLUE_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.Y))
        {
            ligneComplete = ligne.Ajoute_Pion_Ligne(Globales.YELLOW_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            ligneComplete = ligne.Ajoute_Pion_Ligne(Globales.PURPLE_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            ligneComplete = ligne.Ajoute_Pion_Ligne(Globales.CYAN_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            ligneComplete = ligne.Ajoute_Pion_Ligne(Globales.GREEN_COLOR);
        }

        //Debug.Log("Booleen ligneComplete" + ligneComplete);

    }

    private void init_partie()
    {

        for(int i= 0; i< Globales.NB_PION_LIGNE; i++)
        {
            CodeCouleur[i] = Random.Range(1, 6);
        }
        code.GenereCode(CodeCouleur);

    }

    private void ValidationProposition()
    {
        //Debug.Log("<Fonction ValidationProposition> Validation : " + ligneComplete);

        if(ligneComplete)
        {
            ligneComplete= false;
            ligne.CheckResult(CodeCouleur);
        }
    }

}
