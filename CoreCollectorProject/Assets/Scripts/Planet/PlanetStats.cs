using UnityEngine;
using System.Collections;

public class PlanetStats : MonoBehaviour {
	
	public Sprite[] damagedOuterSprites;
	public Sprite[] damagedMiddleSprites;
	public Sprite[] damagedFinalSprites;
	public Sprite[] ironSprites;
	public Sprite[] rubberSprites;
	public int outerLayerHealth;
	public int middleLayerHealth;
	public int finalLayerHealth;

	void Awake(){
		AssignSpecialConditions( StaticVariables.rubberPieces, true );
		AssignSpecialConditions( StaticVariables.ironPieces, false );
	}

	void AssignSpecialConditions( int pieces, bool rubber ){
		for( int i = 0; i < pieces; i++ ){
			GameObject obj = null;
			LayerStats stats = null;

			while( obj == null || obj.tag == Tags.core || stats.rubber || stats.iron ){
				obj = transform.GetChild( Random.Range( 0, transform.childCount ) ).gameObject;
				stats = obj.GetComponent<LayerStats>();
			}

			if( rubber ){
				if( obj.tag == Tags.outerLayer ){
					stats.rubber = true;
				}
				else if( obj.tag == Tags.middleLayer ){
					stats.rubber = true;
				}
				else if( obj.tag == Tags.finalLayer ){
					stats.rubber = true;
				}
			}
			else{
				if( obj.tag == Tags.outerLayer ){
					stats.iron = true;
				}
				else if( obj.tag == Tags.middleLayer ){
					stats.iron = true;
				}
				else if( obj.tag == Tags.finalLayer ){
					stats.iron = true;
				}
			}
		}
	}
}
