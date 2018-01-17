using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRunnerYellowEnemy : MonoBehaviour {


    GameObject camera;

    bool onRightWall = false;
    bool onTopWall = false;
    bool onBottomWall = false;
    bool onLeftWall = false;
    public float speed = 2;
    float originalSpeed = 2;

    bool doOncePerWall = false;
    GameObject lastColWall;
    GameObject twoWallsAgo;
    Vector3 directionToMove;
    Vector3 wallToMoveTo;


    // Use this for initialization
    void Start() {
        originalSpeed = speed;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        int x = Random.Range(0, 2);
        if (x == 0)
        {
            speed = -speed;
        }
        List<Transform> walls = new List<Transform>();
        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObj.name.Contains("Wall") && !gameObj.name.Contains("obs"))
            {
                walls.Add(gameObj.transform);
            }
        }
        print(walls.Count);
        Transform closeWall = GetClosestWall(walls).GetComponent<Transform>().transform;
        lastColWall = closeWall.gameObject;
        // gameObject.GetComponent<Transform>().position = closeWall.position;
        wallToMoveTo = closeWall.position;
        gameObject.GetComponent<Transform>().rotation = closeWall.rotation;

        if (closeWall.gameObject.name.Contains("SideWall"))
        {
            if (closeWall.position.x > camera.GetComponent<Transform>().position.x)
            {
                gameObject.GetComponent<Transform>().position -= new Vector3(1f, 0, 0);
                onRightWall = true;
            }
            else if (closeWall.position.x < camera.GetComponent<Transform>().position.x)
            {
                gameObject.GetComponent<Transform>().position += new Vector3(1f, 0, 0);
                onLeftWall = true;
            }
        }
        else if (closeWall.gameObject.name.Contains("TopWall"))
        {
            if (gameObject.GetComponent<Transform>().position.y > camera.GetComponent<Transform>().position.y)
            {
                gameObject.GetComponent<Transform>().position -= new Vector3(0, 1f, 0);
                onTopWall = true;
            }
            else if (gameObject.GetComponent<Transform>().position.y < camera.GetComponent<Transform>().position.y)
            {
                gameObject.GetComponent<Transform>().position += new Vector3(0, 1f, 0);
                onBottomWall = true;
            }
        }
        StartCoroutine(delay());
    }

        // Update is called once per frame
        void Update () {

        if (lastColWall != null)
        {
            print(lastColWall.GetComponent<Transform>().rotation.z);
            float angle = Mathf.Lerp(gameObject.GetComponent<Transform>().rotation.eulerAngles.z, lastColWall.GetComponent<Transform>().rotation.eulerAngles.z, .05f);
            //transform.eulerAngles = new Vector3(0, 0, angle);
            angle = Mathf.Abs(angle);
            //angle = -angle;
            gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, angle);
           // directionToMove = lastColWall

            //Quaternion rot = Quaternion.Lerp(gameObject.GetComponent<Transform>().rotation, lastColWall.GetComponent<Transform>().rotation, .2f);
            //gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, gameObject.GetComponent<Transform>().rotation.y, gameObject.GetComponent<Transform>().rotation.z);
            //gameObject.GetComponent<Transform>().rotation += Quaternion.Euler(rot.z, 0, 0);

            //flaot rot = Mathf.Lerp()

        }
        if (onTopWall)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            //transform.position += Vector3.right * Time.deltaTime;
        }
        else if (onRightWall)
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            //transform.position += Vector3.right * Time.deltaTime;
        }
        else if (onBottomWall)
        {
            transform.position += new Vector3(-speed,0, 0) * Time.deltaTime;
           // transform.position += Vector3.right * Time.deltaTime;
        }
        else if (onLeftWall)
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
            //transform.position += Vector3.right * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, wallToMoveTo, Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Wall") && !col.gameObject.name.Contains("obs"))
        {
            //if(lastColWall != col.gameObject)
            //{
            if (twoWallsAgo != col.gameObject)
            {
                checkWhichWall(col.gameObject);
                twoWallsAgo = lastColWall;
                lastColWall = col.gameObject;
            }
            //    doOncePerWall = false;
            //}
            //if (doOncePerWall == false)
            //{
               // gameObject.GetComponent<Transform>().rotation = col.gameObject.GetComponent<Transform>().rotation;
                //doOncePerWall = true;
            //}
        }
        if (col.gameObject.name.Contains("Player"))
        {
            StartCoroutine(postHitTimer());
        }
    }

    Transform GetClosestWall(List<Transform> walls)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in walls)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(.001f);
        //List<Transform> walls = new List<Transform>();
        //foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        //{
        //    if (gameObj.name.Contains("Wall") && !gameObj.name.Contains("obs"))
        //    {
        //        walls.Add(gameObj.transform);
        //    }
        //}
        //print(walls.Count);
        //Transform closeWall = GetClosestWall(walls).GetComponent<Transform>().transform;
        //lastColWall = closeWall.gameObject;
        //// gameObject.GetComponent<Transform>().position = closeWall.position;
        //wallToMoveTo = closeWall.position;
        //gameObject.GetComponent<Transform>().rotation = closeWall.rotation;

        //if (closeWall.gameObject.name.Contains("SideWall"))
        //{
        //    if (closeWall.position.x > camera.GetComponent<Transform>().position.x)
        //    {
        //        gameObject.GetComponent<Transform>().position -= new Vector3(1f, 0, 0);
        //        onRightWall = true;
        //    }
        //    else if (closeWall.position.x < camera.GetComponent<Transform>().position.x)
        //    {
        //        gameObject.GetComponent<Transform>().position += new Vector3(1f, 0, 0);
        //        onLeftWall = true;
        //    }
        //}
        //else if (closeWall.gameObject.name.Contains("TopWall"))
        //{
        //    if (gameObject.GetComponent<Transform>().position.y > camera.GetComponent<Transform>().position.y)
        //    {
        //        gameObject.GetComponent<Transform>().position -= new Vector3(0, 1f, 0);
        //        onTopWall = true;
        //    }
        //    else if (gameObject.GetComponent<Transform>().position.y < camera.GetComponent<Transform>().position.y)
        //    {
        //        gameObject.GetComponent<Transform>().position += new Vector3(0, 1f, 0);
        //        onBottomWall = true;
        //    }
        //}
    }

    void checkWhichWall(GameObject wall)
    {
       // gameObject.GetComponent<Transform>().position = wall.transform.position;
        if (wall.gameObject.name.Contains("SideWall"))
        {
            if (gameObject.GetComponent<Transform>().position.x > camera.GetComponent<Transform>().position.x)
            {
                onRightWall = true;
                onLeftWall = false;
                onTopWall = false;
                onBottomWall = false;
                //gameObject.GetComponent<Transform>().position += new Vector3(wall.transform.position.x, 0, 0);
                if (wall != lastColWall)
                {
                    gameObject.GetComponent<Transform>().position += new Vector3(.5f, 0, 0);
                }
            }
            else if (gameObject.GetComponent<Transform>().position.x < camera.GetComponent<Transform>().position.x)
            {
                onLeftWall = true;
                onRightWall = false;
                onTopWall = false;
                onBottomWall = false;
                    // gameObject.GetComponent<Transform>().position += new Vector3(wall.transform.localScale.x / 2 + gameObject.GetComponent<Transform>().localScale.y / 2 + .5f, 0, 0);
                    if (wall != lastColWall)
                    {
                        gameObject.GetComponent<Transform>().position -= new Vector3(.5f, 0, 0);
                    }
            }
        }
        else if (wall.gameObject.name.Contains("TopWall"))
        {
            if (gameObject.GetComponent<Transform>().position.y > camera.GetComponent<Transform>().position.y)
            {
                onTopWall = true;
                onLeftWall = false;
                onRightWall = false;
                onBottomWall = false;
                        // gameObject.GetComponent<Transform>().position -= new Vector3(0, wall.transform.localScale.y / 2 + gameObject.GetComponent<Transform>().localScale.y / 2, 0);
                        if (wall != lastColWall)
                        {
                            gameObject.GetComponent<Transform>().position += new Vector3(0, .5f, 0);
                        }
            }
            else if (gameObject.GetComponent<Transform>().position.y < camera.GetComponent<Transform>().position.y)
            {
                onBottomWall = true;
                onLeftWall = false;
                onTopWall = false;
                onRightWall = false;
                            // gameObject.GetComponent<Transform>().position += new Vector3(0, wall.transform.localScale.y / 2 + gameObject.GetComponent<Transform>().localScale.y / 2, 0);
                            if (wall != lastColWall)
                            {
                                gameObject.GetComponent<Transform>().position -= new Vector3(0, .5f, 0);
                            }
            }
        }
    }

    IEnumerator postHitTimer()
    {
        if (speed != 0)
        {
            originalSpeed = speed;
        }
        speed = 0;
        yield return new WaitForSeconds(1);
        speed = -originalSpeed;
    }

}
