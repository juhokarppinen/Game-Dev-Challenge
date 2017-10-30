using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Energy : MonoBehaviour
{
	private Slider slider;
	private float energy;
	public float freeEnergy = 10f;


	void Start ()
	{
		slider = GetComponent<Slider> ();
		energy = slider.minValue;
	}


	void Update ()
	{
		if (energy < freeEnergy)
			energy += Time.deltaTime;
		if (energy < slider.minValue)
			energy = slider.minValue;
		slider.value = energy;
	}


	public bool Consume (float amount)
	{
		energy -= amount;
		return energy > slider.minValue;
	}


	public void Restore (float amount)
	{
		energy += amount;
		if (energy > slider.maxValue)
			energy = slider.maxValue;
	}


	public float Amount ()
	{
		return energy;
	}


	public bool Peek (float amount)
	{
		return energy > slider.minValue;
	}
}
