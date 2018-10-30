using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 5.0f;

    void Start()
    {
        
    }


    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

       
        transform.Translate(x * speed, 0, z * speed);
    }
}
