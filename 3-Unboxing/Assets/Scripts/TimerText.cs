using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
	Text timerText;
	float time = 0;
	bool isRunning = false;

	void Start ()
	{
		timerText = GetComponent<Text> ();
	}


	void Update ()
	{
		if (isRunning) {
			time += Time.deltaTime;
			UpdateText ();
		}
	}


	private void UpdateText ()
	{
		timerText.text = "Current:\n" + FormatTime (time);
	}


	public string FormatTime (float t)
	{
		float m = Mathf.Floor (t / 60);
		float s = Mathf.Floor (t - m * 60);
		float c = Mathf.Floor ((t - m * 60 - s) * 100);

		return m.ToString ("00") +	":" + s.ToString ("00") + ":" +	c.ToString ("00");
	}


	public void ResetTimer ()
	{
		time = 0;
	}


	public void StartTimer ()
	{
		isRunning = true;
	}


	public void StopTimer ()
	{
		isRunning = false;
	}


	public float GetTime ()
	{
		return time;
	}
}
