using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public delegate void PlayerDelegate();
	public static event PlayerDelegate OnPlayerDied;
	public static event PlayerDelegate OnPlayerScored;


	public float tapForce = 10f;
	public float tileSmooth = 5f;
	public float rainbowDuration;
	public Vector3 startPos;

	Rigidbody2D RB2D;
	Quaternion downRotation;
	Quaternion forwardRotation;
	GameManager game;

	public GameObject fartPrefab;
	public Transform spawnLocation;
	public AudioSource fartSFX;
	public AudioSource scoreSFX;
	public AudioSource dieSFX;
	public bool hasPower = false;
	private CircleCollider2D body2D;






	void Start()
	{
		
		RB2D = GetComponent<Rigidbody2D> ();
		downRotation = Quaternion.Euler (0, 0, -90);
		forwardRotation = Quaternion.Euler (0, 0, 35);
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

	void Update(){

		if (game.GameOver) 
			return;
		


		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space)) {
			FartSpawn ();
			transform.rotation = forwardRotation;
			RB2D.velocity = Vector3.zero;
			RB2D.AddForce (Vector2.up * tapForce, ForceMode2D.Force);

		}


		transform.rotation = Quaternion.Lerp (transform.rotation, downRotation, tileSmooth * Time.deltaTime);



	}


	void FartSpawn(){

		Instantiate (fartPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
		fartSFX.Play ();
	}





	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("ScoreZone")) 
		{

			scoreSFX.Play ();
			OnPlayerScored ();
		}




		if (other.gameObject.CompareTag ("DeadZone")&& !hasPower) 
		{
			
			dieSFX.Play ();
			RB2D.simulated = false;
			OnPlayerDied ();
		

		}




	}





}
