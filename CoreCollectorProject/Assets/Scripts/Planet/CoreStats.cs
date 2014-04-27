using UnityEngine;
using System.Collections;

public class CoreStats : MonoBehaviour {

	public GameObject pointText;
	public int scoreGiven;
	public HandleVictory victory;
	public AudioClip coreCollect;
	public AudioClip coreExplode;

	// Use this for initialization
	void Awake () {
		victory = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HandleVictory>();
	}
	
	public void CoreGrab(){
		if( StaticVariables.multiplier < 4 )
			StaticVariables.multiplier++;

		AudioSource.PlayClipAtPoint( coreCollect, transform.position, StaticVariables.defaultVolume / 2 );
		AudioSource.PlayClipAtPoint( coreExplode, transform.position, StaticVariables.defaultVolume );
		StaticVariables.score += scoreGiven * StaticVariables.multiplier;
		CreatePointText();
		victory.OnVictory();
		Destroy ( gameObject );
	}

	void CreatePointText( ){
		GameObject localText = (GameObject)Instantiate( pointText, Camera.main.WorldToViewportPoint( transform.position ), Quaternion.identity );
		
		localText.guiText.text = ( scoreGiven  * StaticVariables.multiplier ).ToString();
	}
}
