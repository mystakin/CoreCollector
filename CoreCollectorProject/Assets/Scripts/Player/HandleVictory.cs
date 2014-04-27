using UnityEngine;
using System.Collections;

public class HandleVictory : MonoBehaviour {

	public GameObject coreExplosion;
	public GameObject planetPrefab;
	public float spaceSpeed;
	public float defaultSpaceSpeed;
	public AudioClip victoryClip;
	public GameObject levelText;

	PlanetSpin spin;
	SpaceStats spaceStats;
	GatherInput gatherInput;
	GameObject player;
	GameObject planet;
	Quaternion respawnRotation;
	Vector3 respawnPosition;

	// Use this for initialization
	void Awake () {
		planet = GameObject.FindGameObjectWithTag(Tags.planet);
		player = GameObject.FindGameObjectWithTag(Tags.player);

		gatherInput = GetComponent<GatherInput>();
		spin = GetComponent<PlanetSpin>();

		spaceStats = GameObject.FindGameObjectWithTag(Tags.space).GetComponent<SpaceStats>();
		defaultSpaceSpeed = spaceStats.scrollSpeed;

		respawnPosition = GameObject.FindGameObjectWithTag(Tags.player).transform.position;
		respawnRotation = GameObject.FindGameObjectWithTag(Tags.player).transform.rotation;
	}
	
	public void OnVictory(){
		Enums.inputMode = Enums.InputMode.VICTORY;
		StartCoroutine( VictoryCutscene() );
	}

	IEnumerator VictoryCutscene(){
		float timer = 0;
		float prevTime = 0;

		if( planet != null )
			AdvanceDifficulty();

		if( planet != null ){
			while( timer < 0.5f ){
				float explosionDelay = Random.Range(0.1f, 1f);
				Vector3 explosionViewport = new Vector3( Random.Range( 0.4f, 0.6f ), Random.Range( 0.3f, 0.7f ), 5 );
				Vector3 explosionPosition = Camera.main.ViewportToWorldPoint( explosionViewport );

				if( planet.transform.childCount != 0 )
					Instantiate( coreExplosion, explosionPosition, Quaternion.identity );
				
				if( prevTime == 0 )
					timer += Time.deltaTime;
				else
					timer += Time.time - prevTime;
				
				prevTime = Time.time;
				
				yield return new WaitForSeconds( explosionDelay / (planet.transform.childCount+1) );
			}
		}

		Quaternion startingRotation = player.transform.rotation;
		float lerpAmount = 0;

		if( planet != null ){
			while( planet.transform.childCount > 0 ){
				yield return new WaitForFixedUpdate();
			}
		}

		while( player.transform.rotation != respawnRotation ){
			player.transform.rotation = Quaternion.Lerp( startingRotation, respawnRotation, lerpAmount * Time.fixedDeltaTime );
			lerpAmount += 50 * Time.fixedDeltaTime;
			
			yield return new WaitForFixedUpdate();
		}

		Vector3 startingVector = player.transform.position;
		lerpAmount = 0;
		AudioSource.PlayClipAtPoint( victoryClip, transform.position, StaticVariables.defaultVolume * 2 );

		StartCoroutine( DisplayLevel() );

		while( player.transform.position != respawnPosition ){
			player.transform.position = Vector3.Lerp( startingVector, respawnPosition, lerpAmount * Time.fixedDeltaTime );
			spaceStats.scrollSpeed = Mathf.Lerp( spaceStats.scrollSpeed, spaceSpeed, Time.fixedDeltaTime * .75f );
			lerpAmount += 25 * Time.fixedDeltaTime;
			
			yield return new WaitForFixedUpdate();
		}

		Destroy( planet );
		Vector3 planetStartViewport = new Vector3( 0.5f, 1.3f, 0 );
		Enums.inputMode = Enums.InputMode.POST_VICTORY;


		yield return new WaitForSeconds(1.666f);

		planet = (GameObject)Instantiate( planetPrefab, Camera.main.ViewportToWorldPoint( planetStartViewport ), Quaternion.identity );
		gatherInput.planet = planet;
		spin.planet = planet;
		lerpAmount = 0;

		Vector3 planetStartPosition = planet.transform.position;

		while( planet.transform.position.y != Vector2.zero.y ){
			planet.transform.position = Vector3.Lerp( planetStartPosition, Vector3.zero, lerpAmount * Time.fixedDeltaTime );
			spaceStats.scrollSpeed = Mathf.Lerp( spaceStats.scrollSpeed, defaultSpaceSpeed, Time.fixedDeltaTime );

			lerpAmount += 25 * Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}

		spaceStats.scrollSpeed = defaultSpaceSpeed;
		Enums.inputMode = Enums.InputMode.NONE;
	}

	IEnumerator DisplayLevel(){
		GameObject localText = (GameObject)Instantiate( levelText );
		GameObject localTextShadow = localText.transform.GetChild(0).gameObject;

		localText.transform.position = new Vector3( 0.5f, 0.65f, 0 );
		localTextShadow.transform.position = new Vector3( 0.5f, 0.65f, 0 );

		localText.guiText.fontSize = Screen.height / 5;
		localTextShadow.guiText.fontSize = Screen.height / 5;

		Color invisibleWhite = new Color( 1, 1, 1, 0 );
		Color invisibleBlack = new Color( 0, 0, 0, 0 );
		Color visibleWhite = new Color( 1, 1, 1, 1 );
		Color visibleBlack = new Color( 0, 0, 0, 1 );

		localText.guiText.color = invisibleWhite;
		localTextShadow.guiText.color = invisibleBlack;

		string levelString = "Level " + StaticVariables.level;
		localText.guiText.text = levelString;
		localTextShadow.guiText.text = levelString;

		float lerp = 0;

		while( localText.guiText.color.a != 1 ){
			localText.guiText.color = Color.Lerp( invisibleWhite, visibleWhite, lerp );
			localTextShadow.guiText.color = Color.Lerp( invisibleBlack, visibleBlack, lerp );
			lerp += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(3f);
		lerp = 0;

		while( localText.guiText.color.a != 0 ){
			localText.guiText.color = Color.Lerp( visibleWhite, invisibleWhite, lerp );
			localTextShadow.guiText.color = Color.Lerp( visibleBlack, invisibleBlack, lerp );
			lerp += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		Destroy ( localText );
		Destroy ( localTextShadow );
	}

	void AdvanceDifficulty(){
		StaticVariables.spinSpeedModifier += 0.5f;
		StaticVariables.asteroidSpawnModifier = StaticVariables.asteroidSpawnModifier / 1.5f;
		StaticVariables.level++;

		if( (StaticVariables.ironPieces + StaticVariables.rubberPieces) < StaticVariables.maxRubberIron ){
			int rand = Random.Range(0,2);

			if( rand == 0 )
				StaticVariables.ironPieces++;
			else if( rand == 1 )
				StaticVariables.rubberPieces++;
			else
				Debug.Log ("huh?");
		}
		  
	}
}
