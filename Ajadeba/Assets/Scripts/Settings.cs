using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Settings : MonoBehaviour 
{
	public Slider sldrTimeOfTurn;
	public Text txtTimeOfTurn;
	public Dropdown drpdwnComplexity;

	List<string> turnTimeOps = new List <string>();

	void Start () 
	{
		GenerateTurnTimeOps ();
		sldrTimeOfTurn.maxValue = turnTimeOps.Count-1;

		LoadPrevSettings ();
	}

	void GenerateTurnTimeOps() //GenerateLinearIntervall() can be useful...
	{
		turnTimeOps.Add ("10 seconds");
		turnTimeOps.Add ("30 seconds");
		turnTimeOps.Add ("1 minute");
		turnTimeOps.Add ("2 minute");
		GenerateLinearIntervall (5, 30, "minutes", 5);
		turnTimeOps.Add ("infinite");
	}

	void GenerateLinearIntervall (int start, int end, string unit, int step=1)
	{
		int num = (end - start) / step; //don't be a cretin, give normal values!
		for (int i = 0; i <= num; i++) 
		{
			int currVal = start + i * step;
			string newOp = currVal.ToString () + " " + unit;
			turnTimeOps.Add (newOp);
		}
	}

	public void ChangeComplexity()
	{
		PlayerPrefs.SetString ("Complexity", drpdwnComplexity.options [drpdwnComplexity.value].text);
	}

	public void ChangeTurnTime()
	{
		txtTimeOfTurn.text = "Time of turn: " + turnTimeOps [(int)sldrTimeOfTurn.value];
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void StartGame()
	{
		SaveData ();
		SceneManager.LoadScene("Game");
	}

	void LoadPrevSettings() 
	{
		//TODO
	}

	void SaveData()
	{
		PlayerPrefs.SetString ("Turn time", turnTimeOps [(int)sldrTimeOfTurn.value]);
	}
}
