using UnityEngine;
using System.Collections;
using MadLevelManager;

public class LevelController : UnitySceneSinglton<LevelController> {

	private int curLevelNumber=1;

	public GameObject[] LevelObjects;

	void Awake()
	{
		curLevelNumber=int.Parse(MadLevel.arguments);

		foreach(GameObject obj in LevelObjects)
		{
			obj.SetActive(false);
		}

		LevelObjects[curLevelNumber-1].SetActive(true);
	}


	public void CompleteLevel(int star)
	{
		switch(star)
		{
		case 1:
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
			break;
		case 2:
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_2", true);
			break;
		case 3:
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_2", true);
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_3", true);
			break;
		}

		MadLevelProfile.SetCompleted(MadLevel.currentLevelName, true);
		MadLevelProfile.SetLocked(MadLevel.GetNextLevelNameTo(MadLevel.currentLevelName),false);
	}

	public void LoadNextLevel()
	{
		if (MadLevel.HasNext(MadLevel.Type.Level)) {
			MadLevel.LoadNext(MadLevel.Type.Level);
		} else {
			MadLevel.LoadLevelByName("LogoScene");
		}
	}



}
