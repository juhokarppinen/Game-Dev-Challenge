using UnityEngine;

/// <summary>
/// Menu manager.
/// </summary>
public class MenuManager : MonoBehaviour
{
	public GameObject stateManager;

	/// <summary>
	/// Makes sure that any excess StateManagers are destroyed.
	/// </summary>
	void Awake ()
	{
		StateManager state = FindObjectOfType<StateManager> ();
		if (state) {
			state.Destroy ();
		}
	}


	/// <summary>
	/// Starts a new game and instantiates a new StateManager.
	/// </summary>
	public void StartGame ()
	{
		Instantiate (stateManager);
		FindObjectOfType<LevelManager> ().Load (LevelManager.Level.FIRST_LEVEL);
	}
}
