using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	public void DestroyIn (float seconds)
	{
		Invoke ("Destroy", seconds);
	}


	private void Destroy ()
	{
		Destroy (gameObject);	
	}
}
