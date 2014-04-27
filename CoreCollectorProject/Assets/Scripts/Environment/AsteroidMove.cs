using UnityEngine;
using System.Collections;

public class AsteroidMove : MonoBehaviour {

	public bool unstable;
	public float speed;
	public float rotationSpeed;
	public int dir;
	public AudioClip smash;
	
	Animator anim;
	HashIDs hash;
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag(Tags.dataController).GetComponent<HashIDs>();

		speed += Random.Range( 0f, 5f );
		rotationSpeed += Random.Range( 0f, 180f );

		if( Camera.main.WorldToViewportPoint( transform.position ).x < 0 )
			dir = 1;
		else if( Camera.main.WorldToViewportPoint( transform.position ).x > 1 )
			dir = -1;
	}
	
	void FixedUpdate(){
		if( !unstable ){
			transform.position += new Vector3( dir * speed * Time.fixedDeltaTime, 0, 0 );
			transform.rotation = Quaternion.Euler( 0, 0, transform.rotation.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime );
		}

		if( dir > 0 && Camera.main.WorldToViewportPoint( transform.position ).x > 1 ){
			Destroy( gameObject );
		}
			
		else if( dir < 0 && Camera.main.WorldToViewportPoint( transform.position ).x < 0 ){
			Destroy( gameObject );
		}
			
	}

	void OnTriggerEnter2D( Collider2D col ){
		if( col.tag != Tags.player ){
			AudioSource.PlayClipAtPoint( smash, transform.position, StaticVariables.defaultVolume /2 + StaticVariables.VolumeVariance() );
			unstable = true;
			GetComponent<Collider2D>().enabled = false;
			anim.SetBool( hash.boolImpact, true );
			Destroy ( gameObject, 1f );
		}
	}
}
