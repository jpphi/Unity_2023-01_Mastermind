using UnityEngine;

public class Pion : PionMarqueCode
{
    [SerializeField] protected GameObject forme;
    [SerializeField] protected Material[] tableau_couleur= new Material[6];

    private GameObject pion;

    Quaternion rot = new Quaternion(0, 0, 0, 0);

    public void CreationPion(Vector3 pos, int couleur)
    {

        pion = Instantiate<GameObject>(forme, pos, rot);
        pion.GetComponent<Renderer>().material = tableau_couleur[couleur];
        Debug.Log("Dans créationPion");
        //pion = Instantiate<GameObject>(forme, pos, rot);
        //pion.GetComponent<Renderer>().material = tableau_couleur[couleur];
    }


}
