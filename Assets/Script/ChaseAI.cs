using UnityEngine;
using System.Collections;

public class ChaseAI : MonoBehaviour
{
    public float Distance;
    public Transform Target;
    public GameObject player;
    bool canLookAt = true;
    public float lookAtDistance = 25.0f;
    public float lookAtSpeed = 1;
    public float chaseRange = 15.0f;
    public float moveSpeed = 5.0f;
    public float wanderSpeed = 1.2f;

    bool playSoundOnce = true;

    Animator animator;
    const int AngerIdle = 0;
    const int AngerWake = 1;
    int currentState = AngerIdle;

    float lastTimeIWandered = 0;
    float whenIShouldWander = 3;

    int currWander = 0;

    public bool canChase;

    public bool chaseTimer = false;
    Quaternion start;

    int rng;
    int whereToGo;

       int up = 270;
    int upRight = 315;
    int right = 0;
    int downRight = 45;
    int down = 90;
    int downLeft = 135;
    int left = 180;
    int upLeft = 225;

    public AudioSource redEnemySound1;
    public AudioSource redEnemySound2;
    public AudioSource redEnemySound3;
    public AudioSource redEnemySound4;
    public AudioSource redEnemySound5;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        changeState(AngerIdle);
        rng = Random.Range(0, 8);
        Direction(rng);
        Quaternion end = Quaternion.Euler(whereToGo, 270, 0);
        Target = GameObject.FindWithTag("Player").transform;
        player = GameObject.Find("Player");
        GetComponent<Transform>().rotation = end;
        canChase = true;
        StartCoroutine(postHitTimer());
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        lastTimeIWandered = Time.time;
    }

    void Update()
    {
        if (Time.time - lastTimeIWandered > whenIShouldWander)
        {
            rng = Random.Range(0, 8);
            Direction(rng);
            StartCoroutine(wander());
            lastTimeIWandered = Time.time;
        }

        if (player.GetComponent<PlayerMovement>().inChaseBar == true && canChase)
        {
            if (canLookAt == true)
            {

                transform.LookAt(Target);
                canLookAt = false;
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
            if (playSoundOnce)
            {
                changeState(AngerWake);
                chaseSound();
                playSoundOnce = false;
            }
        }
        else
        {
            changeState(AngerIdle);
            playSoundOnce = true;
            redEnemySound1.Stop();
            redEnemySound2.Stop();
            redEnemySound3.Stop();
            redEnemySound4.Stop();
            redEnemySound5.Stop();
        }
            if (currWander == 0)
            {
            Quaternion end = Quaternion.Euler(whereToGo, 270, 0);
            transform.rotation = end;
            transform.Translate(Vector3.forward * wanderSpeed * Time.deltaTime, Space.Self);
            }
        }


    void OnCollisionExit2D(Collision2D col)
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.name.Contains("Decoy"))
        {
            StartCoroutine(postHitTimer());
        }

        //else if (col.gameObject.name.Contains("obs"))
        //{

        //}
        //else if (col.gameObject.name.Contains("Enemy"))
        //{
        //    Quaternion end = Quaternion.Euler(whereToGo, 270, 0);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, end, .005f);
        //}

        else if (col.gameObject.name.Contains("wall") || col.gameObject.name.Contains("Wall"))
        {
            if (player.GetComponent<PlayerMovement>().inChaseBar == true)
            {
                StartCoroutine(chaseCoolDown());
            }
            if(rng == 0)
            {
                rng = Random.Range(3, 6);
                Direction(rng);
                StartCoroutine(wander());
            }
            else if (rng == 1)
            {
                rng = Random.Range(4, 7);
                Direction(rng);
                StartCoroutine(wander());
            }
            else if (rng == 2)
            {
                rng = Random.Range(5, 8);
                Direction(rng);
                StartCoroutine(wander());
            }
            else if (rng == 3)
            {
                rng = Random.Range(6, 8);
                if(rng == 8)
                {
                    rng = 0;
                }
                Direction(rng);
                StartCoroutine(wander());
            }
            else if (rng == 4)
            {
                rng = Random.Range(0, 3);
                if (rng == 2)
                {
                    rng = 7;
                }
                Direction(rng);
                StartCoroutine(wander());
            }
            else if (rng == 5)
            {
                rng = Random.Range(0, 3);
                Direction(rng);
                StartCoroutine(wander());
            }
            else if (rng == 6)
            {
                rng = Random.Range(1, 4);
                Direction(rng);
                StartCoroutine(wander());
            }
            else if (rng == 7)
            {
                rng = Random.Range(2, 5);
                Direction(rng);
                StartCoroutine(wander());
            }

            player.GetComponent<PlayerMovement>().inChaseBar = false;
                
        }
       
        else if (col.gameObject.name.Contains("octo"))
        {

            if (player.GetComponent<PlayerMovement>().inChaseBar == true)
            {
                StartCoroutine(chaseCoolDown());
            }
            else
            {
                player.GetComponent<PlayerMovement>().inChaseBar = false;
                canChase = false;
                wanderSpeed = 0;
                rng = Random.Range(0, 8);
                Direction(rng);
                transform.rotation = Quaternion.Euler(whereToGo, 270, 0);
                wanderSpeed = 1.2f;
                canChase = true;
            }
        }

    }

    public void startWander()
    {
       // StartCoroutine(hitWallTimer());
    }


    IEnumerator chaseCoolDown()
    {
        ///NOT COOLING DOWN WHEN COLLIDE WITH WALL AFTER CHASE??????
        moveSpeed = 0;
        canChase = false;
        canLookAt = false;

        yield return new WaitForSeconds(1f);
        moveSpeed = 6.5f;
        canChase = true;
        canLookAt = true;
        player.GetComponent<PlayerMovement>().inChaseBar = false;
        canChase = false;
        wanderSpeed = 0;
        rng = Random.Range(0, 8);
        Direction(rng);
        transform.rotation = Quaternion.Euler(whereToGo, 270, 0);
        wanderSpeed = 1.2f;
        canChase = true;
    }

    IEnumerator postHitTimer()
    {
        moveSpeed = 0;
        canChase = false;
        yield return new WaitForSeconds(1);
        canChase = true;
        moveSpeed = 6.5f;
    }

    IEnumerator wander()
    {
        lastTimeIWandered = Time.time;
        currWander = 0;
        Quaternion end = Quaternion.Euler(whereToGo, 270, 0);
        transform.rotation = end;
        yield return new WaitForSeconds(0);
    }

    //IEnumerator hitWallTimer()
    //{

    //    ////canChase = false;
    //    ////canLookAt = false;
    //    //wanderSpeed = 0f;
    //    //rng = Random.Range(0, 8);
    //    //Direction(rng);
    //    //Quaternion end = Quaternion.Euler(whereToGo, 270, 0);
    //    //transform.rotation = end;
    //    //yield return new WaitForSeconds(1);
    //    //wanderSpeed = 1.5f;
    //    //canChase = true;
    //    //canLookAt = true;
    //}

    void Direction(int rng)
    {
        if (rng == 0)
        {
            whereToGo = right;
        }
        else if (rng == 1)
        {
            whereToGo = upRight;
        }
        else if (rng == 2)
        {
            whereToGo = right;
        }
        else if (rng == 3)
        {
            whereToGo = downRight;
        }
        else if (rng == 4)
        {
            whereToGo = down;
        }
        else if (rng == 5)
        {
            whereToGo = downLeft;
        }
        else if (rng == 6)
        {
            whereToGo = left;
        }
        else if (rng == 7)
        {
            whereToGo = upLeft;
        }
    }
    void changeState(int state)
    {

        if (currentState == state)
            return;

        switch (state)
        {

            case AngerIdle:
                animator.SetInteger("state", AngerIdle);
                break;

            case AngerWake:
                animator.SetInteger("state", AngerWake);
                break;

           

        }

        currentState = state;
    }

    void chaseSound()
    {
        int rng = Random.Range(0, 5);
        if (rng == 0)
        {
            redEnemySound1.Play();
        }
        else if (rng == 1)
        {
            redEnemySound2.Play();
        }
        else if (rng == 2)
        {
            redEnemySound3.Play();
        }
        else if (rng == 3)
        {
            redEnemySound4.Play();
        }
        else if (rng == 4)
        {
            redEnemySound5.Play();
        }
    }

}