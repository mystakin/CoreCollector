using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	public GameObject scoreText;
	public GameObject localText;
	public GameObject localTextShadow;
	public string scoreString;
	public float previousScore;

	void Start(){
		previousScore = StaticVariables.score;

		localText = (GameObject)Instantiate( scoreText );
		localTextShadow = localText.transform.GetChild(0).gameObject;

		localText.guiText.anchor = TextAnchor.UpperLeft;
		localTextShadow.guiText.anchor = TextAnchor.UpperLeft;

		localText.transform.position = new Vector3( .02f, .98f, 0 );

		localText.guiText.fontSize = Screen.height / 15;
		localTextShadow.guiText.fontSize = Screen.height / 15;

		scoreString = "Score\n" + StaticVariables.score;
		localText.guiText.text = scoreString;
		localTextShadow.guiText.text = scoreString;
	}

	void FixedUpdate(){
		if( previousScore != StaticVariables.score ){
			previousScore += Mathf.CeilToInt( (StaticVariables.score - previousScore) / 25 );
			scoreString = DevelopScoreString( (int)previousScore );

			localText.guiText.text = scoreString;
			localTextShadow.guiText.text = scoreString;
		}
	}

	string DevelopScoreString( int score ){
		string str = "Score\n";

		int mil = score - (score % 1000000);
		score -= mil;
		mil = mil / 1000000;

		int thou = score - (score % 1000);
		score -= thou;
		thou = thou / 1000;

		if( mil > 0 ){
			str += mil + ",";

			if( thou < 100 )
				str += "0";

			if( thou < 10 )
				str += "0";
		}

		if( thou > 0 ){
			str += thou + ",";

			if( score < 100 )
				str += "0";
			
			if( score < 10 )
				str += "0";
		}

		str += score;

		return str;
	}
}
