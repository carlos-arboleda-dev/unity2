using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_clase : MonoBehaviour
{
    // Start is called before the first frame update

    public float posX = 11.9f;
    public float posY = 18.4f;
    public float posZ = -34.2f;

    void Start()
        
    {
        //Debug.Log("Mi nombre es: " + name);
        //Debug.Log("Posicion en x: " + transform.position.x);
        //Debug.Log("Posicion actual es: " + transform.position);

        transform.position = new Vector3(11.9f, 18.4f, -34.2f);
        Debug.Log("Nueva posicion camara: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hello world Unity engine");
    }
}
