using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuActions : MonoBehaviour {

	public GameObject Panel;

	public void LoadScene(){
		Application.LoadLevel ("Stage_One");
	}

	public void QuitScene(){
		Application.Quit();
	}

	public void TurnOffMainMenu(){
		Application.LoadLevel ("Controls_Menu");
		//canvasGroup.gameObject.SetActive(!canvasGroup.gameObject.activeSelf);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
