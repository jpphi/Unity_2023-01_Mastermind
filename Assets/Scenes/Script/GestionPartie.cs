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

    int indicePion;
    Vector3 pos;

    void Start()
    {
        // Initialisation de La partie
        init_partie();

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
            ligne.Ajoute_Pion_Ligne(Globales.RED_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            ligne.Ajoute_Pion_Ligne(Globales.BLUE_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.Y))
        {
            ligne.Ajoute_Pion_Ligne(Globales.YELLOW_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            ligne.Ajoute_Pion_Ligne(Globales.PURPLE_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            ligne.Ajoute_Pion_Ligne(Globales.CYAN_COLOR);
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            ligne.Ajoute_Pion_Ligne(Globales.GREEN_COLOR);
         }

        //if(indicePion>= Globales.NB_PION_LIGNE)
        //{
        //    indicePion = 0;
        //}


    }

    void init_partie()
    {
        int[] couleurs = new int[Globales.NB_PION_LIGNE];
        //string chaine="";

        // Choix des couleurs
        for(int i= 0; i< couleurs.Length; i++)
        {
            couleurs[i] = Random.Range(1, 6);
            //chaine += ("i: " + i.ToString() + ": " + couleurs[i].ToString()) ;
        }
        //Debug.Log("Chaine: " + chaine);

        code.GenereCode(couleurs);

    }


}
