using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
	private float x;
	private float y;
	private float z;

	// Use this for initialization
	void Start ()
	{
		x = Random.value;
		y = Random.value;
		z = Random.value;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (x, y, z);
	}
}
