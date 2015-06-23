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

	public CameraFollowTarget cmFollowTarget;
	[HideInInspector]
	public int PlayerStandLevel=1;

	void Start()
	{
		DOTween.Init();

		//Init all controller
		Init();
		TouchController.Instance.Init();

		bgNumber.showBeginNumber();
	}

	void Update()
	{
		TouchController.Instance.TouchUpdate();
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
		GameOverBoard.DOLocalMoveY(37f,1f).SetEase(Ease.InCirc).OnComplete(()=>{
			showStar(3);
			LevelController.Instance.CompleteLevel(3);
		});
		timer.Stop();
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
}
