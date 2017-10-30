using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
	Renderer r;
	Color colorRaised;
	Color colorLowered;
	AudioSource source;

	
	void Start ()
	{
		r = GetComponent<Renderer> ();
		colorRaised = r.material.color;
		colorLowered = new Color (0, 0, 0, 0);
		source = GetComponent<AudioSource> ();
		Lower ();
	}


	public void Raise ()
	{
		r.material.color = colorRaised;
		if (!source.isPlaying)
			GetComponent<AudioSource> ().Play ();
	}


	public void Lower ()
	{
		r.material.color = colorLowered;
		GetComponent<AudioSource> ().Stop ();
	}
}
