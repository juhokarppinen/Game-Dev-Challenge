using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Manages level / scene changes.
/// </summary>
public class LevelManager : MonoBehaviour
{
	public enum Level
	{
		MENU,
		FIRST_LEVEL,
		L_00,
		L_01,
		GAMEWIN,
		GAMEOVER
	}

	public static Dictionary<Level,string> levels;


	/// <summary>
	/// Build the level dictionary using the Level enum and scene names.
	/// </summary>
	void Start ()
	{
		levels = new Dictionary<Level,string> {
			{ Level.MENU, "Main_Menu" },
			{ Level.FIRST_LEVEL, "Game_Level_00" },
			{ Level.L_00, "Game_Level_00" },
			{ Level.L_01, "Game_Level_00" },
			{ Level.GAMEOVER, "Main_Menu" },
			{ Level.GAMEWIN, "Main_Menu" }
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
