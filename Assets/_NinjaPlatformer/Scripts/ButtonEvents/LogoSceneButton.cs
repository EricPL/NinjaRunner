using UnityEngine;
using System.Collections;

public class LogoSceneButton : MonoBehaviour {

	public enum LogoSceneButtonType
	{
		PlayButton,
		ExitButton,
		MoreGameButton
	}

	public LogoSceneButtonType buttonType;
	public string moreGameURL;
	public Fading fadingScript;

	public void Clicked()
	{
		switch (buttonType) {
		case LogoSceneButtonType.ExitButton:
			Application.Quit();
			break;
		case LogoSceneButtonType.MoreGameButton:
			Application.OpenURL(moreGameURL);
			break;
		case LogoSceneButtonType.PlayButton:
			StartCoroutine(loadGameScene());
			break;
		default:
			break;
		}
	}

	private IEnumerator loadGameScene()
	{
		float fadeTime=fadingScript.BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		
		fadingScript.BeginFade(-1);
		Application.LoadLevel("NinjaPlatformerScene 1");
	}
	
	

}
