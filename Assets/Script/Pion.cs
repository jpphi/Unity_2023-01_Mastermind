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
        //float smooth = 2.0f;
        //Vector3 newScale = new Vector3(0.05f, 1f, 0.033f); // Les dimensions souhait�s

        //pionpion = Instantiate<GameObject>(forme, pos, rot);
        // On cr�e l'objet dans la hi�rarchie MasterMind (donc en tant qu'enfant) et pas dans plateau
        //   pour �viter le probl�me de la transmission du scale parent enfant
        pionpion = Instantiate<GameObject>(forme, plat.transform);

        pionpion.transform.localPosition = pos; //position

        pionpion.GetComponent<Renderer>().material = tableau_couleur[couleur];
        // Debug.Log("<Pion.CreationPion> position: " + pos + " Couleur : " + couleur);

        return pionpion;
    }


}
