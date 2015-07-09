using UnityEngine;
using System.Collections;

public enum MissionType
{
	TimeMission, 
	KillMonster,
	CollectGold,
	FinishLevel, 
	NoDie,
}

[System.Serializable]
public class MissionObject {

	public int ID;

	public MissionType[] levelMissions=new MissionType[3];
	public int[] missionArgs=new int[3];


	
}
