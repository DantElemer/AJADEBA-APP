using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class Settings : MonoBehaviour 
{
	public Slider sldrTimeOfTurn;
	public Text txtTimeOfTurn;

	List<string> turnTimeOps = new List <string>();

	void Start () 
	{
		generateTurnTimeOps ();
		sldrTimeOfTurn.maxValue = turnTimeOps.Count-1;
	}

	void generateTurnTimeOps()
	{
		turnTimeOps.Add ("10 seconds");
		turnTimeOps.Add ("30 seconds");
		turnTimeOps.Add ("1 minute");
		turnTimeOps.Add ("2 minute");
		turnTimeOps.Add ("infinite");
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
		SceneManager.LoadScene("Game");
	}
}
