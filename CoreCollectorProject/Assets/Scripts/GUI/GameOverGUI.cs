using UnityEngine;
using System.Collections;

public class GameOverGUI : MonoBehaviour {

	public GameObject gameOverText;
	public string gameOverString;
	public AudioSource music;
	public float volume;

	// Use this for initialization
	void Awake () {
		gameOverString = "GAME OVER\n\n\n\n\nPress Fire to try again.";
		music = GetComponent<AudioSource>();
	}
	
	public void OnGameOver(){
		GameObject localText = (GameObject)Instantiate( gameOverText );
		GameObject localTextShadow = localText.transform.GetChild(0).gameObject;

		volume = StaticVariables.defaultVolume * 3;
		StartCoroutine( FadeIn() );

		localText.guiText.fontSize = Screen.height / 10;
		localTextShadow.guiText.fontSize = Screen.height / 10;

		localText.guiText.text = gameOverString;
		localTextShadow.guiText.text = gameOverString;
	}

	IEnumerator FadeIn(){
		music.Play();
		music.volume = 0;
		float lerp = 0;
		
		while( music.volume != volume ){
			music.volume = Mathf.Lerp( 0, volume, lerp );
			lerp += Time.fixedDeltaTime / 2;
			yield return new WaitForFixedUpdate();
		}
	}
}
