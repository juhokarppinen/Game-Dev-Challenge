using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
	public enum Level
	{
		L_01
	}

	public static Dictionary<Level,string> levels;


	void Start ()
	{
		levels = new Dictionary<Level,string> {
			{ Level.L_01, "Game_Level_01" }
		};
	}


	void Update ()
	{
	
	}


	public static void Load (Level level)
	{
		SceneManager.LoadScene (levels [level]);	
	}


	public static void Next ()
	{
		
	}
}
