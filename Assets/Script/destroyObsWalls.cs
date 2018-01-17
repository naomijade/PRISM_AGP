using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObsWalls : MonoBehaviour
{

    int wallDamage;
    // Use this for initialization
    void Start()
    {
        wallDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (wallDamage >= 5)
        {
            Component[] hingeJoints;
            hingeJoints = GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer joint in hingeJoints)
            {
                joint.enabled = false;
            }
            GetComponent<Collider2D>().isTrigger = true;
            Destroy(gameObject, 3);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("Bullet"))
        {
            wallDamage++;
        }
    }
}
