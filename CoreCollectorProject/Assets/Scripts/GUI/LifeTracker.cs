using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeTracker : MonoBehaviour {

	public Sprite lifeSprite;
	public List<GameObject> lifeList = new List<GameObject>();
	public HandleDeath death;
	public int freeLivesGiven;
	public int scoreForLife;
	public AudioClip freeLifeSFX;

	// Use this for initialization
	void Start () {
		death = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HandleDeath>();
		ChangeLivesGUI( StaticVariables.lives );
	}

	void Update(){
		if( StaticVariables.score > ( scoreForLife * (freeLivesGiven+1) ) ){
			StaticVariables.lives++;
			freeLivesGiven++;
			AudioSource.PlayClipAtPoint( freeLifeSFX, transform.position, StaticVariables.defaultVolume );
			ChangeLivesGUI( StaticVariables.lives );
		}
	}

	public void ChangeLivesGUI( int lives ){
		if( lifeList.Count < lives ){
			for ( int i = lifeList.Count; i < lives; i++ ){
				GameObject obj = new GameObject();
				SpriteRenderer rend = obj.AddComponent<SpriteRenderer>();
				
				obj.name = i + " life";
				obj.transform.position = Camera.main.ViewportToWorldPoint( Vector3.zero );
				obj.transform.position += new Vector3( 1 + (i * 1), 1, 1 );
				rend.sprite = lifeSprite;
				
				lifeList.Add( obj );
			}
		}
		else if( lifeList.Count > lives ){
			death.newLifeObject = lifeList[ lifeList.Count-1 ];
			lifeList.RemoveAt( lifeList.Count-1 );
		}
	}
}
