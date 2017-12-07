using UnityEngine;

/// <summary>
/// Score multiplier. If the player manages to break enough bricks without losing a ball, increase the multiplier.
/// The amount of breaks needed can be set in the editor.
/// </summary>
public class ScoreMultiplier : MonoBehaviour
{
	public int breaksNeeded;

	private int counter;
	private int multiplier;
	private MultiplierText multiplierText;

	public int Value {
		get { return multiplier; }
	}


	void Start ()
	{
		multiplierText = FindObjectOfType<MultiplierText> ();
		Reset ();
	}


	/// <summary>
	/// Add one break to the counter. Increase multiplier if counter value gets big enough.
	/// </summary>
	public void Add ()
	{
		counter += 1;
		if (counter >= breaksNeeded) {
			counter -= breaksNeeded;
			multiplier += 1;
			multiplierText.UpdateText (multiplier);
		}
	}


	/// <summary>
	/// Reset the multiplier and the counter.
	/// </summary>
	public void Reset ()
	{
		counter = 0;
		multiplier = 1;
		multiplierText.UpdateText (multiplier);
	}
}
