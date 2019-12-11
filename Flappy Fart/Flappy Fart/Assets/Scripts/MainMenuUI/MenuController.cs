using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public int sceneNumber;
	public Animator fader;



	public void LoadScene(){

		StartCoroutine (StartLoading ());
	

	}


	IEnumerator StartLoading(){

		yield return new WaitForSeconds (.5f);
		fader.SetBool ("SetFader", true);

		yield return new WaitForSeconds (.5f);
		SceneManager.LoadScene (sceneNumber);

	}


}
