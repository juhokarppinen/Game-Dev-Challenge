using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Mirror flip taken from:
 * https://answers.unity.com/questions/353680/mirror-flip-camera.html
 */
public class Mirror : MonoBehaviour
{
	Camera cam;

	void Start ()
	{
		cam = GetComponent<Camera> ();
	}

	void OnPreCull ()
	{
		cam.ResetWorldToCameraMatrix ();
		cam.ResetProjectionMatrix ();
		cam.projectionMatrix *= Matrix4x4.Scale (new Vector3 (-1, 1, 1));
	}

	void OnPreRender ()
	{
		GL.invertCulling = true;
//		GL.SetRevertBackfacing (true);
	}

	void OnPostRender ()
	{
		GL.invertCulling = false;
//		GL.SetRevertBackfacing (false);
	}
}
