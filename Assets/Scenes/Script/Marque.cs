
using UnityEngine;

public class Marque : PionMarqueCode
{
    [SerializeField] protected GameObject forme;
    [SerializeField] protected Material[] TableauCouleurMarque = new Material[2];

    private GameObject marqueMarque;

    private Quaternion rot = new Quaternion(0, 0, 0, 0);

    public GameObject CreationMarque(Vector3 pos, int couleur)
    {

        marqueMarque = Instantiate<GameObject>(forme, pos, rot);
        marqueMarque.GetComponent<Renderer>().material = TableauCouleurMarque[couleur];
        Debug.Log("Dans créationPion");
        //pion = Instantiate<GameObject>(forme, pos, rot);
        //pion.GetComponent<Renderer>().material = tableau_couleur[couleur];

        return marqueMarque;
    }



}
