using UnityEngine;
using UnityEngine.UI;

public class MultiplierText : MonoBehaviour
{
	public void UpdateText (int multiplier)
	{
		GetComponent<Text> ().text = "x" + multiplier;
	}
}
