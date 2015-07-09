using UnityEngine;
using System.Collections;

public class LevelBlock :MonoBehaviour {

	public NinjaCollectible[] levelItems;
	private MovingPlatform[] moveBoards;

	public Enemy_FlyingA[] enemys;

	void Start()
	{
		//Collect Item
		int itemNumber=transform.FindChild("LevelElements").FindChild("COLLECTIBLES").childCount;
		levelItems=new NinjaCollectible[itemNumber];

		for (int i = 0; i < itemNumber; i++) {
			levelItems[i]=transform.FindChild("LevelElements").FindChild("COLLECTIBLES").GetChild(i).GetComponent<NinjaCollectible>();
		}

		//Move Board
		if(transform.FindChild("LevelElements").FindChild("PLATFORMS").FindChild("FlyItem")!=null)
		{
			int moveNumber=transform.FindChild("LevelElements").FindChild("PLATFORMS").FindChild("FlyItem").childCount;

			moveBoards=new MovingPlatform[moveNumber];
			
			for (int i = 0; i < moveNumber; i++) {
				moveBoards[i]=transform.FindChild("LevelElements").FindChild("PLATFORMS").FindChild("FlyItem").GetChild(i).GetComponent<MovingPlatform>();
			}
		}
		else
			moveBoards=null;

		//Enemy
		if(transform.FindChild("Enemys")!=null)
		{
			int eNumber=transform.FindChild("Enemys").childCount;

			enemys=new Enemy_FlyingA[eNumber];

			for (int i = 0; i < eNumber; i++) {
				enemys[i]=transform.FindChild("Enemys").GetChild(i).GetComponent<Enemy_FlyingA>();
			}
		}
		else
			enemys=null;


	}

	public void ResetLevelBlock()
	{
		if(levelItems!=null)
		foreach (var item in levelItems) {
			item.ResetCollectItem();
		}

		if(moveBoards!=null)
		foreach (var item in moveBoards) {
			item.ResetBoard();
		}

		if(enemys!=null)
		{
			foreach (var item in enemys) {
				item.ResetEnemy();
			}
		}
	}


}
