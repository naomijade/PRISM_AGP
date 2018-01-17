using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class greenShootEnemy : MonoBehaviour
{
    public float spawn = 3;
    public GameObject bullet;
    public float Distance;
    public Transform Target;
    public float lookAtDistance = 25.0f;
    public float lookAtSpeed = 1;
    public float chaseRange = 15.0f;
    public Vector2 target;
    public Vector2 myPos;
    public Vector2 direction;
    public GameObject player;
    public GameObject projectile;
    public float projectileSpeed = 5f;
    public int shotsToFire;
    public int maxShotsToFire;
    public AudioSource fireSound1;
    public AudioSource fireSound2;
    public AudioSource fireSound3;
    public AudioSource fireSound4;
    public AudioSource fireSound5;

    //Animator animator;
    //const int sadnessIdle = 0;
    //const int sadnessShoot = 1;
    //int currentState = sadnessIdle;

    // Use this for initialization
    void Start()
    {
        spawn = 1;
        shotsToFire = 1;
        Target = GameObject.FindWithTag("Player").transform;
        //animator = this.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Gauge the distance to the player. Line in 3d space.Draws a line from source to Target.
        if (Target != null)
        {
            Distance = Vector3.Distance(Target.position, transform.position);
        }

        //AI begins tracking player.
        //if (Distance < lookAtDistance)
        //{
        //    lookAt();
        //}
        if(shotsToFire >= maxShotsToFire)
        {
            shotsToFire = maxShotsToFire;
        }
        //Attack!Chase the player until /if player leaves attack range.
        if (Distance < chaseRange)
        {
            //  Debug.Log("enemy chase");
            shoot();
        }
    }
    void lookAt()
    {
        // Rotate to look at player.
        //Quaternion rotation = Quaternion.LookRotation(Target.position - transform.position);

        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookAtSpeed * Time.deltaTime);
        //transform.LookAt(Target); //alternate way to track player replaces both lines above.
    }
    void shoot()
    {
        spawn -= Time.deltaTime;
        if (spawn < 0)
        {
            StartCoroutine(shootBullets());
            spawn = 3f;

        }
        //else if (spawn < 1.5)
        //{
        //    changeState(sadnessIdle);
        //}

    }

    void shootSound()
    {
        int rng = Random.Range(0, 5);
        if (rng == 0)
        {
            fireSound1.Play();
        }
        else if (rng == 1)
        {
            fireSound2.Play();
        }
        else if (rng == 2)
        {
            fireSound3.Play();
        }
        else if (rng == 3)
        {
            fireSound4.Play();
        }
        else if (rng == 4)
        {
            fireSound5.Play();
        }
    }

    //void changeState(int state)
    //{

    //    if (currentState == state)
    //        return;

    //    switch (state)
    //    {

    //        case sadnessIdle:
    //            animator.SetInteger("state", sadnessIdle);
    //            break;

    //        case sadnessShoot:
    //            animator.SetInteger("state", sadnessShoot);
    //            break;
    //    }

    //    currentState = state;
    //}

    IEnumerator shootBullets()
    {
        for (int i = 0; i < shotsToFire; i++)
        {
            //spawn bullet and have it go to the player
            float x = Target.GetComponent<Transform>().position.x;
            float y = Target.GetComponent<Transform>().position.y;
            target = (new Vector2(x, y));

            myPos = new Vector2(transform.position.x, transform.position.y);
            direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.identity;
            GameObject bullet = (GameObject)Instantiate(projectile, myPos, rotation);
            Vector3 diff = Target.transform.position - transform.position;
            diff.Normalize();
            //correct rotation of bullet
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

            //add speed to bullet
            bullet.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            shootSound();
            yield return new WaitForSeconds(1f);
        }
    }

}