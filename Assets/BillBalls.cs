using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBalls : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Sheild"))
        {
            if (gameObject.name == "BlueBillBall")
            {
                if (col.gameObject.name.Contains("Blue"))
                {
                    Destroy(col.gameObject);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = -(gameObject.GetComponent<Rigidbody2D>().velocity);
                    gameObject.GetComponent<Rigidbody2D>().angularVelocity = -(gameObject.GetComponent<Rigidbody2D>().angularVelocity);
                }
            }
            if (gameObject.name == "GreenBillBall")
            {
                if (col.gameObject.name.Contains("Green"))
                {
                    Destroy(col.gameObject);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = -(gameObject.GetComponent<Rigidbody2D>().velocity);
                    gameObject.GetComponent<Rigidbody2D>().angularVelocity = -(gameObject.GetComponent<Rigidbody2D>().angularVelocity);
                }
            }
            if (gameObject.name == "YellowBillBall")
            {
                if (col.gameObject.name.Contains("Yellow"))
                {
                    Destroy(col.gameObject);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = -(gameObject.GetComponent<Rigidbody2D>().velocity);
                    gameObject.GetComponent<Rigidbody2D>().angularVelocity = -(gameObject.GetComponent<Rigidbody2D>().angularVelocity);
                }
            }
            if (gameObject.name == "RedBillBall")
            {
                if (col.gameObject.name.Contains("Red"))
                {
                    Destroy(col.gameObject);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = -(gameObject.GetComponent<Rigidbody2D>().velocity);
                    gameObject.GetComponent<Rigidbody2D>().angularVelocity = -(gameObject.GetComponent<Rigidbody2D>().angularVelocity);
                }
            }

        }
        if (gameObject.name == "YellowBillBall")
        {
            if (col.gameObject.tag == "YellowEnemy")
            {
                killedAnEnemy();
                Destroy(col.gameObject);
            }
        }
    }
        void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.name == "BlueBillBall")
        {
            if (col.gameObject.tag == "BlueEnemy")
            {
                killedAnEnemy();
                Destroy(col.gameObject);
            }
        }
        if (gameObject.name == "RedBillBall")
        {
            if (col.gameObject.tag == "RedEnemy" || col.gameObject.tag == "SDEnemyRed")
            {
                killedAnEnemy();
                Destroy(col.gameObject);
            }
        }

        if (gameObject.name == "YellowBillBall")
        {
            if (col.gameObject.tag == "YellowEnemy")
            {
                killedAnEnemy();
                Destroy(col.gameObject);
            }
        }

        if (gameObject.name == "GreenBillBall")
        {
            if (col.gameObject.tag == "GreenEnemy" || col.gameObject.name.Contains("greenShootEnemy") || col.gameObject.name.Contains("GreenOrb"))
            {
                killedAnEnemy();
                Destroy(col.gameObject);
            }
        }
    }

    void killedAnEnemy()
    {
        Camera.main.GetComponent<randomSpawner>().decoyKills++;
        Camera.main.GetComponent<randomSpawner>().bombKills++;
        Camera.main.GetComponent<randomSpawner>().enemiesRemaining--;
        GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + Camera.main.GetComponent<randomSpawner>().enemiesRemaining;
    }
}
