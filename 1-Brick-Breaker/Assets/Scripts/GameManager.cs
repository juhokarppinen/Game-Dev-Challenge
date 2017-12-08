using UnityEngine;

/// <summary>
/// A global facade / messenger class for managing various object interactions in a centralised fashion.
/// All public methods are so that they can be called without an explicit object reference.
/// Will instantiate a new StateManager if it doesn't find one.
/// </summary>
public class GameManager : MonoBehaviour
{
	public int maxLives;
	public StateManager stateManager;

	private StateManager state;
	private LevelManager level;
	private PaddleController paddle;
	private GameOverController gameOver;
	private Lives lives;
	private Score score;
	private ScoreMultiplier scoreMultiplier;
	private ExtraLifeCounter oneUpCount;
	private bool won = false;


	/// <summary>
	/// Set object references and pass this reference to objects. 
	/// If a StateManager isn't found, instantiate a new one.
	/// </summary>
	void Awake ()
	{
		state = FindObjectOfType<StateManager> ();
		if (!state) {
			state = Instantiate (stateManager).GetComponent<StateManager> ();
			state.Lives = maxLives;
		}

		level = FindObjectOfType<LevelManager> ();
		paddle = FindObjectOfType<PaddleController> ();
		paddle.SetGameManager (this);

		gameOver = FindObjectOfType<GameOverController> ();
		gameOver.SetGameManager (this);

		lives = GetComponent<Lives> ();
		score = GetComponent<Score> ();
		scoreMultiplier = GetComponent<ScoreMultiplier> ();
		oneUpCount = GetComponent<ExtraLifeCounter> ();
	}


	/// <summary>
	/// Load the player's state from StateManager before calculating the first update frame.
	/// </summary>
	void Start ()
	{
		FetchState ();
	}


	public void BallLost (bool noBallsInPlay)
	{
		if (noBallsInPlay) {
			scoreMultiplier.Reset ();
			paddle.MakeNormal ();
		}

		if (noBallsInPlay && !HasLives ()) {
			GameOver ();
		}
	}


	public void AddToScore (int scoreValue)
	{
		int points = scoreValue * scoreMultiplier.Value;
		score.Add (points);
		scoreMultiplier.Add ();
		if (oneUpCount.UpdateCounter (points)) {
			lives.Add (1);
		}
	}


	public bool HasLives ()
	{
		return (lives.Get () > 0);
	}


	public void DecrementLives ()
	{
		lives.Remove (1);
	}

	
	public void GameOver ()
	{
		gameOver.Lift ();
		paddle.Explode ();
	}


	public void Win ()
	{
		won = true;
		lives.Add (BallController.RetrieveBallsInPlay ());
		StoreState ();
		SoundManager.Play (SoundManager.Sound.Win);
		Invoke ("LoadNext", 2);
	}


	private void LoadNext ()
	{
		level.NextLevel ();
	}


	public void LoadLevel (LevelManager.Level lvl)
	{
		level.Load (lvl);
	}


	/// <summary>
	/// Store score, lives and 1upCounter value to the StateManager.
	/// </summary>
	private void StoreState ()
	{
		state.Score = score.Get ();
		state.Lives = lives.Get ();
		state.OneUpCount = oneUpCount.Get ();
	}


	/// <summary>
	/// Fetch score, lives and 1upCounter value from the StateManager.
	/// </summary>
	private void FetchState ()
	{
		SetScore (state.Score);
		SetLives (state.Lives);
		SetOneUpCount (state.OneUpCount);
	}

		
	public void SetScore (int scoreValue)
	{
		score.Set (scoreValue);
	}


	public void SetLives (int livesValue)
	{
		lives.Set (livesValue);
	}


	public void SetOneUpCount (int countValue)
	{
		oneUpCount.Set (countValue);
	}


	/// <summary>
	/// Check if there are any bricks on the playfield. FindObjectOfType is used here, because 
	/// using a static integer in the brick class for tracking the amount of bricks was prone 
	/// to bugs when a PowerBall plowed through multiple bricks.
	/// 
	/// The 'won' boolean is used to prevent multiple calls to Win -- and also to prevent unnecessary 
	/// calls to FindObjectOfType.
	/// </summary>
	void Update ()
	{
		if (won)
			return;
		BrickController someBrick = FindObjectOfType<BrickController> ();
		if (!someBrick) {
			Win ();
		}
	}


	public void GotPowerUp ()
	{
		BallController latestBall = FindObjectOfType<BallController> ();
		if (latestBall)
			latestBall.ActivatePowerBall ();
	}
}
