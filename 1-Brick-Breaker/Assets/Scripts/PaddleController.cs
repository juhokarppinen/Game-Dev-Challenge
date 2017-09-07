using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour
{
	public float speedScale;
	public bool autoPlay;
	public GameObject explosion;
	public AudioClip explosionSound;

	private const float playAreaWidth = 14f;
	private new Renderer renderer;
	private BallController ball;
	private GameObject particleSystemParent;

	public float X {
		get { return transform.position.x; }
	}

	private float Y {
		get { return transform.position.y; }
	}

	private float Z {
		get { return transform.position.z; }
	}

	private float HalfSize {
		get { return renderer.bounds.extents.x; }
	}

	private float Left {
		get { return X - HalfSize; }
		set { transform.position = new Vector3 (value + HalfSize, Y, Z); }
	}

	private float Right {
		get { return X + HalfSize; }
		set { transform.position = new Vector3 (value - HalfSize, Y, Z); }
	}

	void Start ()
	{
		particleSystemParent = GameObject.Find ("Particle Effects");
		if (!particleSystemParent) {
			particleSystemParent = new GameObject ("Particle Effects");
		}

		renderer = GetComponentInChildren<Renderer> ();
		ball = FindObjectOfType<BallController> ();
	}

	void FixedUpdate ()
	{
		float horizontalMovement = Input.GetAxisRaw ("Horizontal");
		float x = X + horizontalMovement * Time.deltaTime * speedScale;
		if (autoPlay) {
			x = ball.transform.position.x;
		}
		transform.position = new Vector3 (x, Y, Z);
		if (Left < -playAreaWidth)
			Left = -playAreaWidth;
		if (Right > playAreaWidth)
			Right = playAreaWidth;
	}

	public void Explode ()
	{
		ball.ShouldBeReplaced = false;
		GameObject newExplosion = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		newExplosion.transform.SetParent (particleSystemParent.transform);
		AudioSource.PlayClipAtPoint (explosionSound, transform.position);
		gameObject.SetActive (false);
	}
}
