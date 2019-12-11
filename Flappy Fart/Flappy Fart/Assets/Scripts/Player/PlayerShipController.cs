using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour {


	public delegate void PlayerDelegate();
	public static event PlayerDelegate OnPlayerDied;
	public static event PlayerDelegate OnPlayerScored;

	//player stats
	public float moveSpeed;
	public float xPos, yPos;
	public Vector3 startPos;
	private Rigidbody2D RB2D;

	public AudioSource scoreSFX;
	public AudioSource dieSFX;
	GameManager game;


	void Start()
	{

		RB2D = GetComponent<Rigidbody2D> ();
		game = GameManager.Instance;
		RB2D.simulated = false;
	}


	void OnEnable(){
		GameManager.OnGameStarted += OnGameStarted;
		GameManager.OnGameOverConfirmed += OnGameOverConfirmed;

	}

	void OnDisable(){

		GameManager.OnGameStarted -= OnGameStarted;
		GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
	}

	void OnGameStarted(){

		RB2D.velocity = Vector3.zero;
		RB2D.simulated = true;

	}

	void OnGameOverConfirmed(){

		transform.localPosition = startPos;
		transform.rotation = Quaternion.identity;

	}








	void Update () {

		if (game.GameOver) 
			return;
		

	
		//controller movement.
		xPos = Input.GetAxisRaw ("Horizontal") * moveSpeed;
		yPos = Input.GetAxisRaw ("Vertical") * moveSpeed;

		RB2D.velocity = (new Vector2 (xPos, yPos));


		Vector3 calampedPosition = transform.position;
		calampedPosition.y = Mathf.Clamp (transform.position.y, -5f, 5f);
		calampedPosition.x = Mathf.Clamp (transform.position.x, -8f, 8f);
		transform.position = calampedPosition;

	}




	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("ScoreZone")) 
		{

			scoreSFX.Play ();
			OnPlayerScored ();
		}




		if (other.gameObject.CompareTag ("DeadZone")) 
		{

			dieSFX.Play ();
			RB2D.simulated = false;
			OnPlayerDied ();


		}




	}










}
