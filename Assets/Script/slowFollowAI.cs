using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowFollowAI : MonoBehaviour {

    // Use this for initialization
    public float Distance;
    public Transform Target;
    public GameObject player;
    public float lookAtDistance = 25.0f;
    public float lookAtSpeed = 1;
    public float chaseRange = 15.0f;
    public float moveSpeed = 5.0f;
    void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;
        player = GameObject.Find("Player");
        GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
    }


    void Update()
    {
        //Gauge the distance to the player. Line in 3d space.Draws a line from source to Target.
        Distance = Vector3.Distance(Target.position, transform.position);

        //AI begins tracking player.
        if (Distance < lookAtDistance)
        {
            lookAt();
        }

        //Attack!Chase the player until /if player leaves attack range.
        if (Distance < chaseRange)
        {
            chase();
        }

    }

    // Turn to face the player.
    void lookAt()
    {
        // Rotate to look at player.
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
    }

    void chase()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), Target.position, moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.name.Contains("Decoy"))
        {
            StartCoroutine(postHitTimer());
        }
    }


        IEnumerator postHitTimer()
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(1);
        moveSpeed = 1.5f;
    }
}
