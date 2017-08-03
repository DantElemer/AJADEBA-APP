using UnityEngine;
using System.Collections;

public class Character {

	public int cost = 0; //number of turns you have to wait until choosing an other if you choose this one
	public string name = "Anonymus";

	public const string MASON = "Mason";
	public const string PROLIFIC = "Prolific";
	public const string ROMAN = "Roman";

	public virtual void Activated()
	{
		Debug.Log ("I'm chosen!");
	}
}
