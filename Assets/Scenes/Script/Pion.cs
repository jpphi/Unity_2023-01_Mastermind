using UnityEngine;

public class Pion : PionMarqueCode
{
    [SerializeField] protected GameObject forme;
    [SerializeField] protected Material[] tableau_couleur= new Material[6];

    private GameObject pionpion;

    Quaternion rot = new Quaternion(0, 0, 0, 0);

    public GameObject CreationPion(Vector3 pos, int couleur)
    {

        pionpion = Instantiate<GameObject>(forme, pos, rot);
        pionpion.GetComponent<Renderer>().material = tableau_couleur[couleur];
        Debug.Log("Dans créationPion");
        //pion = Instantiate<GameObject>(forme, pos, rot);
        //pion.GetComponent<Renderer>().material = tableau_couleur[couleur];

        return pionpion;
    }


}
