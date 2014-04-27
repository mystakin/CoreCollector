using UnityEngine;
using System.Collections;

public class PointsMovement : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Awake () {
		guiText.fontSize = Screen.height / 20;
		StartCoroutine ( FloatAndDie() );
	}
	
	IEnumerator FloatAndDie(){
		float timer = 0f;

		while( timer < 0.4f ){
			transform.position += new Vector3( 0, speed * Time.fixedDeltaTime, 0 );
			timer += Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}

		timer = 0f;

		while( guiText.color.a > 0 ){
			guiText.color = Color.Lerp( new Color( 1, 1, 1, 1 ), new Color( 1, 1, 1, 0 ), timer );
			transform.position += new Vector3( 0, speed * Time.fixedDeltaTime, 0 );
			timer += Random.Range(1f, 3f) * Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}

		Destroy ( gameObject );
	}
}
