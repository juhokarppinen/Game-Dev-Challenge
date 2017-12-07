using UnityEngine;

/// <summary>
/// Menu manager.
/// </summary>
public class MenuManager : MonoBehaviour
{
	/// <summary>
	/// If a StateManager instance still persists from a previous game, destroy it before starting a new game.
	/// </summary>
	public void StartGame ()
	{
		StateManager stateManager = FindObjectOfType<StateManager> ();
		if (stateManager)
			Destroy (stateManager.gameObject);
		
		FindObjectOfType<LevelManager> ().Load (LevelManager.Level.FIRST_LEVEL);
	}


	/// <summary>
	/// Return to the main menu.
	/// </summary>
	public void ReturnToMain ()
	{
		FindObjectOfType<LevelManager> ().Load (LevelManager.Level.MAIN_MENU);
	}
}
