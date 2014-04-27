using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour {

	public enum InputMode{
		NONE,
		GAMEPLAY,
		DEAD,
		VICTORY,
		GAMEOVER,
		POST_VICTORY,
		PAUSE
	}

	public static InputMode inputMode = InputMode.NONE;
}
