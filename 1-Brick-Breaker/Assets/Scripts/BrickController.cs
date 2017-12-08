using UnityEngine;

public class BrickController : MonoBehaviour
{
	public int scoreValue;
	public int health;
	public GameObject powerUpPowerBall;
	public GameObject powerDownHalfSize;

	private int maxHealth;
	private Material m;

	void Start ()
	{
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
		if (Random.value > .90f) {
			DropItem ();
		}

		GameManager.AddToScore (scoreValue);
		
		GetComponent<Explodable> ().Explode (transform.position);
		Destroy (gameObject);
	}


	/// <summary>
	/// Select an item to drop based on a predefined probability table.
	/// </summary>
	private void DropItem ()
	{
		GameObject item;
		float value = Random.value;
		if (value >= 0.75f) {
			item = powerUpPowerBall;
		} else {
			item = powerDownHalfSize;
		}

		Instantiate (
			item, 
			new Vector3 (transform.position.x, .9f, transform.position.z), 
			Quaternion.Euler (0, 0, 90));
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
	/// REMOVE FROM PRODUCTION VERSION
	/// 
	/// Deal damage to all bricks.
	/// </summary>
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.T))
			OnCollisionEnter (null);
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("PowerBall")) {
			DestroyBrick ();
			other.GetComponent<PowerBall> ().BrickHit ();
		}
	}
}
