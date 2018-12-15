using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    private Vector3 rotationEuler;
    [SerializeField]
    private int degrees = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        rotationEuler += Vector3.forward * degrees * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotationEuler);
	}
}
