using UnityEngine;
using System.Collections;

public class HightCheckPoint : MonoBehaviour {

	public float targetHeight;
	public int targetStandLevel;
	private bool collected=false;

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player" && collected == false && GameController.Instance.PlayerStandLevel!=targetStandLevel) {
			collected = true;

			GameController.Instance.MoveCameraY(targetHeight,targetStandLevel);

			RemovePoint();
		}
	}

	void Update()
	{
		if(GameController.Instance.PlayerStandLevel!=targetHeight)
		{
			collected=false;
		}
	}

	private void RemovePoint()
	{
		//Destroy(this.gameObject,1f);
	}



}
