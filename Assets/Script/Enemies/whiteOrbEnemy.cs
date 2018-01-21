using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteOrbEnemy : MonoBehaviour
{

    // Use this for initialization
    public float Distance;
    public Transform Target;
    public GameObject player;
    public float lookAtDistance = 25.0f;
    public float lookAtSpeed = 1;
    public float chaseRange = 15.0f;
    public float moveSpeed = .8f;
    public float projectileSpeed = 5f;
    public Vector2 target;
    public Vector2 myPos;
    public Vector2 direction;
    public float spawn = 1;

    bool canColWithOtherWhiteEnemies = true;

    void Start()
    {
        spawn = 1;
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
        spawn -= Time.deltaTime;
        if (spawn < 0)
        {
            int randomNumber = Random.Range(0, this.gameObject.transform.childCount/2);
            //MAKESURE TO ADJUST FOR WHEN YOU KILL ONE CHILD
            if (gameObject.transform.GetChild(randomNumber).gameObject.GetComponent<whiteEnemyOrbs>().hasBeenFired == true || gameObject.transform.GetChild(randomNumber).gameObject == null)
            {
                while (gameObject.transform.GetChild(randomNumber).gameObject.GetComponent<whiteEnemyOrbs>().hasBeenFired == true || gameObject.transform.GetChild(randomNumber).gameObject == null)
                {
                    randomNumber = Random.Range(0, this.gameObject.transform.childCount / 2 - 1);
                    //MAKE SURE TO CHECK IF ALL CHILDREN ARE DEAD!!!
                }
            }
            float x = player.GetComponent<Transform>().position.x;
            float y = player.GetComponent<Transform>().position.y;
            target = (new Vector2(x, y));

            GameObject projectile = gameObject.transform.GetChild(randomNumber).gameObject;
            projectile.GetComponent<whiteEnemyOrbs>().hasBeenFired = true;

            myPos = new Vector2(transform.position.x, transform.position.y);
            direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.identity;
            //GameObject bullet = (GameObject)Instantiate(projectile, myPos, rotation);
            Vector3 diff = player.transform.position - transform.position;
            diff.Normalize();
            //correct rotation of bullet
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
            print(gameObject.transform.GetChild(randomNumber).gameObject);
            //add speed to bullet
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            spawn = 3;
            //projectile.GetComponent<whiteEnemyOrbs>().enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.name.Contains("Decoy"))
        {
            StartCoroutine(postHitTimer());
        }
    }


    //tried to fix white enmies colliding with eachother, did not work
    private void OnTriggerEnter2D(Collider2D col)
    {
     //if (col.gameObject.name.Contains("OrbSheild"))
     //   {
     //       if (canColWithOtherWhiteEnemies == true)
     //       {
     //           StartCoroutine(hitOtherWhiteEnemy());
     //           float randSpeedChanger = Random.Range(-.4f, .4f);
     //           moveSpeed += randSpeedChanger;
     //           if (moveSpeed >= 1.4f)
     //           {
     //               moveSpeed = 1.4f;
     //           }
     //           if(moveSpeed < 1f)
     //           {
     //               moveSpeed = 1;
     //           }
     //       }
     //   }
    }


    IEnumerator postHitTimer()
    {
        if (moveSpeed != 0)
        {
            float oldmoveSpeed = moveSpeed;
            moveSpeed = 0;
            yield return new WaitForSeconds(1);
            moveSpeed = oldmoveSpeed;
        }
    }

    //IEnumerator hitOtherWhiteEnemy()
    //{
    //    canColWithOtherWhiteEnemies = false;
    //    yield return new WaitForSeconds(4);
    //    canColWithOtherWhiteEnemies = true;
    //}
}
