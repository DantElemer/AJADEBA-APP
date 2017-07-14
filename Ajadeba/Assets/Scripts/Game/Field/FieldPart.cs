using UnityEngine;
using System.Collections;

public class FieldPart : MonoBehaviour {

	public Field myField 
	{
		get {
			return (transform.parent.gameObject.GetComponent<Field>() as Field);
		}
	}
}
