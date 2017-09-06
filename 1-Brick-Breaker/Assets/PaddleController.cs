using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour
{
	public float speedScale;

	private const float playAreaWidth = 10f;
	private new Renderer renderer;

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
		renderer = GetComponentInChildren<Renderer> ();
	}

	void FixedUpdate ()
	{
		float horizontalMovement = Input.GetAxisRaw ("Horizontal");
		float x = X + horizontalMovement * Time.deltaTime * speedScale;
		transform.position = new Vector3 (x, Y, Z);
		if (Left < -playAreaWidth)
			Left = -playAreaWidth;
		if (Right > playAreaWidth)
			Right = playAreaWidth;
	}
}
