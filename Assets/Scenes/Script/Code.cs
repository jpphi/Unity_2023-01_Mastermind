using UnityEngine;

public class Code : PionMarqueCode
{
    [SerializeField] protected GameObject forme;
    [SerializeField] protected GameObject coffre;
    //[SerializeField] protected Material[] tableau_couleur = new Material[6];

    private GameObject[] pions = new GameObject[Globales.NB_PION_LIGNE];

    public void GenereCode(int[] couleurs)
    {

        Quaternion rot = new Quaternion(0, 0, 0, 0);

        Debug.Log("LA !!!");

        //Material m_Material;

        for(int i = 0; i< couleurs.Length; i++)
        {
            Vector3 offset = new Vector3(2*i - (int)(Globales.NB_PION_LIGNE/2)-1, 0, 0);

            Vector3 pos = coffre.transform.position + offset;

            pions[i] = Instantiate<GameObject>(forme, pos, rot);
            pions[i].GetComponent<Renderer>().material = tableau_couleur[couleurs[i]];


        }




        //m_Material = pion.GetComponent<Renderer>().material;

        //Debug.Log("Position : " + pos);
        //balle.GetComponent<Rigidbody>().velocity = ProjectilePosition.transform.up * force;

        //    public Material[] tabMaterial;
        //public int index;

    }

    public void Triche()
    {
        Color color = coffre.GetComponent<Renderer>().material.color;

        color.a = 0;

        coffre.GetComponent<Renderer>().material.color = color;
        Debug.Log("Couleur: " + color);
    }

}
