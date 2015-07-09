using UnityEngine;
using System.Collections;

public class MissionDatabase : ScriptableObject {

	public MissionObject[] missions; 

	public MissionObject Get(int index)
	{
		return (this.missions[index]);
	}

	public MissionObject GetByID(int id)
	{
		for (int i = 0; i < this.missions.Length; i++) {
			if(this.missions[i].ID==id)
			{
				return this.missions[i];
			}
		}

		return null;
	}
}
