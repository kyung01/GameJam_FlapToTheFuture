using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BirdAni: MonoBehaviour
{
	public GameObject PREFAB_ALIVE, PREFAB_DEAD;
	static BirdAni me;
	void Awake()
	{
		PREFAB_DEAD.renderer.enabled = false;
		BirdAni.me = this;
	}
	public static void displayAlive(bool live = true)
	{

		me.PREFAB_ALIVE.renderer.enabled = live;
		me.PREFAB_DEAD.renderer.enabled = !live;
	}
	

}
