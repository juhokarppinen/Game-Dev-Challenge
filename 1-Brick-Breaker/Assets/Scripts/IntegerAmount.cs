using UnityEngine;
using System.Collections;

public class IntegerAmount : MonoBehaviour
{
	protected int amount;

	public int Get ()
	{
		return amount;
	}


	public void Set (int amount)
	{
		this.amount = amount;
	}


	public virtual void Add (int amount)
	{
		this.amount += amount;
	}


	public void Remove (int amount)
	{
		this.amount -= amount;
	}


	public void Increment ()
	{
		Add (1);
	}


	public void Decrement ()
	{
		Remove (1);
	}
}
