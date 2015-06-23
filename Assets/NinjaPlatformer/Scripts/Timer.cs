using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public Text timerText;

	private float seconds;
	private float minutes;
	private float minSecond;

	private bool isStart=false;
	private float sTime;

	void Start()
	{
		isStart=false;
	}

	void Update()
	{
		if(isStart)
		{
			minutes=(int)((Time.time-sTime)/60f);
			seconds=(int)((Time.time-sTime)%60f);
			minSecond=(float)((Time.time-sTime)%60f *1000f%1000f);
			
			timerText.text=minutes.ToString("00")+" : "+seconds.ToString("00")+" : "+minSecond.ToString("000");
		}
	}

	public void Init(float startTime)
	{
		sTime=startTime;
		isStart=true;
	}

	public void Stop()
	{
		isStart=false;
	}

	public string GetCurTime()
	{
		return timerText.text;
	}

	public string GetBestTimeFormat(float time)
	{
		var bminutes=(int)(time/60f);
		var bseconds=(int)(time%60f);
		var bminSecond=(float)(time%60f *1000f%1000f);

		return bminutes.ToString("00")+" : "+bseconds.ToString("00")+" : "+bminSecond.ToString("000");
	}

}
