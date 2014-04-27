using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {

	public int boolDead = Animator.StringToHash( "dead" );
	public int boolCollided = Animator.StringToHash("collided");
	public int boolImpact = Animator.StringToHash("impact");
}
