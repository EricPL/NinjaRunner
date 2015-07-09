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

	public Transform cameraPos;
	public int[] bgWidthPadding;

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

			if(Mathf.Abs(cameraPos.position.x-BackgroundROOTS[0].transform.position.x)>bgWidthPadding[0])
			{
				BackgroundROOTS [0].transform.localPosition=new Vector3(BackgroundROOTS [0].transform.localPosition.x+bgWidthPadding[0],
				                                                        BackgroundROOTS [0].transform.localPosition.y,
				                                                        BackgroundROOTS [0].transform.localPosition.z);
			}

			if(Mathf.Abs(cameraPos.position.x-BackgroundROOTS[1].transform.position.x)>bgWidthPadding[1])
			{
				BackgroundROOTS [1].transform.localPosition=new Vector3(BackgroundROOTS [1].transform.localPosition.x+bgWidthPadding[1]+20,
				                                                   BackgroundROOTS [1].transform.localPosition.y,
				                                                   BackgroundROOTS [1].transform.localPosition.z);
			}

			if(Mathf.Abs(cameraPos.position.x-BackgroundROOTS[2].transform.position.x)>bgWidthPadding[2])
			{
				BackgroundROOTS [2].transform.localPosition=new Vector3(BackgroundROOTS [2].transform.localPosition.x+bgWidthPadding[2]+40,
				                                                        BackgroundROOTS [2].transform.localPosition.y,
				                                                        BackgroundROOTS [2].transform.localPosition.z);
			}

			if(Mathf.Abs(cameraPos.position.x-BackgroundROOTS[3].transform.position.x)>bgWidthPadding[3])
			{
				BackgroundROOTS [3].transform.localPosition=new Vector3(BackgroundROOTS [3].transform.localPosition.x+bgWidthPadding[3]+10,
				                                                   BackgroundROOTS [3].transform.localPosition.y,
				                                                   BackgroundROOTS [3].transform.localPosition.z);
			}

			if(Mathf.Abs(cameraPos.position.x-BackgroundROOTS[4].transform.position.x)>bgWidthPadding[4])
			{
				BackgroundROOTS [4].transform.localPosition=new Vector3(BackgroundROOTS [4].transform.localPosition.x+bgWidthPadding[4]+20,
				                                                        BackgroundROOTS [4].transform.localPosition.y,
				                                                        BackgroundROOTS [4].transform.localPosition.z);
			}
		}
		
	}

	//Here you can set camera delay for the Ninja death.
	public void PlayerDied(){
		PlayerJustDied = true;
		//Invoke ("BackToBusiness", 0.05f);
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

	public void stopFollow()
	{
		PlayerJustDied=true;
	}

	public void startFollow()
	{
		PlayerJustDied=false;
	}
}
