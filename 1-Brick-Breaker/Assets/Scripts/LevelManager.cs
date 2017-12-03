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
		L_01
	}

	public static Dictionary<Level,string> levels;


	/// <summary>
	/// Build the level dictionary using the Level enum and scene names.
	/// </summary>
	void Start ()
	{
		levels = new Dictionary<Level,string> {
			{ Level.L_01, "Game_Level_01" }
		};
	}


	/// <summary>
	/// Load the specified level using the Level enum.
	/// </summary>
	/// <param name="level">Enumerated level.</param>
	public static void Load (Level level)
	{
		SceneManager.LoadScene (levels [level]);	
	}


	/// <summary>
	/// Load next level.
	/// </summary>
	public static void NextLevel ()
	{
		// TODO: Load Next Level
	}
}
