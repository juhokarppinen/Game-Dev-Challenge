using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	public AudioClip ballBouncesFromWall;
	public AudioClip ballBouncesFromPaddle;
	public AudioClip ballBouncesFromBrick;
	public AudioClip ballLost;
	public AudioClip paddleSmaller;
	public AudioClip explosion;
	public AudioClip win;
	public AudioClip lose;


	public enum Sound
	{
		BallBouncesFromWall,
		BallBouncesFromPaddle,
		BallBouncesFromBrick,
		BallLost,
		PaddleSmaller,
		Explosion,
		Win,
		Lose
	}


	public static Dictionary<Sound, AudioClip> sounds;


	void Start ()
	{
		sounds = new Dictionary<Sound,AudioClip> {	
			{ Sound.BallBouncesFromWall, ballBouncesFromWall },
			{ Sound.BallBouncesFromPaddle, ballBouncesFromPaddle },
			{ Sound.BallBouncesFromBrick, ballBouncesFromBrick },
			{ Sound.BallLost, ballLost },
			{ Sound.PaddleSmaller, paddleSmaller },
			{ Sound.Explosion, explosion },
			{ Sound.Win, win },
			{ Sound.Lose, lose }
		};
	}


	/**
	 * A simple facade for the AudioSource API. A Sound enum is passed as an argument
	 * instead of a concrete AudioClip reference.
	 */
	public static void Play (Sound sound, Vector3 position)
	{
		AudioSource.PlayClipAtPoint (sounds [sound], position);
	}
}
