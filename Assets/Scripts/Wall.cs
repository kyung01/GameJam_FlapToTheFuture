using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Wall : MonoBehaviour
{
	public delegate void REQUEST_SIMPLE();
	public static REQUEST_SIMPLE EVENT_SCORE_UP = delegate { };
	public static REQUEST_SIMPLE EVENT_SCORE_DOWN = delegate { };
	bool isCollected = false;

	void OnTriggerEnter2D(Collider2D c) { }
	void OnTriggerExit2D(Collider2D c)
	{
		if (Bird.myState == Bird.State.Flying)
		{
			if (!isCollected)
			{
				Debug.Log("COLLECTED");
				EVENT_SCORE_UP();
				isCollected = true;
			}
		}
		if (Bird.myState == Bird.State.Rewinding)
		{
			if (isCollected)
			{
				Debug.Log("UN_COLLECTED");
				EVENT_SCORE_DOWN();
				isCollected = false;
			}
		}
	}
}