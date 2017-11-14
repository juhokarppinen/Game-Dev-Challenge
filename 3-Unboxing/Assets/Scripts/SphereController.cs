using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody> ().useGravity = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
