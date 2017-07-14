using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


//Ocsmány dolgok történnek itt, ne nézd meg, ha nem muszáj!

public class Connection : MonoBehaviour {

	class point
	{ //meet: from middle you can reach neighbour's edge
	  //strong: the neighbour can reach your edge
	    public bool northMeet=false;
		public bool northStrong=false;
		public bool southMeet=false;
		public bool southStrong=false;
		public bool eastMeet=false;
		public bool eastStrong=false;
		public bool westMeet=false;
		public bool westStrong=false;
		public bool checked2=false;
		int x, y;

		public point (int xCoord, int yCoord, Player useStrongs, bool canEnterEnTer, Player who)
	    {
			x=xCoord;
			y=yCoord;
			if (MapHandler.instance.InMap(x,y))
			{
				if (MapHandler.instance.InMap(x,y-1))
		        {
					if (MapHandler.instance.fields[x][y].HasPart(Field.SOUTH_ROAD))
		                southMeet=true;
					if (MapHandler.instance.fields[x][y-1].HasPart(Field.NORTH_ROAD) && (canEnterEnTer || !MapHandler.instance.fields[x][y-1].HasEnemyOwner(who)) )
		                southStrong=true;
		        }
				if (MapHandler.instance.InMap(x,y+1))
		        {
					if (MapHandler.instance.fields[x][y].HasPart(Field.NORTH_ROAD))
		                northMeet=true;
					if (MapHandler.instance.fields[x][y+1].HasPart(Field.SOUTH_ROAD) && (canEnterEnTer || !MapHandler.instance.fields[x][y+1].HasEnemyOwner(who)))
		                northStrong=true;
		        }
				if (MapHandler.instance.InMap(x+1,y))
		        {
					if (MapHandler.instance.fields[x][y].HasPart(Field.EAST_ROAD))
		                eastMeet=true;
					if (MapHandler.instance.fields[x+1][y].HasPart(Field.WEST_ROAD)  && (canEnterEnTer || !MapHandler.instance.fields[x+1][y].HasEnemyOwner(who)))
		                eastStrong=true;
		        }
				if (MapHandler.instance.InMap(x-1,y))
		        {
					if (MapHandler.instance.fields[x][y].HasPart(Field.WEST_ROAD))
		                westMeet=true;
					if (MapHandler.instance.fields[x-1][y].HasPart(Field.EAST_ROAD)  && (canEnterEnTer || !MapHandler.instance.fields[x-1][y].HasEnemyOwner(who)))
		                westStrong=true;
		        }

				if (MapHandler.instance.InMap(x,y-1))
					if (MapHandler.instance.fields[x][y-1].HasPart(Field.STRONGHOLD))
						if (MapHandler.instance.fields[x][y-1].myStronghold.owner==useStrongs  && (canEnterEnTer || !MapHandler.instance.fields[x][y-1].HasEnemyOwner(who)))
	                    	southStrong=true;
				if (MapHandler.instance.InMap(x,y+1))
					if (MapHandler.instance.fields[x][y+1].HasPart(Field.STRONGHOLD))
						if (MapHandler.instance.fields[x][y+1].myStronghold.owner==useStrongs && (canEnterEnTer || !MapHandler.instance.fields[x][y+1].HasEnemyOwner(who)))
							northStrong=true;
				if (MapHandler.instance.InMap(x+1,y))
					if (MapHandler.instance.fields[x+1][y].HasPart(Field.STRONGHOLD))
						if (MapHandler.instance.fields[x+1][y].myStronghold.owner==useStrongs && (canEnterEnTer || !MapHandler.instance.fields[x+1][y].HasEnemyOwner(who)))
							eastStrong=true;
				if (MapHandler.instance.InMap(x-1,y))
					if (MapHandler.instance.fields[x-1][y].HasPart(Field.STRONGHOLD))
						if (MapHandler.instance.fields[x-1][y].myStronghold.owner==useStrongs && (canEnterEnTer || !MapHandler.instance.fields[x-1][y].HasEnemyOwner(who)))
							westStrong=true;
				
				if (MapHandler.instance.fields[x][y].HasPart(Field.STRONGHOLD))
					if (MapHandler.instance.fields[x][y].myStronghold.owner==useStrongs)
		            {
		                northMeet=true;
		                southMeet=true;
		                eastMeet=true;
		                westMeet=true;
		            }
			}
	    }

