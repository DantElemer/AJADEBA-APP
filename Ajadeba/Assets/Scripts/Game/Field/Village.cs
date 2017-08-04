using UnityEngine;
using System.Collections;

public class Village : FieldPart {

	int myCapacity=1;

	public int capacity {
		get {
			return myCapacity;
		}
	}

	public void Prolificate()
	{
		myCapacity *= 2;
	}
}
