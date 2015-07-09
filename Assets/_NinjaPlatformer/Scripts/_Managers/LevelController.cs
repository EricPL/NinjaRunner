using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class LevelController : UnitySceneSinglton<LevelController> {

	public MissionDatabase missionDatabase;
	public Text[] missionTexts;
	public Transform missionBoard;
	public Ease showEase;
	public Ease hideEase;

	private int curLevelNumber=1;

	public Transform starGet;
	public Transform coinGet;
	public Transform noDieGet;
	public Transform killGet;

	public GameObject[] LevelObjects;

	private string TIMEMISSION ="Use less than ";
	private string KILLMISSION ="You need to kill ";
	private string FINISHLEVELMISSION ="Find the remote door";
	private string NODIEMISSION ="Dont die at all";
	private string COLLECTMISSION ="Collect ";

	public int M_Time_Status{get;set;}
	public int M_Kill_Status{get;set;}
	public int M_Finish_Status{get;set;}
	public int M_Collect_Status{get;set;}
	public int M_NoDie_Status{get;set;}

	private int timeTarget;
	private int killTarget;
	private int collectTarget;

	private bool isRunGetStarEffect=false;
	private bool hasPadding=false;
	private Vector3 paddingPos;
	private Transform paddingTrs;

	private int levelCount=1;
	private int ltest=1;
	public Transform bgRoot;

	void Awake()
	{
		//curLevelNumber=int.Parse(MadLevel.arguments);
		curLevelNumber=1;

		foreach(GameObject obj in LevelObjects)
		{
			//obj.SetActive(false);
		}

		LevelObjects[curLevelNumber-1].SetActive(true);

		InitMissionStatus();
	}

	public void HideOtherLevel()
	{
		for (int i = 1; i < LevelObjects.Length; i++) {
			LevelObjects[i].SetActive(false);
		}
	}

	public void createNextLevel()
	{
		if(ltest==1)
		{
			ltest=2;
			LevelObjects[3].SetActive(false);
		}
		else if(ltest==2)
		{
			ltest=3;
			LevelObjects[0].SetActive(false);
		}
		else if(ltest==3)
		{
			ltest=4;
			LevelObjects[1].SetActive(false);
		}
		else
		{
			ltest=1;
			LevelObjects[2].SetActive(false);
		}

		LevelObjects[ltest-1].gameObject.SetActive(true);
		LevelObjects[ltest-1].GetComponent<LevelBlock>().ResetLevelBlock();
		LevelObjects[ltest-1].transform.localPosition=new Vector3(levelCount*230f,0,0);


		levelCount++;
	}

	private void InitMissionStatus()
	{
		M_Time_Status=0;
		M_Kill_Status=0;
		M_NoDie_Status=0;
		M_Collect_Status=0;
		M_Finish_Status=0;

		for (int i = 0; i < missionTexts.Length; i++) {
			
			string missionDesc="";
			string missionArg=missionDatabase.GetByID(curLevelNumber).missionArgs[i].ToString();

			switch (missionDatabase.GetByID(curLevelNumber).levelMissions[i]) {
			case MissionType.TimeMission:
				missionDesc=TIMEMISSION+missionArg+" seconds";
				timeTarget=missionDatabase.GetByID(curLevelNumber).missionArgs[i];
				M_Time_Status=1;
				break;
			case MissionType.KillMonster:
				missionDesc=KILLMISSION+missionArg+" monsters";
				killTarget=missionDatabase.GetByID(curLevelNumber).missionArgs[i];
				M_Kill_Status=1;
				break;
			case MissionType.FinishLevel:
				missionDesc=FINISHLEVELMISSION;
				M_Finish_Status=1;
				break;
			case MissionType.NoDie:
				missionDesc=NODIEMISSION;
				M_NoDie_Status=1;
				break;
			case MissionType.CollectGold:
				missionDesc=COLLECTMISSION+missionArg+" souls";
				collectTarget=missionDatabase.GetByID(curLevelNumber).missionArgs[i];
				M_Collect_Status=1;
				break;
			}
			
			missionTexts[i].text=(i+1).ToString()+". "+missionDesc;
		}

		showMissionBoard();
		
		if(M_Time_Status==0)
			GameController.Instance.hideTimer();
		
		if(M_Collect_Status==0)
			GameController.Instance.hideCollectLabel();
		
		if(M_Kill_Status==0)
			GameController.Instance.hideKillLabel();
	}

	public void showMissionBoard()
	{
		missionBoard.DOScale(Vector3.one,0.96f).SetEase(showEase);
	}

	public void hideMissionBoard()
	{
		missionBoard.DOScale(Vector3.zero,0.36f).SetEase(hideEase).SetDelay(0.1f).OnComplete(()=>{
			GameController.Instance.showBeginNumber();
		});
	}
		
	public void CheckMissionStatus(bool isGameOver)
	{
		if(isGameOver)
		{
			if(M_Time_Status==1)
			{
				if(GameController.Instance.GetUseTime()<timeTarget)
				{
					M_Time_Status=2;

				}
			}

			if(M_NoDie_Status==1)
			{
				if(!GameController.Instance.GetHadDead())
				{
					M_NoDie_Status=2;
				}
			}

			//complete Finish mission 
			M_Finish_Status=2;
		}
		else
		{
			if(M_Kill_Status==1)
			{
				if(GameController.Instance.GetKillEnemyNumber()>=killTarget)
				{
					M_Kill_Status=2;

					getStarEffect(killGet.position,killGet);
				}
			}

			if(M_Collect_Status==1)
			{
				if(GameController.Instance.GetCollectNumber()>=collectTarget)
				{
					M_Collect_Status=2;

					getStarEffect(coinGet.position,coinGet);
				}
			}
		}


		if(M_Time_Status==2)
		{
			M_Time_Status=3;
			GameController.Instance.starNumber++;
		}

		if(M_Kill_Status==2)
		{
			M_Kill_Status=3;
			GameController.Instance.starNumber++;
		}

		if(M_Finish_Status==2)
		{
			M_Finish_Status=3;
			GameController.Instance.starNumber++;
		}

		if(M_Collect_Status==2)
		{
			M_Collect_Status=3;
			GameController.Instance.starNumber++;
		}

		if(M_NoDie_Status==2)
		{
			M_NoDie_Status=3;
			GameController.Instance.starNumber++;
		}
	}

	public void getStarEffect(Vector3 pos,Transform item)
	{
		if(!isRunGetStarEffect)
		{
			isRunGetStarEffect=true;
			Vector3 orgPos=starGet.position;
			
			Sequence starSeq=DOTween.Sequence();
			
			starSeq.Append(starGet.DOScale(Vector3.one,0.36f).SetEase(Ease.OutElastic));
			starSeq.Append(starGet.DOMove(pos,0.5f).SetEase(Ease.OutCirc).SetDelay(0.2f));
			starSeq.Join(starGet.DOScale(new Vector3(1.5f,1.5f,1.5f),0.45f).SetEase(Ease.Linear).SetDelay(0.2f));
			starSeq.Append(starGet.DOScale(Vector3.zero,0.18f).SetEase(Ease.InOutQuint).OnComplete(()=>{
				isRunGetStarEffect=false;
				item.DOScaleY(1f,0.2f).SetEase(Ease.InOutBounce).SetDelay(0.1f).OnComplete(()=>{
					item.DOPunchRotation(new Vector3(0,0,15),3f,10,1);
				});
			}));
			starSeq.Append(starGet.DOMove(orgPos,0.01f));
			
			starSeq.Play();
		}
		else
		{
			hasPadding=true;
			paddingPos=pos;
			paddingTrs=item;
		}
	}

	public void noDieFailEffect()
	{
		noDieGet.localScale=Vector3.zero;

		noDieGet.DOScale(1f,0.3f).SetEase(Ease.InOutElastic).SetDelay(0.1f).OnComplete(()=>{
			noDieGet.DOPunchRotation(new Vector3(0,0,15),3f,10,1);
		});
	}

	void Update()
	{
		if(Time.frameCount%9==0)
		{
			if(hasPadding)
			{
				hasPadding=false;
				getStarEffect(paddingPos,paddingTrs);
			}
		}
	}


	public void CompleteLevel(int star)
	{
		/*
		switch(star)
		{
		case 1:
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
			break;
		case 2:
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_2", true);
			break;
		case 3:
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_1", true);
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_2", true);
			MadLevelProfile.SetLevelBoolean(MadLevel.currentLevelName, "star_3", true);
			break;
		}

		MadLevelProfile.SetCompleted(MadLevel.currentLevelName, true);
		MadLevelProfile.SetLocked(MadLevel.GetNextLevelNameTo(MadLevel.currentLevelName),false);
		*/
	}

	public void LoadNextLevel()
	{
		/*
		if (MadLevel.HasNext(MadLevel.Type.Level)) {
			MadLevel.LoadNext(MadLevel.Type.Level);
		} else {
			MadLevel.LoadLevelByName("LogoScene");
		}
		*/
	}




}
