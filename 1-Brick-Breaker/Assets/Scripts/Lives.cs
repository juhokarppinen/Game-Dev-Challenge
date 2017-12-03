using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lives : IntegerAmount
{
	public int lives;
	public GameObject counter;

	private Stack<GameObject> counters;

	void Start ()
	{
		amount = lives;
	}


	public override void Remove (int amount)
	{
		base.Remove (amount);
		if (this.amount <= 0) {
			GameManager.GameOver ();
		}
	}
}
