using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Ligne : Pion
{
    [SerializeField] protected Pion pion;
    [SerializeField] protected Marque marque;
    [SerializeField] private GameObject plateau;

    private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE];
    private int[] TabCouleurPions = new int[Globales.NB_PION_LIGNE];
    private int indicePion = 0, nombreElementDansLigne = 0;


    private GameObject[] marques = new GameObject[Globales.NB_PION_LIGNE];
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
        if(nombreElementDansLigne== Globales.NB_PION_LIGNE)
        { // La ligne est complète
            if(indicePion== Globales.NB_PION_LIGNE) { indicePion = 0; }

            // Detruire le pion avant d'en créer un nouveau
            Destroy(pions[indicePion]);

            pos = new Vector3(2 * indicePion, 5, 0);
            pions[indicePion] = pion.CreationPion(pos, couleur);
            TabCouleurPions[indicePion] = couleur;

            //Debug.Log("4EL ELSE Dans Ligne création pion couleur: " + couleur);
            //Debug.Log("L'indice est: " + indicePion + " la position est :" + pos);

            indicePion++;

        }
        else
        {
            pos = new Vector3(2 * indicePion, 5, 0);

            pions[indicePion] = pion.CreationPion(pos, couleur);
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
        Debug.Log("< Fonction CheckResult > " + transform.position);

        // POSER LA LIGNE AVEC LES MARQUES

        // On pose la ligne
        for(int i=0; i<Globales.NB_PION_LIGNE; i++)
        {
            pions[i].transform.position =  plateauPosition + new Vector3(9 - 2 * i, 1, profondeur );
        }

        // On détermine les marques en comparant la proposition au code

        for (int i = 0; i < Globales.NB_PION_LIGNE; i++,TabReponse[k] = -1); // Init tableau

        for(int i=0; i < Globales.NB_PION_LIGNE;i++)
        {
            if (TabCouleurPions[i] == TabCouleurCode[i])
            { // La couleur est-elle bien placé ? 
                TabReponse[k++]= Globales.BLACK_COLOR;
            }
            else
            { // La couleur n'est pas bien placé, mais existe t'elle dans le code
                for(int j= 0; j< Globales.NB_PION_LIGNE;j++)
                {
                    if (TabCouleurPions[i] == TabCouleurCode[j])
                    { // Si on trouve une couleur mal placé on le note et on sort de la boucle

                        TabReponse[k++] = Globales.WHITE_COLOR;
                        break;
                    }
                }

            }

        }


        // Affichons le tableau des marques
        for (int i = 0; (i < Globales.NB_PION_LIGNE) && (TabReponse[i] != -1); k++)
        {
            pos = new Vector3(2 * indicePion, 5, 0);
            marques[k] = marque.CreationMarque(pos, TabReponse[i]);
        }

        //Debug.Log("");

        // On décale pour la ligne suivante
        profondeur += 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
