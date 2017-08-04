using UnityEngine;
using System.Collections;

public class Roman : Character 
{

	public Roman()
	{
		cost = 5;
		name = ROMAN;
	}

	public override void AddedToPlayer (Player toWhom)
	{
		toWhom.maxSteps += 2;
	}
}
