using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Centralised sound manager for sound effects not associated with particular game object behaviors.
/// </summary>
public class SoundManager : MonoBehaviour
{
	public AudioClip win;
	public AudioClip lose;


	public enum Sound
	{
		Win,
		Lose
	}


	public static Dictionary<Sound, AudioClip> sounds;


	void Start ()
	{
		sounds = new Dictionary<Sound,AudioClip> {	
			{ Sound.Win, win },
			{ Sound.Lose, lose }
		};
	}


	/// <summary>
	/// A simple facade for the AudioSource API. A Sound enum is passed as an argument
	/// instead of an actual AudioClip reference. An optional Vector3 can be passed 
	/// if a positional sound is required.
	/// </summary>
	/// <param name="sound">The sound to be played.</param>
	/// <param name="position">The position of the sound.</param>
	public static void Play (Sound sound, Vector3 position)
	{
		AudioSource.PlayClipAtPoint (sounds [sound], position);
	}


	/// <summary>
	/// A simple facade for the AudioSource API. A Sound enum is passed as an argument
	/// instead of an actual AudioClip reference. The sound is played at (0,0,0).
	/// </summary>
	/// <param name="sound">The sound to be played.</param>
	public static void Play (Sound sound)
	{
		Play (sound, Vector3.zero);
	}
}
