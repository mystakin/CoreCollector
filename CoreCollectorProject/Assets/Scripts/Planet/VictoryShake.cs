using UnityEngine;
using System.Collections;

public class VictoryShake : MonoBehaviour {

	public float fallSpeed;
	public float jitter;
	public float initialX;
	public LayerStats stats;
	public GameObject pointText;

	// Use this for initialization
	void Start () {
		stats = GetComponent<LayerStats>();
		initialX = -1000;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( Enums.inputMode == Enums.InputMode.VICTORY ){
			if( initialX == -1000 ) {
				initialX = transform.position.x;
				stats.CreatePointText( (int)(stats.scoreGiven * StaticVariables.multiplier * 1.5f) );
				StaticVariables.score += stats.scoreGiven * StaticVariables.multiplier * 1.5f;
			}
			   
			jitter = Random.Range(-0.1f, 0.1f);
			fallSpeed += Random.Range(0f, 0.5f);
			transform.position = new Vector3( initialX, transform.position.y, transform.position.z ) + 
				new Vector3( jitter, -fallSpeed * Time.fixedDeltaTime, 0 );

			if( Camera.main.WorldToViewportPoint( transform.position ).y < -0.2f )
				Destroy( gameObject );
		}
	}
}
