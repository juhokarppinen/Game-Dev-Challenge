using UnityEngine;

/// <summary>
/// Takes care of tracking lives and updating the UI element which displays them.
/// </summary>
public class Lives : MonoBehaviour
{
	private LivesText livesText;
	private int lives;

	void Awake ()
	{
		livesText = FindObjectOfType<LivesText> ();
	}


	void Start ()
	{
		livesText.UpdateText (lives);
	}


	/// <summary>
	/// Remove the specified amount of lives and update the UI element accordingly.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void Remove (int amount)
	{
		lives -= amount;
		livesText.UpdateText (lives);
	}


	/// <summary>
	/// Add the specified amount of lives and update the UI element accordingly.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void Add (int amount)
	{
		lives += amount;
		livesText.UpdateText (lives);
	}


	/// <summary>
	/// Set lives to the specified amount and update the associated UI element.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void Set (int amount)
	{
		lives = amount;
		livesText.UpdateText (lives);
	}


	public int Get ()
	{
		return lives;
	}
}
