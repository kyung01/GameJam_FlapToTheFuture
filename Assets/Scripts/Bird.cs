using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {
	public enum State { Flying, Rewinding ,Dead};
	static float INTERVAL_TRACKKER_MAX = .2f;
	public GameObject PREFAB_TRACKKER;
	public float VELO_X,VELO_JUMP;

	public static State myState = State.Flying;
	float timeElapsed = 0;
	float intervalTrackker = 0;
	MovementTrackker trackker; 

	void Start () {
		helperInstantiateTrackker();
		rigidbody2D.velocity = new Vector2(VELO_X, VELO_JUMP);
	}
	void helperInstantiateTrackker()
	{
		var o = Instantiate(PREFAB_TRACKKER, transform.position, Quaternion.identity) as GameObject;
		var t = o.GetComponent<MovementTrackker>();
		if (trackker != null) trackker.AFTER = t;
		t.init(timeElapsed,rigidbody2D.velocity.x,rigidbody2D.velocity.y, trackker);
		trackker = t;
		
	}
	void UpdateTrackker()
	{
		intervalTrackker += Time.deltaTime;
		if (intervalTrackker >= INTERVAL_TRACKKER_MAX)
		{
			intervalTrackker = 0;
			helperInstantiateTrackker();
		}
	}
	void transitionTo_Rewinding()
	{
		BirdAni.displayAlive();
		rigidbody2D.velocity = Vector2.zero;
		//rigidbody2D.isKinematic = true;
		myState = State.Rewinding;	
	}
	void transitionTo_Flying()
	{
		//rigidbody2D.isKinematic = false;
		AudioManager.musicAlive();
		BirdAni.displayAlive();
		intervalTrackker = 0;
		myState = State.Flying;
		rigidbody2D.velocity = new Vector2(VELO_X, 0);
	}
	bool isValid(Vector3 v)
	{
		return (!float.IsInfinity(v.x) && !float.IsInfinity(v.y) && !float.IsInfinity(v.z));
	}
	bool isValid(Vector2 v)
	{
		return (!float.IsInfinity(v.x) && !float.IsInfinity(v.y));
	}
	void UpdateRewind()
	{
		timeElapsed -= Time.deltaTime * 5.0f;
		timeElapsed = Mathf.Max(0, timeElapsed);
		for (int trackkerValid = trackker.isTimeValid(timeElapsed); 
			!trackker.isFirst && trackkerValid != 0; 
			trackkerValid = trackker.isTimeValid(timeElapsed))
		{
			Debug.Log(trackker.isTimeValid(timeElapsed));
			if (trackkerValid == 1) throw new System.NotImplementedException("REWIND ERROR");
			else if (trackkerValid == -1) trackker = trackker.BEFORE;
		}
		var pos = trackker.getPosAt(timeElapsed);
		var speed = trackker.getSpeedAt(timeElapsed);
		
		//transform.position = new Vector3(pos.x, pos.y, transform.position.z);
		if(isValid(pos))transform.position = pos;
		if (isValid(speed)) rigidbody2D.velocity = speed;
		else rigidbody2D.velocity = Vector2.zero;
		//Debug.Log(trackker.getPosAt(timeElapsed));
	}
	void FinishRewind()
	{
		
		var trackkerOld = trackker;
		if (!trackker.isFirst) trackker = trackker.BEFORE;
		if (trackker.AFTER != null) trackker.AFTER.killChain();
		helperInstantiateTrackker();
	}
	void jump()
	{
		AudioManager.soundFly();
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, VELO_JUMP);
	}
	void UpdateFlying()
	{
		UpdateTrackker();
		if (Input.GetKeyDown(KeyCode.Mouse0))  jump(); 
	}
	void Update()
	{
		switch (myState)
		{
			case State.Flying:
				timeElapsed += Time.deltaTime;
				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					helperInstantiateTrackker();
					transitionTo_Rewinding();
					return;
				}
				UpdateFlying();
				break;
			case State.Rewinding:
				if (Input.GetKeyUp(KeyCode.Mouse1))
				{
					FinishRewind();
					transitionTo_Flying();
					return;
				}
				UpdateRewind();
				break;
			case State.Dead:
				timeElapsed += Time.deltaTime;
				UpdateTrackker();
				if (Input.GetKeyDown(KeyCode.Mouse1))
				{
					helperInstantiateTrackker();
					transitionTo_Rewinding();
					return;
				}
				break;
		}
	
	}
	void OnCollisionEnter2D(Collision2D c){
		if (myState != State.Flying) return;
		BirdAni.displayAlive(false);
		AudioManager.soundBad();
		AudioManager.musicDead();
		Debug.Log("I am dead now");
		rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
		myState = State.Dead;
	}
}
