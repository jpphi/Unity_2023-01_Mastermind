using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Ligne : Pion
{
    [SerializeField] protected Pion pion;

    private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE];
    private int indicePion = 0, nombreElementDansLigne=0;

    private Vector3 pos= new Vector3(0,5,0);



    // Start is called before the first frame update
    public bool Ajoute_Pion_Ligne(int couleur)
    {
        if(nombreElementDansLigne== Globales.NB_PION_LIGNE)
        { // La ligne est complète
            if(indicePion== Globales.NB_PION_LIGNE) { indicePion = 0; }

            // Detruire le pion avant d'en créer un nouveau
            Destroy(pions[indicePion]);

            pos = new Vector3(2 * indicePion, 5, 0);
            pions[indicePion] = pion.CreationPion(pos, couleur);

            //Debug.Log("4EL ELSE Dans Ligne création pion couleur: " + couleur);
            //Debug.Log("L'indice est: " + indicePion + " la position est :" + pos);

            indicePion++;

        }
        else
        {
            pos = new Vector3(2 * indicePion, 5, 0);

            pions[indicePion] = pion.CreationPion(pos, couleur);
            //Debug.Log("Dans Ligne création pion couleur: " + couleur);
            //Debug.Log("L'indice est: " + indicePion + " la position est :" + pos);

            indicePion++;
            nombreElementDansLigne++;
        }


        return (nombreElementDansLigne== Globales.NB_PION_LIGNE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
