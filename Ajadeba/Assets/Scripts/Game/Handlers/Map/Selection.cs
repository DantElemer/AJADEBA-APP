using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour { 

	public const string VILLAGES = "villages";
	public const string ASSAULT_AIMS = "possible assault aims"; //assuming assault base is chosen fuild; at the moment it finds enemy strongholds later barrack assault can be added
	static Stronghold assaultBase;

	//for BJB
	public const string MY_STRONGS = "friendly strongs"; //current player's strongholds 
	static Player strongOwner;
	public const string STRONG_TER = "stronghold territory"; //currently pressed stronghold's territory
	static Stronghold terOwner;
	public const string BORDERS = "stronghold's territory border"; //currently chosen stronghold's territory's border fields
	static Field ter; //the chosen territory field (to be moved)

	public static bool SelectionTime (string whatToSelect)
	{
		bool somethingSelected = false;

		if (whatToSelect == VILLAGES) {
			foreach (Field[] row in MapHandler.instance.fields)
				foreach (Field f in row)
					if (f != null)
					if (f.HasPart (Field.VILLAGE)) {
						somethingSelected = true;
						f.ActivateSelection ();
					}
		} else if (whatToSelect == MY_STRONGS) {
			strongOwner = PlayerHandler.instance.currentPlayer;
			foreach (Field[] row in MapHandler.instance.fields)
				foreach (Field f in row)
					if (f != null)
					if (f.HasPart (Field.STRONGHOLD))
					if (f.myStronghold.owner == strongOwner) {
						somethingSelected = true;
						f.ActivateSelection ();
					}
		} else if (whatToSelect == STRONG_TER) {
			terOwner = MapHandler.instance.chosenField.myStronghold;
			foreach (Field ter in terOwner.territory) {
				ter.ActivateSelection ();
				somethingSelected = true;
			}
		} else if (whatToSelect == BORDERS) {
			ter = MapHandler.instance.chosenField;
			foreach (Field tery in terOwner.territory)
				if (ter != tery)
					foreach (Field bord in tery.Borders())
						if (ter != bord) {
							bord.ActivateSelection ();
							somethingSelected = true;
						}
		} else if (whatToSelect == ASSAULT_AIMS) {
			assaultBase = MapHandler.instance.chosenField.myStronghold;
			foreach (Field[] row in MapHandler.instance.fields)
				foreach (Field f in row)
					if (f != null)
					if (f.HasPart (Field.STRONGHOLD))
					if (PlayerHandler.instance.areEnemies (assaultBase.owner, f.myStronghold.owner))
					if (assaultBase.attStrength > f.myStronghold.defStrength)
					if (Connection.CanGo (assaultBase, f.myStronghold)) {
						somethingSelected = true;
						f.ActivateSelection ();
					}
		}

		return somethingSelected;
	}

	public static bool SelectionTimeOver (string toWhat)
	{
		bool somethingDeselected = false;

		if (toWhat == VILLAGES) {
			foreach (Field[] row in MapHandler.instance.fields)
				foreach (Field f in row)
					if (f != null)
					if (f.HasPart (Field.VILLAGE)) {
						somethingDeselected = true;
						f.DeactivateSelection ();
					}
		} else if (toWhat == MY_STRONGS) {
			foreach (Field[] row in MapHandler.instance.fields)
				foreach (Field f in row)
					if (f != null)
					if (f.HasPart (Field.STRONGHOLD))
					if (f.myStronghold.owner == strongOwner) {
						somethingDeselected = true;
						f.DeactivateSelection ();
					}
		} else if (toWhat == STRONG_TER)
			foreach (Field ter in terOwner.territory) {
				ter.DeactivateSelection ();
				somethingDeselected = true;
			}
		else if (toWhat == BORDERS) {
			foreach (Field tery in terOwner.territory)
				if (ter != tery)
					foreach (Field bord in tery.Borders())
						if (ter != bord) {
							bord.DeactivateSelection ();
							somethingDeselected = true;
						}
		} else if (toWhat == ASSAULT_AIMS) {
			foreach (Field[] row in MapHandler.instance.fields)
				foreach (Field f in row)
					if (f != null)
					if (f.HasPart (Field.STRONGHOLD))
					if (PlayerHandler.instance.areEnemies (assaultBase.owner, f.myStronghold.owner))
					if (assaultBase.attStrength > f.myStronghold.defStrength)
					if (Connection.CanGo (assaultBase, f.myStronghold)) {
						somethingDeselected = true;
						f.DeactivateSelection ();
					}
		}

		return somethingDeselected;
	}
}
