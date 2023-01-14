using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    // Start is called before the first frame update
 
    public delegate void IADecode(int score); //int score
    public event IADecode OnIADecode;
    [SerializeField] public GestionPartie gp;
    //[SerializeField] public GestionPartie gp;
    //public GestionPartie gp;

    //public GestionPartie gp = new GestionPartie();

    private void Start()
    {
        // Initialisation de l'univers des possibles

        OnIADecode += new IADecode(gp.recup);

    }
    public void lancerIA()
    {
        int[] proposition = new int[Globales.NB_PION_LIGNE];
        int[] reponse = new int[Globales.NB_PION_LIGNE];

        //int[] propositionInitiale = new int[Globales.NB_PION_LIGNE];

        //Debug.Log("<Decode.lancerIA> ");

        // 1ère proposition
        for (int i = 0; i < proposition.Length; proposition[i] = i, i++) ;

        while(true)
        {
            //reponse= Ligne.compareTableau(proposition, );
            break;
        }

        OnIADecode.Invoke(51165);

        //OnIADecode= recup; // 51165
        //recup(51165);

    }



    private void rechercheCode()
    {
        Debug.Log("<Decode.rechercheCode> ");

    }

}



/*
 using UnityEngine;
using System.Collections;

public delegate void SliderChangedHandler(float val);

public class testEvent : MonoBehaviour {

 public event SliderChangedHandler SliderChanged; 
 float sliderValue = 0F;

 public void Start(){
    SliderChanged += new SliderChangedHandler(updateCubePos); 
 }

 public void fireSliderChanged(float val){
    if(SliderChanged != null ){
       SliderChanged(val);
    }
 }

 void OnGUI(){
    float oldSliderValue = sliderValue;
    sliderValue = GUI.HorizontalSlider(new Rect(Screen.width*0.1F, Screen.height*0.1F, Screen.width*0.2F, Screen.height*0.05F),sliderValue,0,10);
    if(sliderValue!=oldSliderValue){
       fireSliderChanged(sliderValue); 
    } 
 }

 void updateCubePos(float val){
    //exemple de gestion de l'évènement
    Vector3 pos = GameObject.Find("Cube").transform.position; 
    pos.x = val;
    GameObject.Find("Cube").transform.position = pos;
 }

}
 
 
 */
