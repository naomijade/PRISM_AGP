using UnityEngine;
using System.Collections;


public class fireBullet : MonoBehaviour
{
    public KeyCode fire;
    public Vector2 target;
    public Vector2 myPos;
    public Vector2 direction;

    public AudioSource fireSound;
    public AudioSource cantFireInAOESound;

    public float projectileSpeed = 5f;

    public GameObject strongProjectile;
    //public GameObject weakProjectile;

    public bool canShoot;

    //true if not by the yellow enemy
    public bool notInAOE;
    public float cooldown = 2f;
    

    void Start()
    {
        //user starts able to shoot
        canShoot = true;

    }

    // Update is called once per frame
    void Update()
    {
        

        //checks to see if user is trying to fire when in the AOE, plays error sound
        if (Input.GetKeyDown(fire) && canShoot &&  notInAOE == false)
        {
            cantFireInAOESound.Play();
        }

        //they can fire and and they are not in the AOE
        if (Input.GetKeyDown(fire) && canShoot && notInAOE)
        {
            fireSound.Play();
            Fire();
        }
    }
    //}

    //Instantiate a bullet and fire it with variable speed
    void Fire()
    {
        //target is the mouse position
        target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        //your position
        myPos = new Vector2(transform.position.x, transform.position.y);

        //tragectory of the bullet
        direction = target - myPos;
        direction.Normalize();

        //bullet rotation based on the direction
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        GameObject bullet;

        //if (GameObject.Find("Player").GetComponent<PlayerMovement>().bulletHearts > 0)
        //{

            //spawn the bullet
            //GameObject.Find("Player").GetComponent<PlayerMovement>().bulletHearts--;
            bullet = (GameObject)Instantiate(strongProjectile, myPos, rotation);
            projectileSpeed = 10;
            //cooldown = .5f;
        //}
        //else
        //{
        //    bullet = (GameObject)Instantiate(weakProjectile, myPos, rotation);
        //    projectileSpeed = 6f;
        //    //cooldown = .5f;
        //}

        //add velocity to bullet so it moves towards the enemy
        bullet.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        //start bullet cooldown
        canShoot = false;
        StartCoroutine(ShootDelay());
    }

    //manage time between shots
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
