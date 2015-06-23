using UnityEngine;
using System.Collections;

public class GlobalManager : UnityAllSceneSinglton<GlobalManager> {


	void Start()
	{
		Application.targetFrameRate=60;
	}
	


}
