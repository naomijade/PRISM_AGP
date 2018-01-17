using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(startTimer());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator startTimer()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<Collider2D>().enabled = true;
    }
}
