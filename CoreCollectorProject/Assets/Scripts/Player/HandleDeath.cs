using UnityEngine;
using System.Collections;

public class HandleDeath : MonoBehaviour {


	public GameObject newLifeObject;
	public AudioClip newLifeSFX;

	AsteroidSpawner spawner;
	LifeTracker lifeTracker;
	Vector3 respawnPosition;
	Quaternion respawnRotation;
	HashIDs hash;
	GameOverGUI gameOver;

	void Awake(){
		lifeTracker = GameObject.FindGameObjectWithTag(Tags.guiController).GetComponent<LifeTracker>();
		respawnPosition = GameObject.FindGameObjectWithTag(Tags.player).transform.position;
		respawnRotation = GameObject.FindGameObjectWithTag(Tags.player).transform.rotation;
		hash = GameObject.FindGameObjectWithTag(Tags.dataController).GetComponent<HashIDs>();
		gameOver = GameObject.FindGameObjectWithTag(Tags.guiController).GetComponent<GameOverGUI>();
		spawner = GameObject.FindGameObjectWithTag(Tags.dataController).GetComponent<AsteroidSpawner>();
	}

	public void Died( GameObject obj, Animator anim ){
		Enums.inputMode = Enums.InputMode.DEAD;
		StaticVariables.lives--;
		StaticVariables.multiplier = 1;

		if( StaticVariables.lives >= 0 ){
			lifeTracker.ChangeLivesGUI( StaticVariables.lives );
			StartCoroutine( ResetRoom( obj, anim ) );
		}
		else{
			Enums.inputMode = Enums.InputMode.GAMEOVER;
			gameOver.OnGameOver();
		}
	}

	IEnumerator ResetRoom( GameObject player, Animator anim ){
		yield return new WaitForSeconds(1f);
		Vector3 startingVector = newLifeObject.transform.position;
		float lerpAmount = 0;

		player.transform.position = startingVector;
		player.transform.rotation = respawnRotation;

		Destroy( newLifeObject );
		anim.SetBool( hash.boolDead, false );

		AudioSource.PlayClipAtPoint( newLifeSFX, transform.position, StaticVariables.defaultVolume );

		yield return new WaitForSeconds(0.5f);

		while( player.transform.position != respawnPosition ){
			player.transform.position = Vector3.Lerp( startingVector, respawnPosition, lerpAmount * Time.fixedDeltaTime );
			lerpAmount += 40 * Time.fixedDeltaTime;

			yield return new WaitForFixedUpdate();
		}

		Enums.inputMode = Enums.InputMode.GAMEPLAY;
		StartCoroutine( spawner.Spawner() );
	}
}
