using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


/// <summary>
/// Manages level / scene changes.
/// </summary>
public class LevelManager : MonoBehaviour
{
	public enum Level
	{
		MAIN_MENU,
		GAMEWIN,
		GAMEOVER,
		FIRST_LEVEL,
		L_00,
		L_01,
	}

	public Dictionary<Level,string> levels;


	/// <summary>
	/// Build the level dictionary using the Level enum and scene names.
	/// </summary>
	void Start ()
	{
		levels = new Dictionary<Level,string> {
			{ Level.MAIN_MENU, "Menu_Main" },
			{ Level.GAMEOVER, "Menu_Main" },
			{ Level.GAMEWIN, "Menu_Win" },
			{ Level.FIRST_LEVEL, "Game_Level_00" },
			{ Level.L_00, "Game_Level_00" },
			{ Level.L_01, "Game_Level_00" }
		};
	}


	/// <summary>
	/// Load the specified level using the Level enum.
	/// </summary>
	/// <param name="level">Enumerated level.</param>
	public void Load (Level level)
	{
		SceneManager.LoadScene (levels [level]);	
	}


	/// <summary>
	/// Load next level.
	/// </summary>
	public void NextLevel ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
}
