using UnityEngine;
using System.Collections;

public class ExtraLifeCounter : MonoBehaviour
{
	public int oneUpEvery;

	private int counter;

	void Start ()
	{
		counter = oneUpEvery;
	}


	public bool UpdateCounter (int amount)
	{
		counter -= amount;
		if (counter <= 0) {
			counter += oneUpEvery;
			return true;
		} else {
			return false;
		}
	}
}
