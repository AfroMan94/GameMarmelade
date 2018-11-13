using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicValuesHandler : MonoBehaviour {

    public float obstaclesSpeed;
    public float basicSpeed;
    
    bool swiped = false;

    public bool swipeRight = false;

    public PlayerScript swipe;

    // Use this for initialization
    void Start () {

        basicSpeed = obstaclesSpeed;
        
	}
	
	// Update is called once per frame
	void Update () {

        //Changing global variable for obstacles speed for .3sec 
        if (swipeRight)
        {
            Debug.Log("swipeConfirmed speedup!");
            obstaclesSpeed = 50;
            StartCoroutine("DashCount");
        }
    }

    IEnumerator DashCount()
    {
        swipeRight = false;
        yield return new WaitForSeconds(.3f);
        obstaclesSpeed = basicSpeed;
    }
    
}
