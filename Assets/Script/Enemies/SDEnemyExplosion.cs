using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SDEnemyExplosion : MonoBehaviour
{
    public ParticleSystem exploPart;
    GameObject bloodStain;
    ParticleSystem currExplo;
    GameObject mainCam;

    // Use this for initialization
    void Start()
    {
        Instantiate(exploPart, transform.position, Quaternion.identity).transform.parent = gameObject.transform;
        Destroy(gameObject, .6f);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        bloodStain = GameObject.Find("blood-splatter-hd-pixel-design");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "NPCHolder")
        {
            Destroy(col.gameObject);
            Instantiate(bloodStain, col.gameObject.GetComponent<Transform>().position, Quaternion.identity);
        }

        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<PlayerMovement>().canBeHit == true)
            {
                col.gameObject.GetComponent<PlayerMovement>().playerHP--;
                col.gameObject.GetComponent<PlayerMovement>().takenDamage();
                GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().playerHP;
                col.gameObject.GetComponent<PlayerMovement>().updateHPUI();
            }
        }
        if (col.gameObject.tag == "SDEnemyRed")
        {
            col.gameObject.GetComponent<RedExplosionEnemy>().canIterateER = false;
            col.gameObject.GetComponent<RedExplosionEnemy>().chaseRange = 100;
            col.gameObject.GetComponent<RedExplosionEnemy>().SDTime = -1;           
            mainCam.GetComponent<randomSpawner>().enemiesRemaining--;
        }
    }
}
