using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIHelper : MonoBehaviour {


    GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("Wall") && player.GetComponent<PlayerMovement>().inChaseBar == false)
        {
            GetComponent<Collider2D>().enabled = false;
            transform.parent.GetComponent<ChaseAI>().startWander();
            StartCoroutine(postHit());
        }
    }

    IEnumerator postHit()
    {
        yield return new WaitForSeconds(.25f);
       GetComponent<Collider2D>().enabled = true;
    }
}
