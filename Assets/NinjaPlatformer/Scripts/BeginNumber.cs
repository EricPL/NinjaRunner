using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BeginNumber : MonoBehaviour {

	public Transform[] beginNumberTrs;
	public float pauseTime;
	public Ease downEase;
	public float downTime;
	public Ease disappearEase;
	public float disappearTime;

	void Start()
	{

	}

	public void showBeginNumber()
	{
		MoveDownEffect();
	}

	private void MoveDownEffect()
	{
		Sequence effectSeq=DOTween.Sequence();

		effectSeq.Append(beginNumberTrs[0].DOLocalMoveY(2.5f,downTime).SetEase(downEase).SetDelay(1f));
		//effectSeq.AppendInterval(pauseTime);
		effectSeq.Append(beginNumberTrs[0].DOPunchRotation(new Vector3(0,0,10),pauseTime,20,1));
		effectSeq.Append(beginNumberTrs[0].DOScale(Vector3.zero,disappearTime).SetEase(disappearEase));

		effectSeq.Append(beginNumberTrs[1].DOLocalMoveY(2.5f,downTime).SetEase(downEase));
		//effectSeq.AppendInterval(pauseTime);
		effectSeq.Append(beginNumberTrs[1].DOPunchRotation(new Vector3(0,0,10),pauseTime,20,1));
		effectSeq.Append(beginNumberTrs[1].DOScale(Vector3.zero,disappearTime).SetEase(disappearEase));

		effectSeq.Append(beginNumberTrs[2].DOLocalMoveY(2.5f,downTime).SetEase(downEase));
		//effectSeq.AppendInterval(pauseTime);
		effectSeq.Append(beginNumberTrs[2].DOPunchRotation(new Vector3(0,0,10),pauseTime,20,1));
		effectSeq.Append(beginNumberTrs[2].DOScale(Vector3.zero,disappearTime).SetEase(disappearEase).OnComplete(()=>{
			GameController.Instance.StartGame();
		}));

		effectSeq.Play();
	}



}
