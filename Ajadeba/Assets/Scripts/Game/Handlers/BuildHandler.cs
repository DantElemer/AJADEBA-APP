using UnityEngine;
using System.Collections;

public class BuildHandler : MonoBehaviour {

	private static BuildHandler sInstance;

	public RoadOption roadOptionPref; 
	public BarrackOption barrackOptionPref;
	public StrongBaseOp strongBaseOpPref;

	RoadOption rO;
	BarrackOption bO;
	StrongBaseOp sO;

	public bool open = false;

	public static BuildHandler instance //singleton magic
	{
		get 
		{
			if (sInstance == null)
				sInstance = FindObjectOfType (typeof(BuildHandler)) as BuildHandler;
			if (sInstance == null)
				sInstance = new BuildHandler ();
			return sInstance;
		}
	}

	public void OpenBuildOptions (Field field)
	{
		rO = Instantiate (roadOptionPref); //TODO show around the clicked field
		rO.transform.position = field.transform.position+Vector3.right;
		bO = Instantiate (barrackOptionPref); //TODO show around the clicked field
		bO.transform.position = field.transform.position+Vector3.left;
		sO = Instantiate (strongBaseOpPref); //TODO show around the clicked field
		sO.transform.position = field.transform.position+Vector3.up;
		open = true;
	}

	public void CloseBuildOptions ()
	{
		rO.CloseRoadOps ();
		Destroy (rO.gameObject);
		Destroy (bO.gameObject);
		Destroy (sO.gameObject);
		open = false;
	}

	public bool canBuild(Field field, Player who)
	{
		//TODO ellenőrizni, h tud-e field-re valamit who (terület, ott lévő objektumok)
		if (field.isFullForBuilding ()) //TODO: alagút kivétel
		{ 
			Debug.Log ("You cannot build there, it's full!");
			return false;
		}
		if (field.hasOtherOwner (who)) 
		{
			Debug.Log ("You cannot build there, it's enemy territory!");
			return false;
		}
		return true;
	}


}
