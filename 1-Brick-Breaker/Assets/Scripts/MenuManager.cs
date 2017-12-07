using UnityEngine;

/// <summary>
/// Menu manager. Handles the creation and destruction of the StateManager.
/// </summary>
public class MenuManager : MonoBehaviour
{
	public GameObject stateManager;

	/// <summary>
	/// Remove the previous StateManager and instantiate a new one before starting a new game.
	/// </summary>
	public void StartGame ()
	{
		Destroy (FindObjectOfType<StateManager> ());
		Instantiate (stateManager);

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
