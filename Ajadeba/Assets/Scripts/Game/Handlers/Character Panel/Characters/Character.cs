using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int cost = 0; //number of turns you have to wait until choosing an other if you choose this one

	public const string Mason = "Mason";

	public virtual void activated()
	{
		Debug.Log ("I'm chosen!");
	}
}
