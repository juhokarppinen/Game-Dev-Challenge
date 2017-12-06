using UnityEngine;

public class Score : IntegerAmount
{
	private ScoreText score;

	void Awake ()
	{
		score = FindObjectOfType<ScoreText> ();
	}


	public override void Add (int amount)
	{
		base.Add (amount);
		score.UpdateText (this.amount);
	}


	public override void Set (int amount)
	{
		base.Set (amount);
		score.UpdateText (this.amount);
	}
}
