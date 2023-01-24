using UnityEngine;
//using UnityEngine.UIElements;

public class Pion : MonoBehaviour
{
    [SerializeField] protected GameObject forme;
    [SerializeField] protected Material[] tableau_couleur= new Material[6];
    [SerializeField] protected GameObject plat;

    private GameObject pionpion;

    Quaternion rot = new Quaternion(0, 0, 0, 0);

    public GameObject CreationPion(Vector3 pos, int couleur)
    {
        pionpion = Instantiate<GameObject>(forme, plat.transform);

        pionpion.transform.localPosition = pos; //position

        pionpion.GetComponent<Renderer>().material = tableau_couleur[couleur];
        // Debug.Log("<Pion.CreationPion> position: " + pos + " Couleur : " + couleur);

        return pionpion;
    }


}
