using UnityEngine;
using System.Collections;

public class MapHandler : MonoBehaviour { //TODO add to Map, not to GameHandler?

	private static MapHandler sInstance = null;


	public GameObject map;
	public RoadOption roadOptionPref; 
	public Field fieldPrefab;
	public Field[] fields; 


	public static MapHandler instance //singleton magic
	{
		get 
		{
			if (sInstance == null)
				sInstance = FindObjectOfType (typeof(MapHandler)) as MapHandler;
			if (sInstance == null)
				sInstance = new MapHandler ();
			return sInstance;
		}
	}

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
				if (Random.value < 0.1)
					fields [10 * i + j].addVillage ();
				else 
				{
					if (Random.value < 0.1)
						fields [10 * i + j].addRoad (Field.NORTH);
					if (Random.value < 0.1)
						fields [10 * i + j].addRoad (Field.EAST);
					if (Random.value < 0.1)
						fields [10 * i + j].addRoad (Field.SOUTH);
					if (Random.value < 0.1)
						fields [10 * i + j].addRoad (Field.WEST);
				}
			}
	}

	public void FieldPressed (int fieldInd) //mouse down on field
	{
		Debug.Log (fields[fieldInd].transform.position);
		//TODO valami értelmeset csinálni itt...
		OpenBuildOptions(fieldInd);
	}

	void OpenBuildOptions (int fieldInd)
	{
		Instantiate (roadOptionPref); //TODO show around the clicked field
	}

	public void FieldReleased (int fieldInd) //mouse down on field
	{
		CloseBuildOptions(fieldInd);
	}

	void CloseBuildOptions (int fieldInd)
	{
		//TODO
	}
}
