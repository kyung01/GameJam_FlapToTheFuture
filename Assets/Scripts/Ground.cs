using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {
	public GameObject target;
	public float distance;
	
	
	// Update is called once per frame
	void Update () {
		int ratio = (int)(target.transform.position.x / distance);
		transform.position = new Vector3(ratio* distance, transform.position.y, transform.position.z);
		
	
	}
}
