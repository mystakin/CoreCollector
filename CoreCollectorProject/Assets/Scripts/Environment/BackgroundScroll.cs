using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

	SpaceStats spaceStats;

	// Use this for initialization
	void Start () {
		spaceStats = transform.parent.gameObject.GetComponent<SpaceStats>();
		StartCoroutine( Scroll () );
	}
	
	IEnumerator Scroll(){
		while( spaceStats.scroll ){

			while( Camera.main.WorldToViewportPoint( transform.position ).y > -0.12f ){
				transform.position -= new Vector3( 0, spaceStats.scrollSpeed * Time.deltaTime, 0 );
				yield return new WaitForFixedUpdate();
			}

			transform.position += new Vector3( 0, 23, 0 );
			yield return new WaitForFixedUpdate();
		}


	}
}
