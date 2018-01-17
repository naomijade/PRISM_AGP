using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedExplosionEnemy : MonoBehaviour
{

    public float Distance;
    public Transform Player;
    public GameObject player;
    public float lookAtDistance = 5.0f;
    public float chaseRange = 3.0f;

    public GameObject projectile;
    GameObject camera;

    public AudioSource fireSound1;
    public AudioSource fireSound2;
    public AudioSource fireSound3;
    public AudioSource fireSound4;
    public AudioSource prepareSound;
    bool playAudioOnce = true;
    public bool hasSD = false;
    public bool canIterateER = true;

    public Vector2 target;
    public Vector2 myPos;
    public Vector2 direction;
    public float SDTime = 3;
    public float projectileSpeed = 5f;
    bool colorChanging = false;


    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        camera = GameObject.FindWithTag("MainCamera");
        GetComponent<SpriteRenderer>().color = new Color(.75f, .5f, .5f);
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
            if (colorChanging == false)
            {
                colorChanging = true;
                StartCoroutine(SDColorChange());
            }
        }
        if (Distance > chaseRange)
        {
            SDTime = 2;
            colorChanging = false;
            GetComponent<SpriteRenderer>().color = new Color(.75f, .5f, .5f);
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
        SDTime -= Time.deltaTime;

        if (SDTime < 0 && hasSD == false)
        {
            hasSD = true;
            myPos = new Vector2(transform.position.x, transform.position.y);
            Quaternion rotation = Quaternion.identity;
            GameObject SD = (GameObject)Instantiate(projectile, myPos, rotation);
            StartCoroutine(SDTimer(SD));
        }
    }

    IEnumerator SDTimer(GameObject currSD)
    {
        //SDSound.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        float i = 0;
        Vector3 j = currSD.transform.localScale;
        while (i < 7 && currSD != null)
        {
            i = currSD.transform.localScale.x;
            currSD.transform.localScale = new Vector3(j.x + .15f, j.y + .15f, j.z + .15f);
            yield return new WaitForSeconds(.001f);
            if (currSD != null)
            {
                j = currSD.transform.localScale;
            }
        }
        Destroy(currSD);
        if (canIterateER == true)
        {
            camera.GetComponent<randomSpawner>().enemiesRemaining--;
        }
        Destroy(gameObject);

    }

    IEnumerator SDColorChange()
    {
        float i = 1;
        while (colorChanging)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            if (i > .1f)
            {
                i = i / 1.7f;
            }
            yield return new WaitForSeconds(i);
            GetComponent<SpriteRenderer>().color = new Color(.75f, .5f, .5f);
            yield return new WaitForSeconds(i);
        }
    }

    void shootSound()
    {
        //int rng = Random.Range(0, 4);
        //if (rng == 0)
        //{
        //    fireSound1.Play();
        //}
        //else if (rng == 1)
        //{
        //    fireSound2.Play();
        //}
        //else if (rng == 2)
        //{
        //    fireSound3.Play();
        //}
        //else if (rng == 3)
        //{
        //    fireSound4.Play();
        //}
    }

}
