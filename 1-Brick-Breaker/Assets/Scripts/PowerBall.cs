using UnityEngine;

public class PowerBall : MonoBehaviour
{
	private float powerTime = 5.0f;

	void Update ()
	{
		powerTime -= Time.deltaTime;
		if (powerTime <= 0) {
			GameObject.Destroy (gameObject);
		}
	}
}
