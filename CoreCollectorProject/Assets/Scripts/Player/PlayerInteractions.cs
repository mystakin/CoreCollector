using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	public Animator anim;
	public HashIDs hash;
	public HandleDeath death;
	public AudioClip deathSFX;

	void Awake(){
		hash = GameObject.FindGameObjectWithTag(Tags.dataController).GetComponent<HashIDs>();
		death = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HandleDeath>();

		if( GetComponentInChildren<Animator>() != null )
			anim = GetComponentInChildren<Animator>();
	}

	void OnTriggerEnter2D( Collider2D col ){
		if( Enums.inputMode == Enums.InputMode.GAMEPLAY ){
			if( col.tag != Tags.core && col.tag != Tags.untagged && col.tag != Tags.rubber ){
				AudioSource.PlayClipAtPoint( deathSFX, transform.position, StaticVariables.defaultVolume );
				death.Died( gameObject, anim );
				anim.SetBool( hash.boolDead, true );
			}
			else if( col.tag == Tags.core ){
				if( col.GetComponent<CoreStats>() != null )
					col.GetComponent<CoreStats>().CoreGrab();
			}

			if( col.tag == Tags.rubber ){
				col.GetComponent<LayerStats>().health = 0;
			}

		}
		 
	}
}
