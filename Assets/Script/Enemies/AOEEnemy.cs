using UnityEngine;
using System.Collections;

public class AOEEnemy : MonoBehaviour
{

    public float Distance;
    public Transform Player;
    public GameObject player;
    public float lookAtDistance = 25.0f;
    public float lookAtSpeed = 1;
    public float chaseRange = 15.0f;
    public float moveSpeed = 5.0f;

    GameObject preProjectile;

    public AudioSource fireSound1;
    public AudioSource fireSound2;
    public AudioSource fireSound3;
    public AudioSource fireSound4;
    public AudioSource prepareSound;
    bool playAudioOnce = true;

    public Vector2 target;
    public Vector2 myPos;
    public Vector2 direction;
    public float spawn = 5;
    public GameObject projectile;
    public float projectileSpeed = 5f;


    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        player = GameObject.FindWithTag("Player");
        projectile = GameObject.Find("AOEHolder");
        preProjectile = GameObject.Find("preAOE");

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startWidth = 0.05f;
        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {

        //if (player == null)
        //{
        //    player = GameObject.FindWithTag("Player");
        //}

        //Gauge the distance to the player. Line in 3d space.Draws a line from source to Target.
        Distance = Vector2.Distance(Player.position, transform.position);

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
        if (Distance > chaseRange)
        {
            //Target.GetComponent<fireBullet>().notInAOE = true;
        }

    }

    // Turn to face the player.
    void lookAt()
    {
        // Rotate to look at player.
        //Quaternion rotation = Quaternion.LookRotation(Target.position - transform.position);
        //Quaternion rotation = Quaternion.Euler(0,-90,0);
        ////transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookAtSpeed * Time.deltaTime);
        //transform.rotation = rotation;
        // transform.LookAt(Target); //alternate way to track player replaces both lines above.
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
    }


    void chase()
    {
        //Target.GetComponent<fireBullet>().notInAOE = false;
        spawn -= Time.deltaTime;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

            if (spawn < 1)
        {
            if (Distance < 11)
            {
                if (playAudioOnce)
                {
                    prepareSound.Play();
                    playAudioOnce = false;
                }
                float x = Player.GetComponent<Transform>().position.x;
                float y = Player.GetComponent<Transform>().position.y;
                target = (new Vector2(x, y));

                myPos = new Vector2(transform.position.x, transform.position.y);
                direction = target - myPos;
                direction.Normalize();
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, myPos);
                lineRenderer.SetPosition(1, target);
                
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }

        if (spawn < 0)
        {
            prepareSound.Stop();
           // prepareSound.Play();
            lineRenderer.positionCount = 0;
            //spawn bullet and have it go to the player
            float x = Player.GetComponent<Transform>().position.x;
            float y = Player.GetComponent<Transform>().position.y;
            target = (new Vector2(x, y));

            myPos = new Vector2(transform.position.x, transform.position.y);
            direction = target - myPos;
            direction.Normalize();
            Quaternion rotation = Quaternion.identity;
            GameObject bullet = (GameObject)Instantiate(projectile, myPos, rotation);
            bullet.transform.parent = gameObject.transform;
            //correct rotation of bullet
            Vector3 diff = Player.transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

            StartCoroutine(destroyTimer());
            Destroy(bullet, 1.7f);
            shootSound();


            //reset spawn, keep in mind the amount of time it took to destroy
            spawn = 2 + 3;
            playAudioOnce = true;
        }
    }

    IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(1.7f);
        Collider2D col = player.GetComponent<Collider2D>();
        player.GetComponent<PlayerMovement>().OnTriggerExit2D(col);
        player.GetComponent<fireBullet>().notInAOE = true;

    }

    void OnDestroy()
    {

        if (player != null)
        {
            player.GetComponent<fireBullet>().notInAOE = true;
        }
    }

    void shootSound()
    {
        int rng = Random.Range(0, 4);
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
    }

}
