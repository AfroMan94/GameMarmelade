using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTestScript : MonoBehaviour {

    private float speed;

	// Use this for initialization
	void Start () {
        speed = GameObject.Find("mainObject").GetComponent<PublicValuesHandler>().obstaclesSpeed;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.Translate(Vector2.left * Time.deltaTime*speed);
		
	}
}
