﻿using UnityEngine;
using System.Collections;

public class menupause : MonoBehaviour {



	public AudioSource audios;
	public float turnsmo = 2.5f;
	private Rect windowRect;
	private bool paused = false , waited = true,option=false;
	
	private void Start()
	{
		windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200);
	}
	
	private void waiting()
	{
		waited = true;
	}

	private void Update()
	{
		if (waited)
			if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P))
		{
			if (paused)
				paused = false;
			else
				paused = true;
			
			waited = false;
			Invoke("waiting",0.3f);
		}
	}
	
	private void OnGUI()
	{

		if (paused) {
						windowRect = GUI.Window (0, windowRect, windowFunc, "Pause");
				}
				
		if (option)
		{windowRect = GUI.Window (0, windowRect, windowFunc1, "Options");
		//audio = GUI.HorizontalSlider(new Rect(300, 250, 100, 100), audio, 0.0F, 10.0F);
			 }
	}
	
	private void windowFunc(int id)
	{

		if (GUILayout.Button("Reprendre"))
		{
			//turnsmo = 0f;
			paused = false;
			Time.timeScale = 1;
		}
		if (GUILayout.Button("Options"))
		{
			paused=false;
			option = true;

		}
		if (GUILayout.Button("Quitter"))
		{
			Application.Quit();
		}
	}

	private void windowFunc1(int id)
	{
		turnsmo = GUI.HorizontalSlider (new Rect (50, 75, 100, 30), turnsmo, 1.0F, 5.0F);
		GUI.Box(new Rect(55,50,130,20), "Rotation: Sensibilté");
		audios.volume = GUI.HorizontalSlider (new Rect (50, 125, 100, 30), audios.volume, 0.0F, 1.0F);
		GUI.Box(new Rect(85,100,31,20), "Son");

		if (GUILayout.Button("Retour"))
		{
			option = false;
			paused = true;

		}

	}
}
