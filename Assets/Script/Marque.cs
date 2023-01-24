using UnityEngine;

public class Marque : MonoBehaviour 
{
    [SerializeField] protected GameObject forme;
    [SerializeField] protected GameObject plateauMarque;
    [SerializeField] protected Material[] TableauCouleurMarque = new Material[2];

    private GameObject marqueMarque;

    private Quaternion rot = new Quaternion(0, 0, 0, 0);
    public GameObject CreationMarque(Vector3 pos, int couleur)
    {

        //marqueMarque = Instantiate<GameObject>(forme, pos, rot);
        marqueMarque = Instantiate<GameObject>(forme, plateauMarque.transform);

        marqueMarque.transform.localPosition = pos; //position

        marqueMarque.GetComponent<Renderer>().material = TableauCouleurMarque[couleur];
        
        //Debug.Log("<Marque.CreationMarque> position: " + pos + " Couleur : " + couleur);

        return marqueMarque;
    }
}
