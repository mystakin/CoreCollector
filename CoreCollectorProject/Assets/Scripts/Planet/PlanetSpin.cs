using UnityEngine;
using System.Collections;

public class PlanetSpin : MonoBehaviour {

	public float spinSpeed;
	public float realSpeed;
	public GameObject planet;

	// Use this for initialization
	void Awake () {
		planet = GameObject.FindGameObjectWithTag(Tags.planet);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( Enums.inputMode != Enums.InputMode.VICTORY && planet != null ){
			realSpeed = spinSpeed * StaticVariables.spinSpeedModifier;
			planet.transform.rotation = Quaternion.Euler( 0, 0, planet.transform.rotation.eulerAngles.z + realSpeed * Time.fixedDeltaTime );
		}

	}
}
