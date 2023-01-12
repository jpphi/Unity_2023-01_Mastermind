using UnityEngine;

public class Code : PionMarqueCode
{
    [SerializeField] protected GameObject forme;
    [SerializeField] protected GameObject coffre;
    [SerializeField] protected GameObject plat;
    [SerializeField] protected Material[] tableau_couleur = new Material[6];

    private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE];
    private int[] tableau_couleur_code= new int[Globales.NB_PION_LIGNE];

    public void GenereCode(int[] couleurs)
    {

        Quaternion rot = new Quaternion(0, 0, 0, 0);

        for(int i = 0; i< couleurs.Length; i++)
        {
            Vector3 offset = new Vector3(-2*i + (int)(Globales.NB_PION_LIGNE/2)-1, 0, 0);

            Vector3 pos = coffre.transform.position + offset;

            pions[i] = Instantiate<GameObject>(forme, pos, rot);
            //pions[i] = Instantiate<GameObject>(forme, plat);
            pions[i].GetComponent<Renderer>().material = tableau_couleur[couleurs[i]];

            // FAIRE COPIE TABLEAU EN C#
            tableau_couleur_code[i]= couleurs[i];
        }
    }

    public void Triche()
    {
        Color color = coffre.GetComponent<Renderer>().material.color;

        if (color.a >= 1) color.a = 0;
        else color.a = 1;
        coffre.GetComponent<Renderer>().material.color = color;
    }

}
