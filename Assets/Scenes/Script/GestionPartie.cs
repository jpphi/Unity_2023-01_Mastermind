using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPartie : MonoBehaviour
{
    [SerializeField] protected GameObject boule;
    [SerializeField] protected GameObject marque;
    [SerializeField] protected Code code;
    [SerializeField] protected Pion pion;

    int indicePion;
    Vector3 pos;

    //private Code code;

    // Start is called before the first frame update
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            pion.CreationPion(pos, Globales.RED_COLOR);
            indicePion++;
            Debug.Log("Création pion red");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            pion.CreationPion(pos, Globales.BLUE_COLOR);
            indicePion++;
            Debug.Log("Création pion blue");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            pion.CreationPion(pos, Globales.YELLOW_COLOR);
            indicePion++;
            Debug.Log("Création pion blue");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            pion.CreationPion(pos, Globales.PURPLE_COLOR);
            indicePion++;
            Debug.Log("Création pion blue");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            pion.CreationPion(pos, Globales.CYAN_COLOR);
            indicePion++;
            Debug.Log("Création pion blue");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            pion.CreationPion(pos, Globales.GREEN_COLOR);
            indicePion++;
            Debug.Log("Création pion blue");
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
