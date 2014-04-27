using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public GameObject asteroid;
	public Vector3 spawnLeft;
	public Vector3 spawnRight;
	public float spawnRate;
	public float spawnRateTimer;

	// Use this for initialization
	void Awake () {
		spawnRateTimer = 1;

		spawnLeft = Camera.main.ViewportToWorldPoint( new Vector3( -0.1f, 0.5f, 5 ) );
		spawnRight = Camera.main.ViewportToWorldPoint( new Vector3( 1.1f, 0.5f, 5 ) );
	}

	void Update(){
		if( Enums.inputMode == Enums.InputMode.GAMEPLAY )
			spawnRateTimer += Time.deltaTime / 4;
		else{
			spawnRateTimer = 1;
		}
	}

	public IEnumerator Spawner(){
		while ( Enums.inputMode == Enums.InputMode.GAMEPLAY ){
			int i = Random.Range(0, 2);
			Vector3 spawnPoint = Vector3.zero;

			float finalSpawnRate = spawnRate * StaticVariables.asteroidSpawnModifier * Random.Range(1f, 2f) / spawnRateTimer;

			if( finalSpawnRate < 0.25f )
				finalSpawnRate = 0.25f;

			if( i == 0 )
				spawnPoint = spawnLeft;
			else if( i == 1 )
				spawnPoint = spawnRight;
			else
				Debug.Log("eh?");

			float newY = Camera.main.ViewportToWorldPoint( new Vector3( 0, Random.Range(0f, 1f), 0 ) ).y;
			spawnPoint = new Vector3( spawnPoint.x, newY, spawnPoint.z );

			Instantiate( asteroid, spawnPoint, Quaternion.identity );

			yield return new WaitForSeconds( finalSpawnRate );
		}
	}
}
