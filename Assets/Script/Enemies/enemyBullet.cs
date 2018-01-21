using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "GreenWall" || col.gameObject.tag == "BlueWall" || col.gameObject.tag == "YellowWall" || col.gameObject.tag == "RedWall")
        {
            Destroy(gameObject);
        }
    }
}
