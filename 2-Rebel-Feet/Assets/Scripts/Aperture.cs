using UnityEngine;
using System.Collections;

public class Aperture : MonoBehaviour
{
	private GameManager gameManager;


	void Start ()
	{
		gameManager = FindObjectOfType<GameManager> (); // GameManager.instance;
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			gameManager.PlayerPassedObstacle (this);
		}
	}
}
