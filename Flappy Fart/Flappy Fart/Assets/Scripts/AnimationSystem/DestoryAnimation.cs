using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAnimation : MonoBehaviour {



	public float deathTimer;

	void Start () {

		Destroy (gameObject, deathTimer);
		
	}
	

}
