using MidiJack;
using UnityEngine;
using System.Collections;

public class MusicMatInput : MonoBehaviour
{
	private int LEFT = 64;
	private int RIGHT = 67;
	private int FRONT = 59;
	private int BACK = 62;
	private int LEFT_HARD = 72;
	private int RIGHT_HARD = 55;
	private int FRONT_HARD = 57;
	private int BACK_HARD = 69;
	private int SHIELD_1 = 65;
	private int SHIELD_2 = 53;
	private int SHIELD_3 = 71;
	private int SHIELD_4 = 60;


	public bool InputFrontLeft ()
	{
		return ((Key (FRONT) && Key (LEFT)) || (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow)));
	}


	public  bool InputFrontRight ()
	{
		return ((Key (FRONT) && Key (RIGHT)) || (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow)));
	}


	public  bool InputBackLeft ()
	{
		return ((Key (BACK) && Key (LEFT)) || (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.LeftArrow)));
	}


	public  bool InputBackRight ()
	{
		return ((Key (BACK) && Key (RIGHT)) || (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.RightArrow)));
	}


	public  bool InputLeft ()
	{
		return (Key (LEFT) || Input.GetKey (KeyCode.LeftArrow));
	}


	public  bool InputRight ()
	{
		return (Key (RIGHT) || Input.GetKey (KeyCode.RightArrow));
	}


	public  bool InputFront ()
	{
		return (Key (FRONT) || Input.GetKey (KeyCode.UpArrow));
	}


	public  bool InputBack ()
	{
		return (Key (BACK) || Input.GetKey (KeyCode.DownArrow));
	}


	public  bool InputLeftHard ()
	{
		return (Key (LEFT_HARD) || Input.GetKey (KeyCode.A));
	}


	public  bool InputRightHard ()
	{
		return (Key (RIGHT_HARD) || Input.GetKey (KeyCode.D));
	}


	public  bool InputFrontHard ()
	{
		return (Key (FRONT_HARD) || Input.GetKey (KeyCode.W));
	}


	public bool InputBackHard ()
	{
		return (Key (BACK_HARD) || Input.GetKey (KeyCode.S));
	}


	public bool InputShield ()
	{
		return (Key (SHIELD_1) || Key (SHIELD_2) || Key (SHIELD_3) || Key (SHIELD_4) || Input.GetKey (KeyCode.Space));
	}


	private bool Key (int note)
	{
		return MidiMaster.GetKey (MidiChannel.Ch1, note) > 0;
	}
}
