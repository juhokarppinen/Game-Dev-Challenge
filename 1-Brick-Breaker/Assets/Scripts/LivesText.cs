using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is responsible for updating the Lives UI element. As it stands, 
/// only three life counters are displayed, even though the player can have more 
/// than three lives.
/// </summary>
public class LivesText : MonoBehaviour
{
	public GameObject lifeCounter;
	private GameObject[] containers;
	private Stack<GameObject> counters;
	private int lives;


	void Start ()
	{
		lives = transform.childCount;
		containers = new GameObject[lives];
		counters = new Stack<GameObject> ();

		for (int i = 0; i < lives; i++) {
			containers [i] = transform.GetChild (i).gameObject;
			CreateNewCounter ();
		}
	}


	public void UpdateText (int lives)
	{
		if (lives < counters.Count) {
			Remove ();
		} else {
			Add ();
		}
	}


	private void Remove ()
	{
		GameObject lastCounter = counters.Pop ();
		Vector3 position = lastCounter.transform.position;
		lastCounter.GetComponent<Explodable> ().Explode (position);
		Destroy (lastCounter);
	}


	private void Add ()
	{
		if (counters.Count >= lives) {
			return;
		} else {
			CreateNewCounter ();
		}
	}


	private void CreateNewCounter ()
	{
		GameObject newCounter = (GameObject)Instantiate (lifeCounter);
		counters.Push (newCounter);
		int index = counters.Count - 1;
		newCounter.transform.position = containers [index].transform.position;
		newCounter.transform.parent = containers [index].transform;
	}
}
