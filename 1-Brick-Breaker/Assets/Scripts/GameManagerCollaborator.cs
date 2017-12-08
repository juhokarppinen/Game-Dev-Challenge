using UnityEngine;

public class GameManagerCollaborator : MonoBehaviour
{
	protected GameManager gameManager;

	public void SetGameManager (GameManager gm)
	{
		gameManager = gm;
	}
}
