using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour
{
	private AudioSource source;


	void Start ()
	{
		source = GetComponent<AudioSource> ();
	}


	public void StartThrust ()
	{
		if (!source.isPlaying)
			source.Play ();
	}


	public void StopThrust ()
	{
		source.Stop ();
	}
}
