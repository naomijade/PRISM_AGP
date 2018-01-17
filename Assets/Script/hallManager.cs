using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hallManager : MonoBehaviour {

    public bool hasBeenTraversed;

	// Use this for initialization
	void Start () {
        hasBeenTraversed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            StartCoroutine(doorTimeManager());
        }

    }


    IEnumerator doorTimeManager()
    {
        yield return new WaitForSeconds(5);
        hasBeenTraversed = true;
    }
    }
