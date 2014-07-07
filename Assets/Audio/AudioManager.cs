using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	public AudioSource
		MUSIC_GOOD, MUSIC_BAD;
	public List<AudioSource> SOUND_FLY,SOUND_GOOD,SOUND_BAD,SOUND_NO;
	static AudioManager me;

	// Use this for initialization
	void Start()
	{
		Wall.EVENT_SCORE_UP += soundGood;
		Wall.EVENT_SCORE_DOWN += soundNo;
		MUSIC_GOOD.Play();
		MUSIC_BAD.Stop();
		me = this;
	}
	public static void musicAlive()
	{
		me.MUSIC_GOOD.Play();
		me.MUSIC_BAD.Pause();
	}
	public static void musicDead()
	{
		me.MUSIC_GOOD.Pause();
		me.MUSIC_BAD.Play();
	}
	public static void soundFly()
	{
		me.SOUND_FLY[Random.Range(0, me.SOUND_FLY.Count)].Play();
	}
	public static void soundGood()
	{
		me.SOUND_GOOD[Random.Range(0, me.SOUND_GOOD.Count)].Play();
	}
	public static void soundBad()
	{

		me.SOUND_BAD[Random.Range(0, me.SOUND_BAD.Count)].Play();
	}
	public static void soundNo()
	{

		me.SOUND_NO[Random.Range(0, me.SOUND_NO.Count)].Play();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
