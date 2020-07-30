﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip Hit_01, Menu_Navigate_03;
    static AudioSource audioSrc;

	// Use this for initialization
	public void soundCreation () {
        Hit_01 = Resources.Load<AudioClip>("Hit_01");
        Menu_Navigate_03 = Resources.Load<AudioClip> ("Menu_Navigate_03");

        audioSrc = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
