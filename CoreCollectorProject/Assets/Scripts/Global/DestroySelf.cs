using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	public float timeTilDestroy;

	// Use this for initialization
	void Start () {
		Destroy( gameObject, timeTilDestroy * Time.deltaTime);
	}

}
