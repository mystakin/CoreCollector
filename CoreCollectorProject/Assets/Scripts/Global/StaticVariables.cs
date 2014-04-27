using UnityEngine;
using System.Collections;

public class StaticVariables : MonoBehaviour {

	public static float score;
	public static int lives;
	public static int multiplier;

	public static float spinSpeedModifier;
	public static float asteroidSpawnModifier;
	public static int maxRubberIron;
	public static int ironPieces;
	public static int rubberPieces;

	public static float defaultVolume;
	public static int level;

	public static float VolumeVariance(){
		return Random.Range( -0.1f, 0f );
	}
}
