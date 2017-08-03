using UnityEngine;
using System.Collections;

public class Mason : Character //Kőműves
{
	public Mason()
	{
		cost = 3;
		name = MASON;
	}

	public override void Activated()
	{
		Debug.Log ("For the masons!");
	}
}
