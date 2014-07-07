using UnityEngine;
using System.Collections;

public class MovementTrackker : MonoBehaviour
{
	public bool isFirst = false;
	Vector2 velocity = new Vector2();
	MovementTrackker before,after;
	public MovementTrackker BEFORE
	{
		get
		{
			if (before!=null) return before;
			throw new System.NotImplementedException();
		}
	}
	public MovementTrackker AFTER
	{
		get { return after; }
		set { after = value; }
	}
	public float time;

	public void init(float time=0, float veloX=0, float veloY= 0, MovementTrackker b = null)
	{
		this.time = time;
		before = b;
		velocity = new Vector2(veloX, veloY);
		isFirst = (b == null);
	}
	//-1 should go "behind" 
	// 0 you are at the right timeline
	// 1 should go "forward"
	public int isTimeValid(float timeAt)
	{
		if (timeAt > time) return 1;
		if (!isFirst && timeAt < before.time) return -1;
		return 0;
	}
	public float helperGetRatio(float timeAt)
	{

		float timeFrom = (isFirst) ? 0 : before.time;
		return (timeAt - timeFrom) / (time - timeFrom);
	}
	public Vector3 getPosAt(float timeAt)
	{
		Vector3 init = (isFirst) ? Vector3.zero : before.transform.position;
		var dis = transform.position - init;
		dis = new Vector3(dis.x, dis.y, 0);
		return init + dis * helperGetRatio(timeAt);
	}
	public Vector2 getSpeedAt(float timeAt)
	{
		Vector2 init = (isFirst) ? Vector2.zero : before.velocity;
		var dis = velocity - init;
		return init + dis * helperGetRatio(timeAt);
	}
	public void killChain()
	{
		Destroy(gameObject);
		if(after !=null) after.killChain();
	}
}
