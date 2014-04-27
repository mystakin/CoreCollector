using UnityEngine;
using System.Collections;

public class LaserMove : MonoBehaviour {

	public float speed;
	public bool hit;
	public bool evil;
	public GameObject evilLaserPrefab;
	public AudioClip hitSFX;
	public AudioClip rubberSFX;
	public AudioClip ironSFX;
	public float volAdjust;

	Animator anim;
	HashIDs hash;

	void Awake(){
		volAdjust = StaticVariables.defaultVolume / 3;

		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(Tags.dataController).GetComponent<HashIDs>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if( !hit ){
			if( !evil )
				transform.position += transform.up * speed * Time.fixedDeltaTime;
			else
				transform.position += -transform.up * speed * Time.fixedDeltaTime;
		}
			

		if( Camera.main.WorldToViewportPoint( transform.position ).x > 1 || Camera.main.WorldToViewportPoint( transform.position ).y > 1 ||
		    Camera.main.WorldToViewportPoint( transform.position ).x < 0 || Camera.main.WorldToViewportPoint( transform.position ).y < 0 ){
			Destroy ( gameObject );
		}
	}

	void OnTriggerEnter2D( Collider2D col ){
		if( col.tag != Tags.player && col.tag != Tags.evilLaser ){
			if( col.GetComponent<LayerStats>() != null && col.tag != Tags.rubber && col.tag != Tags.iron )
				col.GetComponent<LayerStats>().health--;

			if( col.tag == Tags.rubber ){
				AudioSource.PlayClipAtPoint( rubberSFX, transform.position, volAdjust + StaticVariables.VolumeVariance());
				GameObject evilLaser = (GameObject)Instantiate( evilLaserPrefab, transform.position, transform.rotation );
				evilLaser.GetComponent<LaserMove>().evil = true;
			}
			else if( col.tag == Tags.iron ){
				AudioSource.PlayClipAtPoint( ironSFX, transform.position, volAdjust + StaticVariables.VolumeVariance() );
			}
			else if( !evil ){
				AudioSource.PlayClipAtPoint( hitSFX, transform.position, volAdjust + StaticVariables.VolumeVariance() );
			}

			if( !evil ){
				hit = true;
				GetComponent<Collider2D>().enabled = false;
				anim.SetBool( hash.boolCollided, true );
				Destroy ( gameObject, 1f );
			}
		}
		else if( evil ){
			Destroy ( gameObject );
		}
	}
}
