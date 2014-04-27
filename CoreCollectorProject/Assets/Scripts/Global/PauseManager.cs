using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {

	public GameObject pauseText;
	public GameObject localText;
	public GameObject localTextShadow;
	public GameObject darkness;
	public Texture darken;

	// Use this for initialization
	void Awake () {

	}
	
	public void Pause(){
		CreateText();
		CreateDarkness();
		Enums.inputMode = Enums.InputMode.PAUSE;
		Time.timeScale = 0;
	}

	void CreateText(){
		localText = (GameObject)Instantiate( pauseText );
		localTextShadow = localText.transform.GetChild(0).gameObject;
		
		localText.guiText.anchor = TextAnchor.UpperCenter;
		localTextShadow.guiText.anchor = TextAnchor.UpperCenter;
		
		localText.transform.position = new Vector3( .5f, .85f, 2 );
		
		localText.guiText.fontSize = Screen.height / 5;
		localTextShadow.guiText.fontSize = Screen.height / 5;
		
		string scoreString = "PAUSE";
		localText.guiText.text = scoreString;
		localTextShadow.guiText.text = scoreString;
	}

	void CreateDarkness(){
		darkness = new GameObject();
		GUITexture darkTexture = darkness.AddComponent<GUITexture>();

		darkTexture.texture = darken;
		darkTexture.transform.position = new Vector3( 0.5f, 0.5f, 1 );
		darkTexture.color = new Color( 0, 0, 0, 0.25f );
	}

	public void Unpause(){
		Destroy( darkness );
		Destroy( localText );
		Destroy( localTextShadow );
		Enums.inputMode = Enums.InputMode.GAMEPLAY;
		Time.timeScale = 1;
	}
}
