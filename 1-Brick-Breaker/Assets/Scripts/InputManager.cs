using UnityEngine;

public class InputManager : MonoBehaviour
{
	private const string LAUNCH_BUTTON = "Launch";
	private const string MOUSE_AXIS_X = "Mouse X";
	private const string CONTROLLER_AXIS_X = "Horizontal";

	public bool LaunchButtonDown {
		get { return Input.GetButtonDown (LAUNCH_BUTTON); }
	}


	public float MouseAxisX {
		get { return Input.GetAxisRaw (MOUSE_AXIS_X); }
	}


	public float ControllerAxisX {
		get { return Input.GetAxisRaw (CONTROLLER_AXIS_X); }
	}
}