	    public bool isAimPoint(point aimPoint, List<List<point>> points) //recursion
	    {
		    if (checked2)
		     	return false;
	        checked2 = true;
	        if (northMeet)
				if (x == aimPoint.x && y+1 == aimPoint.y)
	                return true;
	            else if (northStrong)
	                if (points[x][y+1].isAimPoint (aimPoint, points))
	                    return true;
	        if (southMeet)
				if (x == aimPoint.x && y-1 == aimPoint.y)
	                return true;
	            else if (southStrong)
	                if (points[x][y-1].isAimPoint (aimPoint, points))
	                    return true;
	        if (eastMeet)
				if (x + 1 == aimPoint.x && y == aimPoint.y)
	                return true;
	            else if (eastStrong)
	                if (points[x+1][y].isAimPoint (aimPoint, points))
	                    return true;
	        if (westMeet)
				if (x - 1 == aimPoint.x && y == aimPoint.y)
	                return true;
	            else if (westStrong)
	                if (points[x-1][y].isAimPoint (aimPoint, points))
	                    return true;
	        return false;
	    }
	};

	public static bool IsConnected (Field from, Field to, Player who, bool canUseStrongs = false, bool canEnterEnemyTer = true)
	{
		Player strongholdRoads; // the player whose strongholds can be used as roads, it's null if can't use strongholds as roads
		if (canUseStrongs)
			strongholdRoads = who;
		else
			strongholdRoads = null;

		if (Math.Abs (from.xCoord - to.xCoord) + Math.Abs (from.yCoord - to.yCoord) == 1) //they are newx to each other, so connection is obvious
	        return true;

		List<List<point>> points = new List<List<point>>();
	    for (int i=0;i<MapHandler.instance.fields.Length;i++) //inicializing points
	    {
			List<point> newRow = new List<point>();
			for (int j=0;j<MapHandler.instance.fields[i].Length;j++)
	        {
				point newPoint=new point (i, j, strongholdRoads, canEnterEnemyTer, who);
				newRow.Add(newPoint);
	        }
	        points.Add(newRow);
	    }
		points [from.xCoord] [from.yCoord].northMeet = true;
		points [from.xCoord] [from.yCoord].southMeet = true;
		points [from.xCoord] [from.yCoord].eastMeet = true;
		points [from.xCoord] [from.yCoord].westMeet = true;
	    return points[from.xCoord][from.yCoord].isAimPoint(points[to.xCoord][to.yCoord],points);
	}

	public static bool canGo (Barrack from2, Stronghold to)
	{
		return IsConnected (from2.myField, to.myField, from2.owner, true);
	}

	public static bool canGo (Stronghold from2, Barrack to)
	{
		return IsConnected (from2.myField, to.myField, from2.owner, false, false);
	}

	public static bool canGo (Stronghold from2, Stronghold to)
	{
		if (from2.owner==to.owner) //its just a nice walk to the neighbours
			return IsConnected (from2, to, from2.owner, true); //I don't see a reason for checking this, maybe the two bool shall be the opposite.

		foreach (Field terFie in to.territory) //assault check...
		{
			if (MapHandler.instance.InMap(terFie.xCoord,terFie.yCoord)) //TODO pálya mellett közvetlenül van a mocskos terület? Ajánlat: pályán kívül ne legyen terület (ez esetben felesleges ez az if).
				if (IsConnected(from2.myField,terFie,from2.owner,false,false))
					return true;
		}
		return false;
	}

	public static bool canGo (Village from2, Barrack to)
	{
		return IsConnected (from2.myField, to.myField, to.owner);
	}

	//TODO test CanGo-s







/*bool gameScreen::isConnected(field from, field to)
{
    if (from.getType()==field::VILLAGE
        && to.getType()==field::BARRACK) //can use only roads
        return helperF(from, to, fields, false, *this);
    else if (from.getType()==field::BARRACK //can use roads and friendly strongholds
          && to.getType()==field::STRONGHOLD)
        return helperF(from, to, fields, true, *this);
    else if (from.getType()==field::STRONGHOLD //assault
          && to.getType()==field::STRONGHOLD) //can use only roads, to target(to)'s territory
        for (int i=to.coordinate.X-3;i<=to.coordinate.X+3;i++)
            for (int j=to.coordinate.Y-3;j<=to.coordinate.Y+3;j++)
                if (inFields(makeCoor(i,j)))
                    if (abs(i-to.coordinate.X)+abs(j-to.coordinate.Y)<=3) //border field
                        if (helperF(from,*fields[i][j], fields, false, *this))
                            return true;
    return false;
}*/



}
