using UnityEngine;
using System.Collections;

public class CameraFollowTarget : MonoBehaviour {

	public GameObject FollowTargetOBJ;
	public float FollowSpeed;

	//Paralax layers
	public GameObject[] BackgroundROOTS;

	private bool PlayerJustDied;

	private float targetYPos;
	private bool isYMove=false;
	private float curYPos;

	void Start()
	{
		curYPos=this.transform.position.y;
	}

	void FixedUpdate(){
		if (PlayerJustDied == false) {
						//Smoothly Follow Target
			Vector3 PositionBefore = this.transform.position;
			Vector3 NewPosition = Vector3.Lerp (this.transform.position, FollowTargetOBJ.transform.position, FollowSpeed * Time.deltaTime);
						
			if(isYMove)
			{
				if(Mathf.Abs(curYPos-targetYPos)<0.05f)
				{
					curYPos=targetYPos;
					isYMove=false;
				}
				else
				{
					curYPos=Mathf.Lerp(this.transform.position.y,targetYPos,FollowSpeed*0.5f*Time.deltaTime);
				}
			}
			else
			{
				curYPos=this.transform.position.y;
			}

			this.transform.position = new Vector3 (NewPosition.x, curYPos, this.transform.position.z);

						//Paralax layer movement
			Vector3 CameraMovementAmount = PositionBefore - this.transform.position;

			BackgroundROOTS [0].transform.Translate (-CameraMovementAmount * 0.8f);
			BackgroundROOTS [1].transform.Translate (-CameraMovementAmount * 0.7f);
			BackgroundROOTS [2].transform.Translate (-CameraMovementAmount * 0.5f);
			BackgroundROOTS [3].transform.Translate (-CameraMovementAmount * 0.4f);
			BackgroundROOTS [4].transform.Translate (-CameraMovementAmount * 0.3f);
		}
		
	}

	//Here you can set camera delay for the Ninja death.
	public void PlayerDied(){
		PlayerJustDied = true;
		Invoke ("BackToBusiness", 0.05f);
	}

	//Camera follow back ON.
	void BackToBusiness(){
		PlayerJustDied = false;
	}

	public void PlayerRealDied()
	{
		PlayerJustDied=true;
	}

	public void startToMoveY(float hh)
	{
		targetYPos=hh;
		isYMove=true;
	}
}
