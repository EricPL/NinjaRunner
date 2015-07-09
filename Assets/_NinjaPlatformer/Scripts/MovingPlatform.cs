using UnityEngine;
using System.Collections;

//Add this script to the platform you want to move.
public class MovingPlatform : MonoBehaviour {

	public Transform boardItem;

	//Platform movement speed.
	public float speed;

	//This is the position where the platform will move.
	public Transform MovePosition;
	public Transform MoveStartPos;

	//Some private variables making the code work :)
	private Vector3 StartPosition;
	private Vector3 EndPosition;
	private Vector3 targetPos;
	private bool OnTheMove;

	// Use this for initialization
	void Start () {
		//Store the start and the end position. Platform will move between these two points.
		StartPosition = MoveStartPos.position;
		EndPosition = MovePosition.position;

		targetPos=EndPosition;
	}
	
	void FixedUpdate () {
	
		if(targetPos.x!=MovePosition.position.x)
			ResetBoard();

		float step = speed * Time.deltaTime;

		if (OnTheMove == false) {
			boardItem.position = Vector3.MoveTowards (boardItem.position, EndPosition, step);
		}else{
			boardItem.position = Vector3.MoveTowards (boardItem.position, StartPosition, step);
		}

		//When the platform reaches end. Start to go into other direction.
		if (boardItem.position.x == EndPosition.x && boardItem.position.y == EndPosition.y && OnTheMove == false) {
			OnTheMove = true;
		}else if (boardItem.position.x == StartPosition.x && boardItem.position.y == StartPosition.y && OnTheMove == true) {
			OnTheMove = false;
		}
	}
	
	public void ResetBoard()
	{
		StartPosition = MoveStartPos.position;
		EndPosition = MovePosition.position;

		targetPos=EndPosition;

		boardItem.position=MoveStartPos.position;
	}

}
