using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour {


	public Animator fader;
	public string charChoice_1, charChoice_2;

	public void SelectFrump(){


		StartCoroutine (LoadFrump ());

	}

	IEnumerator LoadFrump(){
		
		yield return new WaitForSeconds (.5f);
		fader.SetBool ("SetFader", true);

		yield return new WaitForSeconds (.5f);
		SceneManager.LoadScene (charChoice_1);
	}


	public void SelectFartdashin(){

	
		StartCoroutine (LoadFartdashin ());

	}



	IEnumerator LoadFartdashin(){

		yield return new WaitForSeconds (.5f);
		fader.SetBool ("SetFader", true);

		yield return new WaitForSeconds (.5f);
		SceneManager.LoadScene (charChoice_2);

	}
}
