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
		L_02,
		L_03,
		L_04,
		L_05,
		L_06,
		L_07,
		L_08,
		L_09
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
			{ Level.L_01, "Game_Level_01" },
			{ Level.L_02, "Game_Level_02" },
			{ Level.L_03, "Game_Level_03" },
			{ Level.L_04, "Game_Level_04" },
			{ Level.L_05, "Game_Level_05" },
			{ Level.L_06, "Game_Level_06" },
			{ Level.L_07, "Game_Level_07" },
			{ Level.L_08, "Game_Level_08" },
			{ Level.L_09, "Game_Level_09" },
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


	/// <summary>
	/// Allow loading any level (of the current ten) for testing purposes.
	/// </summary>
	void Update ()
	{
		if (Input.GetKeyDown ("0"))
			Load (Level.L_00);
		if (Input.GetKeyDown ("1"))
			Load (Level.L_01);
		if (Input.GetKeyDown ("2"))
			Load (Level.L_02);
		if (Input.GetKeyDown ("3"))
			Load (Level.L_03);
		if (Input.GetKeyDown ("4"))
			Load (Level.L_04);
		if (Input.GetKeyDown ("5"))
			Load (Level.L_05);
		if (Input.GetKeyDown ("6"))
			Load (Level.L_06);
		if (Input.GetKeyDown ("7"))
			Load (Level.L_07);
		if (Input.GetKeyDown ("8"))
			Load (Level.L_08);
		if (Input.GetKeyDown ("9"))
			Load (Level.L_09);
	}
}
