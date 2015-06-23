using UnityEngine;
using System.Collections;

public class NinjaCollectible : MonoBehaviour {

	private bool collected;
	public GameObject SoulSprites;
	public ParticleSystem SoulParticles;

	private MainEventsLog MainEventsLog_script;

	public bool isFinalDoor;
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player" && collected == false && !isFinalDoor) {
			collected = true;
			SoulSprites.SetActive(false);
			SoulParticles.Emit(10);
			Invoke ("DestroyObject", 1f);

			//Send message to MainEventsLog. First checks if the reference path is set. If not, it will MainEventsLog from the scene.
			if(MainEventsLog_script == null){
				MainEventsLog_script = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
			}
			MainEventsLog_script.PlayerCollectedSoul();
		}

		if(isFinalDoor && collected==false)
		{
			GameController.Instance.showGameOver();
			collected=true;
			SoulSprites.SetActive(false);
			Invoke("DestroyObject",1f);

			return;
		}
	}

	void DestroyObject(){
		Destroy (this.gameObject);
	}



}
