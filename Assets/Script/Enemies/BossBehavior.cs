using UnityEngine;
using System.Collections;

public class BossBehavior : MonoBehaviour
{
    public float spawn = 2;
    public Vector2 target;
    public Vector2 myPos;
    public Vector2 direction;
    public float Distance;
    public float chaseRange = 15.0f;
    public float wanderSpeed = .7f;

    public GameObject player;
    public GameObject projectile;
    public float projectileSpeed = 5f;

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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        projectile = GameObject.FindGameObjectWithTag("bossBullet");
        rng = Random.Range(0, 8);
        Direction(rng);
        Quaternion end = Quaternion.Euler(whereToGo, 0, 0);
        GetComponent<Transform>().rotation = end;
        StartCoroutine(postHitTimer());
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        lastTimeIWandered = Time.time;
    }

    private void Update()
    {
        if (Distance < chaseRange)
        {
            //  Debug.Log("enemy chase");
            StartCoroutine(shoot());
        }

        if (transform.position.z > -.5f)
        {
            Vector3 oldPos = transform.position;
            transform.position = new Vector3(oldPos.x, oldPos.y, -3);
        }
        //print(Time.time - lastTimeIWandered);
        if (Time.time - lastTimeIWandered > whenIShouldWander)
        {

            rng = Random.Range(0, 8);
            Direction(rng);
            StartCoroutine(wander());
            lastTimeIWandered = Time.time;
        }
    }
    void FixedUpdate()
    {

        if (currWander == 0)
        {
            Quaternion end = Quaternion.Euler(0, 0, whereToGo);
            transform.rotation = end;
            transform.Translate(new Vector2(1,0) * wanderSpeed * Time.deltaTime, Space.Self);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.name.Contains("Decoy"))
        {
            StartCoroutine(postHitTimer());
        }

        else if (col.gameObject.name.Contains("wall") || col.gameObject.name.Contains("Wall"))
        {
            if (rng == 0)
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
                if (rng == 8)
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


        }

        else if (col.gameObject.name.Contains("octo"))
        {

                wanderSpeed = 0;
                rng = Random.Range(0, 8);
                Direction(rng);
                transform.rotation = Quaternion.Euler(0, 0, whereToGo);
                wanderSpeed = 1.5f;
                canChase = true;
        }

    }

    IEnumerator postHitTimer()
    {
        wanderSpeed = 0;
        yield return new WaitForSeconds(1);
        wanderSpeed = .7f;
    }

    IEnumerator wander()
    {
        lastTimeIWandered = Time.time;
        currWander = 0;
        Quaternion end = Quaternion.Euler(0, 0, whereToGo);
        transform.rotation = end;
        yield return new WaitForSeconds(0);
    }

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

    IEnumerator shoot()
    {
        spawn -= Time.deltaTime;
        
        if (spawn < 0)
        {
            int rot = 0;
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 8; i++)
                {


                    spawn = 5;
                    //spawn bullet and have it go to the player
                    //float x = player.GetComponent<Transform>().position.x;
                    //float y = player.GetComponent<Transform>().position.y;
                    //target = (new Vector2(x, y));

                    myPos = new Vector2(transform.position.x, transform.position.y);
                    direction = target - myPos;
                    //direction.Normalize();
                    Quaternion rotation = Quaternion.Euler(0, 0, rot);
                    rot += 45;
                    GameObject bullet = (GameObject)Instantiate(projectile, myPos, rotation);


                    //Vector3 diff = player.transform.position - transform.position;
                    //diff.Normalize();
                    //correct rotation of bullet
                    //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    //bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                    //add speed to bullet
                    if (i == 0)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * projectileSpeed;
                    }
                    if (i == 1)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, -1) * projectileSpeed;
                    }
                    if (i == 2)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * projectileSpeed;
                    }
                    if (i == 3)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * projectileSpeed;
                    }
                    if (i == 4)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * projectileSpeed;
                    }
                    if (i == 5)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * projectileSpeed;
                    }
                    if (i == 6)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * projectileSpeed;
                    }
                    if (i == 7)
                    {
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -1) * projectileSpeed;
                    }
                    yield return new WaitForSeconds(.4f);
                }
                yield return new WaitForSeconds(.8f);
            }
            spawn = 5;

        }

    }
}