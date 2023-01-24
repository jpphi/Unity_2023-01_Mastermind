//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class GestionPartie : MonoBehaviour
{
    [SerializeField] protected GameObject boule;
    [SerializeField] protected GameObject marque;
    [SerializeField] protected Code code;
    [SerializeField] protected Ligne ligne;

    bool ligneComplete, FinPartie;

    void Start()
    {
        // Initialisation de La partie
        init_partie();
        ligneComplete = FinPartie= false;
        //ab.OnIADecode += test;
    }
           
    void Update()
    {
        if (!FinPartie)
        {

            if (Input.GetKeyDown(KeyCode.T))
            {
                code.Triche();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ligneComplete = ligne.Ajoute_Pion_Ligne(Globales.RED_COLOR);
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
            else if ( Input.GetKeyDown(KeyCode.Return) && ligneComplete)
            {
                ValidationProposition();
            }

            //Debug.Log("Booleen ligneComplete" + ligneComplete);
        }

    }

    private void init_partie()
    {
        code.GenereCode();
    }
    public void ValidationProposition()
    /*
     * Déclenché par le boutton "Validation du panel". Doit être public pour pouvoir être "vu" par la gestion de l'évènement
     */
    {
        //Debug.Log("<Fonction ValidationProposition> Validation : " + ligneComplete);

        if(ligneComplete)
        {
            ligneComplete= false;

            ligne.CheckResult();
        }
    }


    //public void OnAGagne() // Déclenché dans Ligne lorsque 4 marques noires sont donnée 
    //{
    //    Debug.Log("On a Gagne !!!! " + Globales.SCORE);
    //    code.Triche();
    //    FinPartie = true;
    //}

    public void finIA(int a)
    {
        Debug.Log("Fin du programme IA " + a);
        code.Triche();

    }
}
