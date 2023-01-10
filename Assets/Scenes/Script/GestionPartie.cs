using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPartie : MonoBehaviour
{
    [SerializeField] protected GameObject boule;
    [SerializeField] protected GameObject marque;
    [SerializeField] protected Code code;
    [SerializeField] protected Pion pion;

    private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE];

    int indicePion;
    Vector3 pos;

    void Start()
    {

        // Initialisation de La partie
        init_partie();

        indicePion = 0;

    }
            
    void Update()
    {
        pos = new Vector3(2 * indicePion, 5, 0);

        if (Input.GetKey(KeyCode.T))
        {
            code.Triche();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            pions[indicePion] = pion.CreationPion(pos, Globales.RED_COLOR);
            indicePion++;
            Debug.Log("Création pion red");
        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            pions[indicePion] = pion.CreationPion(pos, Globales.BLUE_COLOR);
            indicePion++;
            Debug.Log("Création pion blue");
        }

        else if (Input.GetKeyDown(KeyCode.Y))
        {
            pions[indicePion] = pion.CreationPion(pos, Globales.YELLOW_COLOR);
            indicePion++;
            Debug.Log("Création pion yellow");
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            pions[indicePion] = pion.CreationPion(pos, Globales.PURPLE_COLOR);
            indicePion++;
            Debug.Log("Création pion purple");
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            pions[indicePion] = pion.CreationPion(pos, Globales.CYAN_COLOR);
            indicePion++;
            Debug.Log("Création pion cyan");
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            pions[indicePion] = pion.CreationPion(pos, Globales.GREEN_COLOR);
            indicePion++;
            Debug.Log("Création pion green");
        }

        if(indicePion>= Globales.NB_PION_LIGNE)
        {
            indicePion = 0;
        }


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
