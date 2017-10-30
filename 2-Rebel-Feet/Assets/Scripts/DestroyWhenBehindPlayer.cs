using UnityEngine;
using System.Collections;

public class DestroyWhenBehindPlayer : MonoBehaviour
{
	private GameManager gameManager;
	private float destructionOffset = 100;


	void Start ()
	{
		gameManager = FindObjectOfType<GameManager> ();
	}


	void Update ()
	{
		if (transform.position.z <= gameManager.PlayerPos.z - destructionOffset)
			Destroy (gameObject);
	}
}