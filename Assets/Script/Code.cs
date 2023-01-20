using UnityEngine;

public class Code : MonoBehaviour
{
    [SerializeField] protected GameObject forme;
    [SerializeField] protected GameObject coffre;
    //[SerializeField] protected GameObject plat;
    [SerializeField] protected Material[] tableau_couleur = new Material[6];

    private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE];
    private int[] tableau_couleur_code= new int[Globales.NB_PION_LIGNE];

    public void GenereCode()
    {

        Quaternion rot = new Quaternion(0, 0, 0, 0);

        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            tableau_couleur_code[i] = Random.Range(0, Globales.NB_COULEURS);
        }

        ////  BLUE_COLOR CYAN_COLOR GREEN_COLOR PURPLE_COLOR RED_COLOR YELLOW_COLOR
        //tableau_couleur_code[0] = Globales.YELLOW_COLOR;
        //tableau_couleur_code[1] = Globales.BLUE_COLOR;
        //tableau_couleur_code[2] = Globales.YELLOW_COLOR;
        //tableau_couleur_code[3] = Globales.YELLOW_COLOR;

        //for (int i = 0; i < tableau_couleur_code.Length; Debug.Log("Code généré : " + tableau_couleur_code[i]), i++) ;

        for (int i = 0; i< tableau_couleur_code.Length; i++)
        {
            Vector3 offset = new Vector3(-2*i + Globales.NB_PION_LIGNE/2, 0, 0);

            Vector3 pos = coffre.transform.position + offset;

            pions[i] = Instantiate<GameObject>(forme, pos, rot);
            //pions[i] = Instantiate<GameObject>(forme, plat);
            pions[i].GetComponent<Renderer>().material = tableau_couleur[tableau_couleur_code[i]];

            // FAIRE COPIE TABLEAU EN C#
            //tableau_couleur_code[i]= couleurs[i];
        }
    }

    public void Triche()
    {
        Color color = coffre.GetComponent<Renderer>().material.color;

        if (color.a >= 1) color.a = 0;
        else color.a = 1;
        coffre.GetComponent<Renderer>().material.color = color;
    }

    public int[] compareTableau(int[] proposition)
    {
        int[] reponse = new int[proposition.Length];
        int[] code = new int[proposition.Length];
        int[] prop = new int[proposition.Length];
        int k = 0;

        //for (int i = 0; i < Globales.NB_PION_LIGNE; reponse[i] = -1, i++) ;
        
        // Init tableau
        for (int i = 0; i < Globales.NB_PION_LIGNE; code[i] = tableau_couleur_code[i], prop[i]= proposition[i], 
            reponse[i]= -1, i++) ;

        //for (int i = 0; i < Globales.NB_PION_LIGNE; Debug.Log("proposition[i]: " + proposition[i]), i++) ;

        //Debug.Log("<Code.compareTableau> proposition[0]= " + prop[0]);
        // On détermine les marques noires en comparant la proposition au code
        // Les boucles for imbriquée ne permette pas de réglé le problème généré par les combinaisons
        //   code / proposition 11XX X11X 
        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            if (prop[i] == code[i])
            { // La couleur est-elle bien placé ? 
                reponse[k++] = Globales.BLACK_COLOR;
                code[i] = -1; // Le cas est traitée, on écrase la valeur dans le code
                prop[i] = -2; // Le cas est traitée, on écrase la valeur dans la proposition faite
            }
        }
        //for (int i = 0; i < tableau_couleur_code.Length; Debug.Log("reponse aprés for N: " + reponse[i]), i++) ;

        // On a déterminée les marques noires, on détermine les marques blanches
        for (int i = 0; i < Globales.NB_PION_LIGNE; i++)
        {
            if (prop[i] == -2) continue;
            for (int j = 0; j < Globales.NB_PION_LIGNE; j++)
            {
                if (prop[i] == code[j])
                { // Si on trouve une couleur mal placé on le note et on sort de la boucle

                    reponse[k++] = Globales.WHITE_COLOR;
                    code[j] = -1; // Le cas est traitée, on écrase la valeur dans le code
                    prop[i] = -2; // Le cas est traitée, on écrase la valeur dans la proposition faite
                    break;
                }
            }
        }
        //for (int i = 0; i < Globales.NB_PION_LIGNE; Debug.Log("reponse aprés for N et B: " + reponse[i]), i++) ;

        return reponse;
    }


}
