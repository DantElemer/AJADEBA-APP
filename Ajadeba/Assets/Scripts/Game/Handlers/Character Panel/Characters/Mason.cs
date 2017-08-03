using UnityEngine;
using System.Collections;

public class Mason : Character //Kőműves
{
	void Awake()
	{
		cost = 3;
		Debug.Log (cost);
	}

	public void activated()
	{
		Debug.Log ("For the mansons!");
	}
}
