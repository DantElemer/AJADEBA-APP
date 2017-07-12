using UnityEngine;
using System.Collections;

public class MapHandler : MonoBehaviour { //TODO add to Map, not to GameHandler?

	private static MapHandler sInstance = null;


	public GameObject map;
	public Field fieldPrefab;
	public Field[] fields; 

	public Field chosenField;


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
		//TODO: generate map normally
		GenerateMap();
	}

	void GenerateMap()
	{
		fields = new Field[100];
		for (int i = 0; i < 10; i++)
			for (int j = 0; j < 10; j++) 
			{
				fields [10*i+j] = Instantiate(fieldPrefab);
				fields [10*i+j].index = 10*i+j;
				fields [10 * i + j].transform.Translate(new Vector3 (Field.WIDTH * (i-5), Field.WIDTH * (j-5), 0));
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

	public void FieldPressed (Field field) //mouse down on field
	{
		if (BuildHandler.instance.canBuild(field,PlayerHandler.instance.currentPlayer))
			BuildHandler.instance.OpenBuildOptions(field);
		chosenField = field;
	}

	public void FieldReleased (Field field) //mouse down on field
	{
		if (BuildHandler.instance.open)
			BuildHandler.instance.CloseBuildOptions();
	}
}
