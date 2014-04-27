using UnityEngine;
using System.Collections;

public class SetupScene : MonoBehaviour {

	public bool fadingIn;
	public bool fadingOut;

	HandleVictory victory;
	AudioSource music;
	float volume;

	// Use this for initialization
	void Awake () {
		victory = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HandleVictory>();
		Enums.inputMode = Enums.InputMode.NONE;
		music = GetComponent<AudioSource>();
		volume = music.volume;

		StaticVariables.score = 0;
		StaticVariables.multiplier = 1;
		StaticVariables.lives = 3;
		StaticVariables.spinSpeedModifier = 1;
		StaticVariables.maxRubberIron = 7;
		StaticVariables.ironPieces = 0;
		StaticVariables.rubberPieces = 0;
		StaticVariables.asteroidSpawnModifier = 1;
		StaticVariables.defaultVolume = 0.25f;
		StaticVariables.level = 1;
	}

	void Start(){
		GameObject.FindGameObjectWithTag(Tags.player).transform.position = Vector2.zero;
		victory.OnVictory();
	}

	// Update is called once per frame
	void Update () {
		if( Enums.inputMode == Enums.InputMode.GAMEPLAY && !music.isPlaying && !fadingIn ){
			StartCoroutine( FadeIn() );
		}
		else if( Enums.inputMode != Enums.InputMode.GAMEPLAY && Enums.inputMode != Enums.InputMode.DEAD && music.isPlaying && !fadingOut ){
			StartCoroutine( FadeOut() );
		}
	}

	IEnumerator FadeIn(){
		yield return new WaitForSeconds(1f);

		fadingIn = true;
		music.Play();
		music.volume = 0;
		float lerp = 0;

		while( music.volume != volume ){
			music.volume = Mathf.Lerp( 0, volume, lerp );
			lerp += Time.fixedDeltaTime / 2;
			yield return new WaitForFixedUpdate();
		}

		fadingIn = false;
	}

	IEnumerator FadeOut(){
		fadingOut = true;
		float lerp = 0;
		music.volume = volume;

		while( music.volume != 0 ){
			music.volume = Mathf.Lerp( volume, 0, lerp );
			lerp += Time.fixedDeltaTime / 2;
			yield return new WaitForFixedUpdate();
		}
		
		music.Stop();
		fadingOut = false;
	}
}
