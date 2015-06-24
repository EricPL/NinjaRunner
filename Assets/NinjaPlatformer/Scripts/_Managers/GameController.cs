using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class GameController : UnitySceneSinglton<GameController> {

	public BeginNumber bgNumber;
	public Timer timer;
	public starBoxControl starBox;

	public Transform GameOverBoard;
	public Transform GameLoseBoard;

	public Text coinLabel;
	private int coinNumber;

	public Text killLabel;
	private int killEnemyNumber;

	public CameraFollowTarget cmFollowTarget;
	[HideInInspector]
	public int PlayerStandLevel=1;

	[HideInInspector]
	public bool hasDead=false;

	public int starNumber{get;set;}

	void Start()
	{
		DOTween.Init();

		//Init all controller
		Init();
		TouchController.Instance.Init();

		//showBeginNumber();
	}

	void Update()
	{
		TouchController.Instance.TouchUpdate();
	}

	public void showBeginNumber()
	{
		bgNumber.showBeginNumber();
	}

	private void Init()
	{
		coinNumber=0;
		coinLabel.text="x "+coinNumber.ToString();
	}

	public void StartGame()
	{
		TouchController.Instance.MovePlayerForward();
		timer.Init(Time.time);
	}

	public void ResetGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void BackToLogo()
	{
		Application.LoadLevel("LogoScene");
	}

	public void showGameOver()
	{
		timer.Stop();
		
		LevelController.Instance.CheckMissionStatus(true);

		GameOverBoard.DOLocalMoveY(37f,1f).SetEase(Ease.InCirc).OnComplete(()=>{
			showStar(starNumber);
			LevelController.Instance.CompleteLevel(starNumber);
		});
	}

	public void showGameLose()
	{
		GameLoseBoard.DOLocalMoveY(-11f,1f).SetEase(Ease.InCirc);
		timer.Stop();
	}

	public void addCoin()
	{
		coinNumber++;
		coinLabel.text="x "+coinNumber.ToString();

		LevelController.Instance.CheckMissionStatus(false);
	}

	public void addKill()
	{
		killEnemyNumber++;
		killLabel.text="x "+killEnemyNumber.ToString();

		LevelController.Instance.CheckMissionStatus(false);
	}

	public void showStar(int starGetNumber)
	{
		starBox.showStar(starGetNumber);
	}

	public void MoveCameraY(float height,int standLv)
	{
		PlayerStandLevel=standLv;
		cmFollowTarget.startToMoveY(height);
	}

	public void stopCameraFollow()
	{
		cmFollowTarget.stopFollow();
	}

	public void startCameraFollow()
	{
		cmFollowTarget.startFollow();
	}

	public int GetUseTime()
	{
		return timer.GetTimerSecond();
	}

	public int GetCollectNumber()
	{
		return coinNumber;
	}

	public bool GetHadDead()
	{
		return hasDead;
	}

	public int GetKillEnemyNumber()
	{
		return killEnemyNumber;
	}

	public void hideTimer()
	{
		timer.hideTimer();
	}

	public void hideCollectLabel()
	{
		coinLabel.transform.parent.gameObject.SetActive(false);
	}

	public void hideKillLabel()
	{
		killLabel.transform.parent.gameObject.SetActive(false);
	}

	public void setHasDead()
	{
		if(!hasDead)
		{
			hasDead=true;
			LevelController.Instance.noDieFailEffect();
		}
	}
}
