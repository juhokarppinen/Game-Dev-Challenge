﻿using UnityEngine;

public class PowerBall : MonoBehaviour
{
	private float powerTime = 5.0f;
	private int health = 10;

	void Update ()
	{
		powerTime -= Time.deltaTime;
		if (powerTime <= 0) {
			GameObject.Destroy (gameObject);
		}
	}


	public void BrickHit ()
	{
		health -= 1;
		if (health <= 0)
			Destroy (gameObject);
	}
}
