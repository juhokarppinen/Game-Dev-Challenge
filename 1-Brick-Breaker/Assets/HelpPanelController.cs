using UnityEngine;
using System.Collections;

public class HelpPanelController : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetButtonDown ("Launch"))
			GetComponent<Animator> ().SetTrigger ("Space");
	}
}
