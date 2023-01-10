using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPartie : MonoBehaviour
{
    [SerializeField] protected GameObject boule;
    [SerializeField] protected GameObject marque;
    [SerializeField] protected Code code;

    //private Code code;

    // Start is called before the first frame update
    void Start()
    {

        // Initialisation de La partie

        //code = new Code();
        init_partie();

        //---------------------------------- TEST
        //GameObject pion;
        //Vector3 pos = new Vector3(0, 5, 0);
        //Quaternion rot = new Quaternion(0, 0, 0, 0);

        //Material m_Material;

        //pion = Instantiate<GameObject>(boule, pos, rot);

        //m_Material = pion.GetComponent<Renderer>().material;

        //Debug.Log("Position : " + m_Material);


        }
            
    void Update()
    {

        if (Input.GetKey(KeyCode.T))
        {
            code.Triche();
        }
    }

    void init_partie()
    {
        // Code secret
        //Random alea = new Random();
        //int[] tab = new int[5];

        Debug.Log("ICI");

        int[] couleurs = new int[Globales.NB_PION_LIGNE];
        string chaine="";

        // Choix des couleurs
        for(int i= 0; i< couleurs.Length; i++)
        {
            couleurs[i] = Random.Range(1, 6);
            chaine += ("i: " + i.ToString() + ": " + couleurs[i].ToString()) ;
        }
        // O
        Debug.Log("Chaine: " + chaine);

        code.GenereCode(couleurs);

    }


}
