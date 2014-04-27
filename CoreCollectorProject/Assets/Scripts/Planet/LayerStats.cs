using UnityEngine;
using System.Collections;

public class LayerStats : MonoBehaviour {

	public Sprite halfHealthSprite;
	public Sprite quarterHealthSprite;
	public SpriteRenderer rend;
	public GameObject pointText;
	public bool death;
	public bool rubber;
	public bool iron;
	public int health;
	public int maxHealth;
	public int scoreGiven;
	public AudioClip rubberPop;
	public AudioClip normalDeath;

	PlanetStats planetStats;

	// Use this for initialization
	void Start () {
		rend = GetComponentInChildren<SpriteRenderer>();
		planetStats = transform.parent.gameObject.GetComponent<PlanetStats>();

		if( tag == Tags.finalLayer ){
			health = planetStats.finalLayerHealth;
			halfHealthSprite = planetStats.damagedFinalSprites[0];
			quarterHealthSprite = planetStats.damagedFinalSprites[1];
			scoreGiven = 100;

			if( rubber ){
				rend.sprite = planetStats.rubberSprites[0];
				tag = Tags.rubber;
			}
			else if( iron ){
				rend.sprite = planetStats.ironSprites[0];
				tag = Tags.iron;
			}
		}
			
		if( tag == Tags.middleLayer ){
			health = planetStats.middleLayerHealth;
			halfHealthSprite = planetStats.damagedMiddleSprites[0];
			quarterHealthSprite = planetStats.damagedMiddleSprites[1];
			scoreGiven = 50;

			if( rubber ){
				rend.sprite = planetStats.rubberSprites[1];
				tag = Tags.rubber;
			}
			else if( iron ){
				rend.sprite = planetStats.ironSprites[1];
				tag = Tags.iron;
			}
		}

		if( tag == Tags.outerLayer ){
			health = planetStats.outerLayerHealth;
			halfHealthSprite = planetStats.damagedOuterSprites[0];
			quarterHealthSprite = planetStats.damagedOuterSprites[1];
			scoreGiven = 10;

			if( rubber ){
				rend.sprite = planetStats.rubberSprites[2];
				tag = Tags.rubber;
			}
			else if( iron ){
				rend.sprite = planetStats.ironSprites[2];
				tag = Tags.iron;
			}
		}

		maxHealth = health;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if( health <= 2 * maxHealth / 3 ){
			rend.sprite = halfHealthSprite;
		}

		if( health <= maxHealth / 3 ){
			rend.sprite = quarterHealthSprite;
		}

		if( health <= 0 ){
			if( rubber )
				AudioSource.PlayClipAtPoint( rubberPop, transform.position, StaticVariables.defaultVolume );
			else
				AudioSource.PlayClipAtPoint( normalDeath, transform.position, StaticVariables.defaultVolume );

			StaticVariables.score += scoreGiven * StaticVariables.multiplier;
			CreatePointText( scoreGiven * StaticVariables.multiplier );
			Destroy( gameObject );
		}
	}

	public void CreatePointText( int points ){
		GameObject localText = (GameObject)Instantiate( pointText, Camera.main.WorldToViewportPoint( transform.position ), Quaternion.identity );

		localText.guiText.text = ( points ).ToString();
	}
}
