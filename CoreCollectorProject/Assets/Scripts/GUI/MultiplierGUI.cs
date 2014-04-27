using UnityEngine;
using System.Collections;

public class MultiplierGUI : MonoBehaviour {

	public GameObject multiplierGUI;
	public GameObject localText;
	public GameObject localTextShadow;
	public string scoreString;
	public int previousMultiplier;
	
	void Start(){
		previousMultiplier = StaticVariables.multiplier;

		localText = (GameObject)Instantiate( multiplierGUI );
		localTextShadow = localText.transform.GetChild(0).gameObject;
		
		localText.guiText.anchor = TextAnchor.UpperRight;
		localTextShadow.guiText.anchor = TextAnchor.UpperRight;
		
		localText.transform.position = new Vector3( .98f, .98f, 0 );
		
		localText.guiText.fontSize = Screen.height / 15;
		localTextShadow.guiText.fontSize = Screen.height / 15;
		
		scoreString = "Multiplier\nx" + StaticVariables.multiplier;
		localText.guiText.text = scoreString;
		localTextShadow.guiText.text = scoreString;
	}
	
	// Update is called once per frame
	void Update () {
		if( previousMultiplier != StaticVariables.multiplier ){
			previousMultiplier = StaticVariables.multiplier;
			scoreString = "Multiplier\nx" + StaticVariables.multiplier;
			
			localText.guiText.text = scoreString;
			localTextShadow.guiText.text = scoreString;
		}
	}
}
