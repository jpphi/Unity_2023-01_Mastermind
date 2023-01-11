using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private float speed = 40f;
    //private Vector3 direction = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
        transform.Rotate(0f, speed * Time.deltaTime * Input.GetAxis("Horizontal"), 0f);
    }
}