using UnityEngine;

public class ParticleEffectContainer : MonoBehaviour
{
	private static Transform container;

	void Start ()
	{
		container = gameObject.transform;
	}


	public static void Contain (GameObject obj)
	{
		obj.transform.parent = container;
	}
}
