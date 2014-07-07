using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {
	int score = 0;
	public TextMesh text00, text01;
	// Use this for initialization
	void Awake()
	{
		Wall.EVENT_SCORE_DOWN += EVENTHDR_SCORE_DOWN;
		Wall.EVENT_SCORE_UP += EVENTHDR_SCORE_UP;
	
	}
	
	// Update is called once per frame
	void Update()
	{
		var c = text00.color;
		float white = 1.0f*Time.deltaTime;
		text00.color = new Color(c.r + white, c.g + white, c.b + white);
	
	}
	void helperDisplay(int n)
	{

		text00.text = "" + n;
		text01.text = "" + n;
	}
	void EVENTHDR_SCORE_DOWN()
	{
		text00.color = new Color(1, 0, 0);
		helperDisplay(--score);
	}
	void EVENTHDR_SCORE_UP()
	{
		text00.color = new Color(0, 1, 0);
		helperDisplay(++score);
		Debug.Log("UP" + score);
	}
}
