using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

	// Use this for initialization
	void Start () {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform; //Fait en sorte de faire du Pivot un objet enfant de l'objet joueur dès le lancement du jeu

        Cursor.lockState = CursorLockMode.Locked;
	}

	
	// Update is called once per frame
	void Update () {

		//Prendre la position de la souris sur l'axe X et tourner le joueur
		float horizontal = 0.0f;
		if (gameObject.CompareTag("Player1"))
			horizontal = Input.GetAxis("Mouse X");
		else if (gameObject.CompareTag("Player2"))
			horizontal = Input.GetAxis("Mouse X2");
		target.Rotate(0, horizontal, 0);

		float vertical = 0.0f;
		if (gameObject.CompareTag("Player1"))
			vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
		else if (gameObject.CompareTag("Player2"))
			vertical = Input.GetAxis("Mouse Y2") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        //Bouger la caméra selon la rotation actuelle de la cible et l'offset de base
        float desiredYAngle = target.eulerAngles.y; //ca j ai encore un peu de mal à comprendre ce que c'est
        //float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset); //Ordre de la multiplication important

        //transform.position = target.position - offset;

        transform.LookAt(target);	
	}
}
