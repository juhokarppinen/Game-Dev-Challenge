using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
	public GameObject outerWall;
	public GameObject wall;
	public GameObject obstacle;
	public GameObject enemyShip;
	public GameObject energyPickup;

	private GameObject pickupParent;
	private GameObject obstacleParent;
	private GameObject enemyParent;
	private List<GameObject> walls;
	private GameObject lastWall;

	private float xyOffset = 30f;
	private float zOffset = 100f;
	private const float MIN_SCALE = 1.0f;
	private const float MAX_SCALE = 5.0f;
	private float minScale = 1.0f;
	private float maxScale = 5.0f;
	private float pickupProbability = 1.0f;
	private float enemyProbability = 0.0f;

	public List<GameObject> Walls {
		get { return walls; }
	}

	void Start ()
	{
		walls = new List<GameObject> ();
		pickupParent = GameObject.Find ("Pickups");
		obstacleParent = GameObject.Find ("Obstacles");
		enemyParent = GameObject.Find ("Enemies");
		walls.Add (GameObject.Find ("Wall"));
		lastWall = walls [0];
		while (walls.Count < 5)
			CreateWall ();
	}


	public void CreateWall ()
	{
		GameObject lastWall = walls [walls.Count - 1];

		float x = xyOffset;
		float y = xyOffset;
		do {
			x = Random.Range (-xyOffset, xyOffset + 1);
			y = Random.Range (-xyOffset, xyOffset + 1);
		} while (Mathf.Abs (x) + Mathf.Abs (y) >= 2 * xyOffset);

		float z = Random.Range (zOffset, 2 * zOffset + 1) + lastWall.transform.position.z;
		Vector3 location = new Vector3 (x, y, z);

		float xScale = Random.value * (maxScale - minScale) + minScale;
		float yScale = Random.value * (maxScale - minScale) + minScale;

		Quaternion rot = Quaternion.Euler (new Vector3 (0, 0, Random.value * 360));

		GameObject newWall = (GameObject)Instantiate (wall, location, rot);
		newWall.transform.parent = obstacleParent.transform;
		newWall.transform.localScale = new Vector3 (xScale, yScale, 1);
		walls.Add (newWall);

		if (Random.value < enemyProbability)
			CreateEnemy (newWall);
		if (Random.value < pickupProbability)
			CreateEnergyPickup (location);
	}


	public void PopWall (GameObject wall)
	{
		walls.Remove (wall);
		Invoke ("DestroyLastWall", 1); //Destroy (wall);
		CreateWall ();
	}


	private void DestroyLastWall ()
	{
		Destroy (lastWall);
		lastWall = walls [0];
	}


	public void CreateEnergyPickup (Vector3 location)
	{
		float x = xyOffset;
		float y = xyOffset;
		do {
			x = Random.Range (-xyOffset, xyOffset + 1);
			y = Random.Range (-xyOffset, xyOffset + 1);
		} while (Mathf.Abs (x) + Mathf.Abs (y) >= 2 * xyOffset);
		float z = location.z + Random.Range (.25f * zOffset, .75f * zOffset + 1);

		GameObject newPickup = (GameObject)Instantiate (
			                       energyPickup, 
			                       new Vector3 (x, y, z), 
			                       Quaternion.identity);
		newPickup.transform.parent = pickupParent.transform;
	}


	public void CreateEnemy (GameObject wall)
	{
		Vector3 location = wall.transform.position;
		GameObject target = walls [walls.IndexOf (wall) - 1];
		GameObject newEnemy = (GameObject)Instantiate (enemyShip, location, Quaternion.identity);
		newEnemy.transform.parent = enemyParent.transform;
		newEnemy.GetComponent<EnemyShip> ().SetDirection (target.transform.position);
	}


	public void AdjustDifficulty (float score)
	{
		if (score > 2000) {
			maxScale = 3.0f;
			minScale = 1.0f;
			enemyProbability = 1.00f;
			pickupProbability = 0.25f;
		} else if (score > 1500) {
			maxScale = 3.5f;
			minScale = 1.0f;
			enemyProbability = 0.75f;
			pickupProbability = 0.25f;
		} else if (score > 1000) {
			maxScale = 4.0f;
			minScale = 1.5f;
			enemyProbability = 0.55f;
			pickupProbability = 0.35f;
		} else if (score > 500) {
			maxScale = 4.5f;
			minScale = 2.0f;
			enemyProbability = 0.44f;
			pickupProbability = 0.55f;
		} else {
			maxScale = 5.0f;
			minScale = 2.0f;
			enemyProbability = 0.33f;
			pickupProbability = 0.75f;
		}
	}
}
