using UnityEngine;
using System.Collections;

public class MapHandler : MonoBehaviour { //TODO add to Map, not to GameHandler?

	public GameObject map;
	public Field fieldPrefab;
	public static Field[] fields; //TODO destaticalizálás esetleg

	void Start () {
		//TODO: generate map
		GenerateMap();
	}

	void Update () {
	
	}

	void GenerateMap()
	{
		fields = new Field[100];
		for (int i = 0; i < 10; i++)
			for (int j = 0; j < 10; j++) 
			{
				fields [10*i+j] = Instantiate(fieldPrefab);
				fields [10*i+j].index = 10*i+j;
				fields [10 * i + j].transform.Translate(new Vector3 (Field.WIDTH * i, Field.WIDTH * j, 0));
				fields [10 * i + j].transform.SetParent (map.transform);
			}
	}

	public static void FieldPressed(int fieldInd) //mouse down on field
	{
		Debug.Log (fields[fieldInd].transform.position);
		//TODO valami értelmeset csinálni itt...
	}
}
