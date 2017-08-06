using UnityEngine;
using System.Collections;

public class BuildHandler : MonoBehaviour {

	private static BuildHandler sInstance; //the only instance

	public RoadOption roadOptionPref; // you cannot build this one, but this shows the four road options
	public BarrackOption barrackOptionPref;
	public StrongBaseOp strongBaseOpPref;
	// their private instances:
	RoadOption rO;
	BarrackOption bO;
	StrongBaseOp sO;

	public bool open { get; private set; }

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

	void Start()
	{
		open = false;
	}

	public void OpenBuildOptions (Field field)
	{
		rO = Instantiate (roadOptionPref); 
		rO.transform.position = field.transform.position+Vector3.right;
		bO = Instantiate (barrackOptionPref); 
		bO.transform.position = field.transform.position+Vector3.left;
		sO = Instantiate (strongBaseOpPref); 
		sO.transform.position = field.transform.position+Vector3.up;
		open = true;
	}

	public void CloseBuildOptions ()
	{
		rO.CloseRoadOps (); //you know, road options are a bit tricky...
		Destroy (rO.gameObject);
		Destroy (bO.gameObject);
		Destroy (sO.gameObject);
		open = false;
	}

	public bool CanBuild(Field field, Player who)
	{
		if (field.HasOtherOwner (who)
			&& !(field.IsOwner(who) && who.HasChar(Character.TRESPASSER))) //birtokháborító azér bejuthat...
		{
			Debug.Log ("You cannot build there, it's not your territory!");
			return false;
		}
		else if (field.IsFullForBuilding ()) //TODO: alagút kivétel
		{ 
			Debug.Log ("You cannot build there, it's full!");
			return false;
		}
		return true;
	}


}
