using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 15.0f;

    void Start()
    {
      
    }


    void Update()
    {
		var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
       
        transform.Translate(x * speed * Time.deltaTime, 0, z * speed * Time.deltaTime);
    }
}
