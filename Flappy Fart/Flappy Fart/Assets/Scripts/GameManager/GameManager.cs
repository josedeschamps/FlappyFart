using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public delegate void GameDelegate();
	public static event GameDelegate OnGameStarted;
	public static event GameDelegate OnGameOverConfirmed;

	public static GameManager Instance;


	public GameObject startPage;
	public GameObject gameoverPage;
	public GameObject countdownPage;
	public Text scoreText;
	public AudioSource clickSound;
	public Animator flashAnim;

	enum PageState
	{
		None,
		Start,
		Gameover,
		Countdown
	}


	int score = 0;
	bool gameOver = true;


	public bool GameOver{ get { return gameOver; }}
	public int Score{ get { return score; } }


	void Awake(){

		Instance = this;
	}
		
	void OnEnable(){

		CountdownText.OnCountdownFinished += OnCountdownFinished;
		PlayerController.OnPlayerDied += OnPlayerDied;
		PlayerController.OnPlayerScored += OnPlayerScored;
		PlayerShipController.OnPlayerDied += OnPlayerDied;
		PlayerShipController.OnPlayerScored += OnPlayerScored;


	}

	void OnDisable(){

		CountdownText.OnCountdownFinished -= OnCountdownFinished;
		PlayerController.OnPlayerDied -= OnPlayerDied;
		PlayerController.OnPlayerScored -= OnPlayerScored;
		PlayerShipController.OnPlayerDied -= OnPlayerDied;
		PlayerShipController.OnPlayerScored -= OnPlayerScored;

	}

	void OnCountdownFinished(){

		SetPageState (PageState.None);
		OnGameStarted ();
		score = 0;
		gameOver = false;

	}


	void OnPlayerDied(){
		gameOver = true;
		int savedScore = PlayerPrefs.GetInt ("HighScore");

		if (score > savedScore) 
		{
			PlayerPrefs.SetInt ("HighScore", score);
		}


		flashAnim.SetBool ("SetFlash",true);
		StartCoroutine (WaitForFlash ());
	}

	IEnumerator WaitForFlash(){

		yield return new WaitForSeconds (.5f);
		SetPageState (PageState.Gameover);
	}


	void OnPlayerScored(){
		score++;
		scoreText.text = score.ToString ();

	}

	void SetPageState(PageState state){

		switch (state) {

		case PageState.None:
			startPage.SetActive (false);
			gameoverPage.SetActive (false);
			countdownPage.SetActive (false);
			break;

		case PageState.Start:
			startPage.SetActive (true);
			gameoverPage.SetActive (false);
			countdownPage.SetActive (false);
			break;

		case PageState.Gameover:
			startPage.SetActive (false);
			gameoverPage.SetActive (true);
			countdownPage.SetActive (false);
			break;

		case PageState.Countdown:
			startPage.SetActive (false);
			gameoverPage.SetActive (false);
			countdownPage.SetActive (true);
			break;
		}

	}


	public void ConfirmGameOver(){


		StartCoroutine (DelayGameOver ());
		clickSound.Play ();


	}

	IEnumerator DelayGameOver(){

		yield return new WaitForSeconds (.5f);
		OnGameOverConfirmed ();
		scoreText.text = "0";
		SetPageState (PageState.Start);

	}




	public void StartGame(){

		StartCoroutine (DelayStartGame ());
		clickSound.Play ();
	}


	IEnumerator DelayStartGame(){

		yield return new WaitForSeconds (.5f);
		SetPageState (PageState.Countdown);
		flashAnim.SetBool ("SetFlash",false);


	}



}
