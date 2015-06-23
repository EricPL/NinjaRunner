using UnityEngine;
using System.Collections;

public class TouchController : UnitySceneSinglton<TouchController> {

	public NinjaMovementScript ninjaMoveScript;

	public bool enableStandOperation=true;
	[HideInInspector]
	public bool isStand=false;

	public void Init()
	{

	}

	public void TouchUpdate()
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(Input.mousePosition.x>Screen.width*0.5f)
			{
				ninjaMoveScript.Button_Jump_press(true);
			}
			else
			{
				ninjaMoveScript.Button_Jump_press(false);
			}
			//ninjaMoveScript.Button_Jump_press();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			ninjaMoveScript.Button_Jump_release();
		}

		if(enableStandOperation)
		{
			if(Input.GetMouseButtonDown(1))
			{
				isStand=true;
				ninjaMoveScript.Button_Jump_release();
				ninjaMoveScript.Button_Right_release();
				ninjaMoveScript.Button_Left_release();
			}
			else if(Input.GetMouseButtonUp(1))
			{
				isStand=false;
				ninjaMoveScript.Button_Right_press();
			}
		}
		
		
		#region PC Test
		if(Input.GetKeyDown(KeyCode.D))
		{
			ninjaMoveScript.Button_Jump_press(true);
		}
		else if(Input.GetKeyUp(KeyCode.D))
		{
			ninjaMoveScript.Button_Jump_release();
		}

		if(Input.GetKeyDown(KeyCode.A))
		{
			ninjaMoveScript.Button_Jump_press(false);
		}
		else if(Input.GetKeyUp(KeyCode.A))
		{
			ninjaMoveScript.Button_Jump_release();
		}

		if(enableStandOperation)
		{
			if(Input.GetKeyDown(KeyCode.S))
			{
				isStand=true;
				ninjaMoveScript.Button_Jump_release();
				ninjaMoveScript.Button_Right_release();
				ninjaMoveScript.Button_Left_release();
			}
			else if(Input.GetKeyUp(KeyCode.S))
			{
				isStand=false;
				ninjaMoveScript.Button_Right_press();
			}
		}

		#endregion
	}

	public void MovePlayerForward()
	{
		ninjaMoveScript.Button_Right_press();
	}


}
