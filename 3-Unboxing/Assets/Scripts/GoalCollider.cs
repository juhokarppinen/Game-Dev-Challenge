using UnityEngine;

public class GoalCollider : MonoBehaviour
{
	private GameManager gameManager;

	void Start ()
	{
		gameManager = FindObjectOfType<GameManager> ();
	}


	void OnTriggerEnter (Collider other)
	{
		gameManager.LevelComplete ();
	}

}
