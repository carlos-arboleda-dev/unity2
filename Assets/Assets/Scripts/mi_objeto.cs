using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mi_objeto : MonoBehaviour
{
    public float speed = 30.0f;
    public float posZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if(transform.position.z > posZ)
        {
            Destroy(this.gameObject);
        }
    }
}

//Time.deltaTime: regular el tiempo con el que transcurre