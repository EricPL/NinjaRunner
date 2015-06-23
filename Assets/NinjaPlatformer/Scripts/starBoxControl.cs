using UnityEngine;
using System.Collections;
using DG.Tweening;

public class starBoxControl : MonoBehaviour {

	public Transform[] starTrs;
	private Transform curStar;
	public Ease effectEase;
	public float downTime=0.45f;

	private void showStarEffect(int starNumber)
	{
		curStar=starTrs[starNumber];

		curStar.DOLocalMoveY(-47f,downTime).SetEase(effectEase).SetDelay(downTime*starNumber);
		curStar.DOScale(new Vector3(1.5f,1.5f,1f),downTime-0.5f).SetEase(effectEase).SetDelay(downTime*starNumber);
	}

	public void showStar(int getNumber)
	{
		for (int i = 0; i < getNumber; i++) 
		{
			showStarEffect(i);
		}
	}
}
