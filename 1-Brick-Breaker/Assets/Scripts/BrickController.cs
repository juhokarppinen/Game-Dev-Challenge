using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour
{
	public int scoreValue;
	public int health;

	private static int count = 0;

	private int maxHealth;
	private Material m;

	void Start ()
	{
		count++;
		maxHealth = health;
		m = GetComponent<Renderer> ().material;
	}


	/// <summary>
	/// Damage the brick on collision. Currently only the ball can collide with the bricks.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter (Collision collision)
	{
		health -= 1;
		if (health <= 0) {
			DestroyBrick ();
		} else {
			SetColor ();
		}
	}


	/// <summary>
	/// Destroy the brick and add to score.
	/// </summary>
	private void DestroyBrick ()
	{
		GameManager.AddToScore (scoreValue);
		count--;
		if (count <= 0)
			GameManager.Win ();
		GetComponent<Explodable> ().Explode (transform.position);
		Destroy (gameObject);
	}


	/// <summary>
	/// Darken the brick's color logarithmically.
	/// </summary>
	private void SetColor ()
	{
		float p = 0.5f;
		float s = Mathf.Pow ((1.0f * health) / (1.0f * maxHealth), p);
		float r = m.color.r * s;
		float g = m.color.g * s;
		float b = m.color.b * s;
		m.color = new Color (r, g, b, 1);
	}


	/// <summary>
	/// Deal damage to all bricks.
	/// </summary>
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.T))
			OnCollisionEnter (null);
	}
}
