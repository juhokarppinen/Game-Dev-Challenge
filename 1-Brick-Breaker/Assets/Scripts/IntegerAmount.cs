using UnityEngine;

public class IntegerAmount : MonoBehaviour
{
	protected int amount;


	public int Get ()
	{
		return amount;
	}


	public virtual void Set (int amount)
	{
		this.amount = amount;
	}


	public virtual void Add (int amount)
	{
		this.amount += amount;
	}


	public virtual void Remove (int amount)
	{
		this.amount -= amount;
	}
}
