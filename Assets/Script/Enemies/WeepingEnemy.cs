using UnityEngine;
using System.Collections;

public class WeepingEnemy : MonoBehaviour
{

    public float Distance;
    public Transform Target;
    public float lookAtDistance = 25.0f;
    public float lookAtSpeed = 1;
    public float chaseRange = 15.0f;
    public float moveSpeed = 5.0f;
    Animator animator;
    const int envyIdle = 0;
    const int envyWake = 1;
    const int envyWalk = 2;
    const int envySleep = 3;
    int currentState = envyIdle;
    public AudioSource greenEnemySound1;
    public AudioSource greenEnemySound2;
    public AudioSource greenEnemySound3;

    bool playAudioOnce = true;

    public bool canMove = true;



    void Start()
    {
        Target = GameObject.FindWithTag("Player").transform;
        animator = this.GetComponent<Animator>();
        canMove = true;
}


    void Update()
    {

        //Gauge the distance to the player. Line in 3d space.Draws a line from source to Target.
        Distance = Vector2.Distance(Target.position, transform.position);

        //AI begins tracking player.
        if (Distance < lookAtDistance)
        {
            lookAt();
        }

        //Attack!Chase the player until /if player leaves attack range.
        if (Distance < chaseRange)
        {
            //  Debug.Log("enemy chase");
            chase();
        }
        if(moveSpeed == 0)
        {
            changeState(envyIdle);
            playAudioOnce = true;
            greenEnemySound1.Stop();
            greenEnemySound2.Stop();
            greenEnemySound3.Stop();
        }
    }

    // Turn to face the player.
    void lookAt()
    {
        // Rotate to look at player.
        Quaternion rotation = Quaternion.Euler(0,0,0);
    }

    void chase()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), Target.position, moveSpeed * Time.deltaTime);
        if (moveSpeed > 0)
        {
            changeState(envyWalk);
            if (playAudioOnce)
            {
                greenEnemySound();
                playAudioOnce = false;
            }
        }
    }
    void changeState(int state)
    {

        if (currentState == state)
            return;

        switch (state)
        {

            case envyWalk:
                animator.SetInteger("state", envyWalk);
                break;

            case envyIdle:
                animator.SetInteger("state", envyIdle);
                break;

            case envyWake:
                animator.SetInteger("state", envyWake);
                break;

            case envySleep:
                animator.SetInteger("state", envySleep);
                break;

        

        }

        currentState = state;
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
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
        moveSpeed = 0;
    }

    void greenEnemySound()
    {
        int rng = Random.Range(0, 3);
        if (rng == 0)
        {
            greenEnemySound1.Play();
        }
        else if (rng == 1)
        {
            greenEnemySound2.Play();
        }
        else if (rng == 2)
        {
            greenEnemySound3.Play();
        }
    }

}