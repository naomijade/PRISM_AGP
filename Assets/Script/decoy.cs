using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decoy : MonoBehaviour
{
    int decoyHealth = 5;
    bool canBeHit = true;
    GameObject camera;
    GameObject player;
    public GameObject currDecoy;
    int roomsTraversed;
    // Use this for initialization
    void Start()
    {
        currDecoy = this.gameObject;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        roomsTraversed = player.GetComponent<PlayerMovement>().roomsTraversed;
    }

    // Update is called once per frame
    void Update()
    {
        if (roomsTraversed < player.GetComponent<PlayerMovement>().roomsTraversed)
        {
            camera.GetComponent<randomSpawner>().decoyDroped = false;
            Destroy(gameObject);
        }
        if (decoyHealth <= 0)
        {
            camera.GetComponent<randomSpawner>().decoyDroped = false;
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "RedEnemy" || col.gameObject.tag == "BlueEnemy" || col.gameObject.tag == "YellowEnemy" || col.gameObject.tag == "StaticEnemy" || col.gameObject.tag == "GreenEnemy" || col.gameObject.tag == "Boss" || col.gameObject.tag == "SDEnemyRed")
        {
            if (canBeHit)
            {
                StartCoroutine(iFrames());
                decoyHealth--;
                StartCoroutine(hitBounce(col.gameObject));
            }
        }

        if (col.gameObject.tag == "Bullet")
        {
            if (canBeHit)
            {
                StartCoroutine(iFrames());
                decoyHealth--;
                StartCoroutine(hitBounce(col.gameObject));
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemybullet" || col.gameObject.tag == "bossBullet" || col.gameObject.name.Contains("Orb") || col.gameObject.name.Contains("wallRunner"))
        {
            if (canBeHit)
            {
                StartCoroutine(iFrames());
                if (col.gameObject.tag == "Enemybullet" || col.gameObject.tag == "bossBullet")
                {
                    Destroy(col.gameObject);
                }
                decoyHealth--;
                StartCoroutine(hitBounce(col.gameObject));
            }
        }
    }

    IEnumerator hitBounce(GameObject col)
    {
        Vector3 dir = col.transform.position - transform.position;
        // We then get the opposite (-Vector3) and normalize it
        dir = -dir.normalized;
        // And finally we add force in the direction of dir and multiply it by force. 
        // This will push back the player
        GetComponent<Rigidbody2D>().AddForce(dir * 100);

        yield return new WaitForSeconds(.2f);

        GetComponent<Rigidbody2D>().Sleep();
    }
    IEnumerator iFrames()
    {
        canBeHit = false;
        yield return new WaitForSecondsRealtime(1.5f);
        canBeHit = true;
    }

}
