using UnityEngine;
using System.Collections;

public class HelpPanelController : MonoBehaviour
{
	private InputManager input;

	void Awake ()
	{
		input = GetComponent<InputManager> ();
	}


	void Update ()
	{
		if (input.LaunchButtonDown)
			GetComponent<Animator> ().SetTrigger ("Space");
	}
}
