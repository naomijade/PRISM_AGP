using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteEnemyOrbs : MonoBehaviour
{

    public float DistanceToOrigin;
    public float DistanceToParent;
    public float DistanceFromParentToPlayer;
    GameObject obj;
    public GameObject origin;
    Vector3 rotAxis = new Vector3(0, 0, 1);
    public bool hasBeenFired = false;
    public bool runCoroutineOnce = false;
    bool returnToOrigin = false;
    // Use this for initialization
    void Start()
    {
            obj = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name.Contains("OrbOrigin"))
        {
            transform.RotateAround(obj.GetComponent<Transform>().position, rotAxis, 75 * Time.deltaTime);
        }
        else
        {
            if(returnToOrigin == true)
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), origin.GetComponent<Transform>().position, 3 * Time.deltaTime);
            }
            if (hasBeenFired == false)
            {
                gameObject.GetComponent<Transform>().position = origin.GetComponent<Transform>().position;
                gameObject.GetComponent<Transform>().rotation = origin.GetComponent<Transform>().rotation;

            }
            //if(Distance > 10)
             //{
             //    transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), obj.GetComponent<Transform>().position, 10 * Time.deltaTime);
             //}
            if (hasBeenFired)
            {
                transform.Rotate(Vector3.forward * 400 * Time.deltaTime);
                DistanceToOrigin = Vector2.Distance(origin.transform.position, transform.position);
                DistanceToParent = Vector3.Distance(obj.transform.position, transform.position);
                DistanceFromParentToPlayer = Vector3.Distance(obj.transform.position, GameObject.FindWithTag("Player").transform.position);
                if (DistanceToParent > DistanceFromParentToPlayer)
                {
                    if (runCoroutineOnce == false)
                    {
                        StartCoroutine(afterShotReturn(1f));
                        runCoroutineOnce = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!gameObject.name.Contains("OrbOrigin"))
            {
                if (hasBeenFired == true)
                {
                    if (runCoroutineOnce == false)
                    {
                        StartCoroutine(afterShotReturn(0));
                        runCoroutineOnce = true;
                    }
                }
            }
        }
        if (col.gameObject.name.Contains("wall") || col.gameObject.name.Contains("Wall"))
        {
            if (!gameObject.name.Contains("OrbOrigin"))
            {
                if (hasBeenFired == true)
                {
                        StartCoroutine(afterShotReturn(0));
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("wall") || col.gameObject.name.Contains("Wall"))
        {
            if (!gameObject.name.Contains("OrbOrigin"))
            {
                if (hasBeenFired == true)
                {
                        StartCoroutine(afterShotReturn(0));

                }
            }
        }
    }

    IEnumerator afterShotReturn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        returnToOrigin = true;
        yield return new WaitUntil(() => DistanceToOrigin < .1f);
        returnToOrigin = false;
        hasBeenFired = false;
        runCoroutineOnce = false;
        DistanceToOrigin = 0;
        DistanceToParent = 0;
        DistanceFromParentToPlayer = 0;
    }
}
