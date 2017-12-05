using UnityEngine;
using System.Collections;

public class ExtraLifeCounter : MonoBehaviour
{
	public int oneUpEvery;

	private int count;


	public void Set (int val)
	{
		count = val;
	}


	public int Get ()
	{
		return count;
	}


	public bool UpdateCounter (int amount)
	{
		count += amount;
		if (count >= oneUpEvery) {
			count -= oneUpEvery;
			return true;
		} else {
			return false;
		}
	}
}
