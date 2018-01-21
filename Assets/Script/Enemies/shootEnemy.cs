
using UnityEngine;
public class shootEnemy : MonoBehaviour
{
    public float spawn = 4;
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
    int shotsFired;
    public AudioSource fireSound1;
    public AudioSource fireSound2;
    public AudioSource fireSound3;
    public AudioSource fireSound4;
    public AudioSource fireSound5;

    Animator animator;
    const int sadnessIdle = 0;
    const int sadnessShoot = 1;
    int currentState = sadnessIdle;

    // Use this for initialization
    void Start()
    {
        spawn = 1;
        shotsFired = 0;
        Target = GameObject.FindWithTag("Player").transform;
        animator = this.GetComponent<Animator>();

    }

    // Update is called once per frame
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
            changeState(sadnessShoot);
            //spawn bullet and have it go to the player
            float x = player.GetComponent<Transform>().position.x;
            float y = player.GetComponent<Transform>().position.y;
            target = (new Vector2(x, y));

            myPos = new Vector2(transform.position.x, transform.position.y);
            direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.identity;
            GameObject bullet = (GameObject)Instantiate(projectile, myPos, rotation);
            Vector3 diff = player.transform.position - transform.position;
            diff.Normalize();
            //correct rotation of bullet
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

            //add speed to bullet
            bullet.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            shotsFired++;
            shootSound();
            if (shotsFired == 3)
            {
                GameObject bulletTwo = (GameObject)Instantiate(projectile, myPos, rotation);
                GameObject bulletThree = (GameObject)Instantiate(projectile, myPos, rotation);

                shotsFired = 0;
                Vector3 diffTwo = player.transform.position - transform.position;
                diffTwo.Normalize();
                float rot_zTwo = Mathf.Atan2(diffTwo.y, diffTwo.x) * Mathf.Rad2Deg;
                bulletTwo.transform.rotation = Quaternion.Euler(0f, 0f, rot_zTwo + 90);

                //add speed to bullet
                direction = (target - myPos) - new Vector2(4, 0);
                direction.Normalize();
                bulletTwo.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;


                Vector3 diffThree = player.transform.position - transform.position;
                diffThree.Normalize();
                float rot_zThree = Mathf.Atan2(diffThree.y, diffThree.x) * Mathf.Rad2Deg;
                bulletThree.transform.rotation = Quaternion.Euler(0f, 0f, rot_zThree + 90);
                direction = (target - myPos) + new Vector2(4, 0);
                direction.Normalize();
                //add speed to bullet
                bulletThree.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                shootSound();
                shootSound();
            }

            spawn = 2;
          
        }
        else if (spawn < 1.5)
        {
            changeState(sadnessIdle);
        }

    }

    void shootSound()
    {
        int rng = Random.Range(0, 5);
        if(rng == 0)
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
        else if(rng == 3)
        {
            fireSound4.Play();
        }
        else if(rng == 4)
        {
            fireSound5.Play();
        }
    }

    void changeState(int state)
    {

        if (currentState == state)
            return;

        switch (state)
        {

            case sadnessIdle:
                animator.SetInteger("state", sadnessIdle);
                break;

            case sadnessShoot:
                animator.SetInteger("state", sadnessShoot);
                break;
        }

        currentState = state;
    }
}