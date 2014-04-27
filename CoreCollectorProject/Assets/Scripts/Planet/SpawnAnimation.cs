using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnAnimation : MonoBehaviour {

	public GameObject[] chunk_01;
	public GameObject[] chunk_02;
	public GameObject[] chunk_03;
	public GameObject[] chunk_04;
	public GameObject[] chunk_05;
	public GameObject[] chunk_06;
	public GameObject[] chunk_07;
	public GameObject[] chunk_08;
	public GameObject core;
	public float rotateSpeed;
	public AudioClip lockIn;

	AsteroidSpawner spawner;

	List<GameObject[]> chunks = new List<GameObject[]>();

	// Use this for initialization
	void Awake () {
		spawner = GameObject.FindGameObjectWithTag(Tags.dataController).GetComponent<AsteroidSpawner>();

		chunks.Add( chunk_01 );
		chunks.Add( chunk_02 );
		chunks.Add( chunk_03 );
		chunks.Add( chunk_04 );
		chunks.Add( chunk_05 );
		chunks.Add( chunk_06 );
		chunks.Add( chunk_07 );
		chunks.Add( chunk_08 );

		StartCoroutine( SpawnAnim() );
	}
	
	IEnumerator SpawnAnim(){
		float rotationValue = 360 / 8;

		while( Enums.inputMode == Enums.InputMode.POST_VICTORY ){
			yield return new WaitForFixedUpdate();
		}

		while( chunks.Count != 0 ){
			foreach( GameObject[] chunk in chunks ){
				for( int i = 0; i < chunk.Length; i++ ){
					chunk[i].transform.RotateAround( core.transform.position, Vector3.forward, rotateSpeed * Time.fixedDeltaTime );
				}
			}

			if( chunks[0][2].transform.localRotation.eulerAngles.z >= rotationValue ){
				AudioSource.PlayClipAtPoint( lockIn, transform.position, StaticVariables.defaultVolume );
				chunks.RemoveAt( chunks.Count-1 );
				rotationValue += 360 / 8;
			}

			if( chunks.Count == 1 && chunks[0][2].transform.localRotation.eulerAngles.z <= 0f ){
				AudioSource.PlayClipAtPoint( lockIn, transform.position, StaticVariables.defaultVolume );
				chunks.RemoveAt( chunks.Count-1 );
			}

			yield return new WaitForFixedUpdate();
		}

		Enums.inputMode = Enums.InputMode.GAMEPLAY;
		StartCoroutine( spawner.Spawner() );
	}
}
