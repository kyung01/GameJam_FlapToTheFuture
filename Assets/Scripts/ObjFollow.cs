using UnityEngine;
using System.Collections;

public class ObjFollow : MonoBehaviour {
	public GameObject PREFAB_OBJ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var p = PREFAB_OBJ.transform.position;
		transform.position = new Vector3(p.x, transform.position.y, transform.position.z);
	}
}
