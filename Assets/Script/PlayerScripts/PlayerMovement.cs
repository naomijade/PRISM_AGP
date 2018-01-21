using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{

    public bool isOctoRoom = false;
    public bool canShake;
    public bool inChaseBar;
    public KeyCode MoveUp;
    public KeyCode MoveDown;
    public KeyCode MoveRight;
    public KeyCode MoveLeft;
    public KeyCode Replay;
    public KeyCode deleteNPC;

    public Image bombImage;
    public Image bombKey;
    public Text bombText;


    public Image decoyImage;
    public Image decoyKey;
    public Text decoyText;

    public RPGTalk rpgTalk;
    public RPGTalk rpgTalkToFollow;
    public string touching;
    public bool controls;
    public GameObject townButton;
    public GameObject eatButton;
    GameObject camera;
    public bool hasAimItem;
    public bool hasDecoyItem;
    public bool hasBombItem;
    public GameObject currNPC;

    public bool isTalking = false;

    public Vector2 target;
    public Vector2 myPos;
    public Vector2 direction;

    public bool canBeHit;

    public AudioSource playerHurt;


    public GameObject playerLight;

    //cannot be changed from inside editor
    public float characterSpeed = 1f;
    public float actualMoveSpeed = 100;

    public GameObject bullet;
    public int playerHP = 3;
    public int maxPlayerHP = 3;
    //public int bulletHearts = 1;
    //public int maxBulletHearts = 1;

    bool colorIsChanging;

    public int roomsTraversed = 0;

    public GameObject gun;

    public AudioSource playerDeath;
    public AudioSource itemPickup;
    public AudioSource healthPickup;

    public GameObject mainCam;
    public GameObject mainCamGuider;
    Animator animator;
    const int prismIdle = 0;
    const int prismUp = 1;
    const int prismRight = 2;
    const int prismLeft = 3;
    const int prismDeath = 4;
    public int roomsGoneBack = 0;
    int currentState = prismIdle;
    public GameObject[] greenBullets;
    Vector3 targetDoor = new Vector3(0, 0, 0);
    public bool inTown = true;
    bool canTurnDownMusic = true;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        updateHPUI();
        colorIsChanging = false;
        camera = camera = GameObject.FindGameObjectWithTag("MainCamera");
        hasAimItem = false;
        hasDecoyItem = false;
        canShake = true;
        canBeHit = true;
        inChaseBar = false;
        roomsTraversed = 0;

        //create line renderer between player and mouse
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>() as LineRenderer;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startWidth = 0.005f;
        lineRenderer.positionCount = 0;

        animator = this.GetComponent<Animator>();
        offset = new Vector3(transform.position.x , transform.position.y, transform.position.z - 10); //+8.5 and +2
    }


    void LateUpdate()
    {
       //print(bulletHearts);
        if (inTown == true)
        {

            //Vector3 camPos = transform.position;
           // camPos.z += offset.z;
           // mainCam.transform.position = camPos;
           
            GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "";
            GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "";

            if (touching == "NPC(Clone)")
            {
           
                if (Input.GetKeyDown("space") && isTalking == false)
                {
                    townButton.SetActive(false);
                    eatButton.SetActive(false);
                    isTalking = true;
                    rpgTalk.lineToStart = currNPC.GetComponent<NPCBehavior>().dialogueSection;
                    rpgTalk.lineToBreak = currNPC.GetComponent<NPCBehavior>().dialogueSection;
                    rpgTalk.NewTalk();
                    controls = false;
                    rpgTalk.callbackFunction = "GiveBackControls";

                }
            }


        }
    }

    private void FixedUpdate()
    {
        if (controls == true)
        {
            //if (!Input.GetKey(MoveUp) && !Input.GetKey(MoveLeft) && !Input.GetKey(MoveRight) && !Input.GetKey(MoveDown) && playerHP > 0)
            //{
            //    GetComponent<Rigidbody2D>().velocity = characterSpeed * Time.deltaTime * 100 * (GetComponent<Rigidbody2D>().velocity.normalized);
            //}
            //up
            actualMoveSpeed = 95;
            if ((Input.GetKey(MoveRight) || Input.GetKey(MoveLeft)) && (Input.GetKey(MoveUp) || Input.GetKey(MoveDown)))
            {
                actualMoveSpeed = 70;
            }
            if (Input.GetKey(MoveUp) && playerHP > 0)
            {
               //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
               //gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
               // gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 60f) * characterSpeed * Time.deltaTime * 100);
                gameObject.transform.Translate(new Vector2(0, .03f) * characterSpeed * Time.deltaTime * actualMoveSpeed);
                //gameObject.GetComponent<Animator>().SetBool("isWalking", true);
                changeState(prismUp);
            }

            //else
            //{
            //    gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            //}

            //down
            if (Input.GetKey(MoveDown) && playerHP > 0)
            {
                //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                // gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -60f) * characterSpeed * Time.deltaTime * 100);
                gameObject.transform.Translate(new Vector2(0, -.03f) * characterSpeed * Time.deltaTime * actualMoveSpeed);
                //gameObject.GetComponent<Animator>().SetBool("isWalking", true);
                changeState(prismIdle);
            }

            //else
            //{
            //    gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            //}

            //right
            if (Input.GetKey(MoveRight) && playerHP > 0)
            {
                // gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                // gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(60f, 0f) * characterSpeed * Time.deltaTime * 100);
                // checkSpeed();
                gameObject.transform.Translate(new Vector2(.03f, 0) * characterSpeed * Time.deltaTime * actualMoveSpeed);
                changeState(prismRight);
                //gameObject.GetComponent<Animator>().SetBool("isWalking", true);
                //gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            //else
            //{
            //    gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            //}

            //left
            if (Input.GetKey(MoveLeft) && playerHP > 0)
            {
                //gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-60f, 0f) * characterSpeed * Time.deltaTime * 100);
                //checkSpeed();
                gameObject.transform.Translate(new Vector2(-.03f, 0) * characterSpeed * Time.deltaTime * actualMoveSpeed);
                changeState(prismLeft);
                
                //gameObject.GetComponent<Animator>().SetBool("isWalking", true);
                //gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        //checkSpeed();
        //set position of linerenderer from player to mouse

    }

    public void OnTriggerExit2D(Collider2D col)
    {
        GetComponent<fireBullet>().notInAOE = true;

        if (col.gameObject.tag == "Town")
        {
            inTown = false;
            GetComponent<fireBullet>().canShoot = true;
            GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + mainCam.GetComponent<randomSpawner>().totalNumOfEnemies;
            GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + playerHP;
            updateHPUI();
        }

        if (col.gameObject.tag == "NPCHolder")
        {
            GetComponent<fireBullet>().canShoot = true;
            GameObject.Find("pressSpace").GetComponent<Text>().enabled = false;
            touching = "";
        }

        if (col.gameObject.tag == "postIt")
        {
            touching = "";
            GameObject.Find("pressLeftClick").GetComponent<Text>().enabled = false;
            col.gameObject.GetComponent<NoteBehavior>().turnOff();
        }
        //if (col.gameObject.tag == "Gravity")
        //{
       //fixes player movement when they leave the gravity pull

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
       // }
    }
 

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "NPCHolder")
        {
            touching = col.name;
        }

        if (col.gameObject.tag == "postIt")
        {
            touching = col.name;
            GameObject.Find("pressLeftClick").GetComponent<Text>().enabled = true;
        }

        if (col.gameObject.tag == "AOE")
        {
            GetComponent<fireBullet>().notInAOE = false;

        }

        if (col.gameObject.tag == "ChaseBar")
        {
            inChaseBar = true;
        }

        if (col.gameObject.tag == "Town")
        {
            inTown = true;
            GetComponent<fireBullet>().canShoot = false;
        }

       else if (col.gameObject.tag == "Door")
        {
            mainCamGuider.GetComponent<Transform>().Translate(19, 0, 0);
            roomsTraversed++;
            targetDoor.y = mainCamGuider.transform.position.y;
            targetDoor.x += 4;
            StartCoroutine(Transition(true));
            GetComponent<fireBullet>().notInAOE = true;
            for (int i = 0; i < mainCam.GetComponent<randomSpawner>().allDoors.Count; i++)
            {
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<Collider2D>().enabled = false;
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<SpriteRenderer>().enabled = false;
            }

            mainCam.GetComponent<randomSpawner>().enemySpawnXMin = mainCamGuider.transform.position.x - 5;
            mainCam.GetComponent<randomSpawner>().enemySpawnXMax = mainCamGuider.transform.position.x + 8;
            //if (col.gameObject.name.Contains("octo"))
            //{
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y - 3;
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y + 3;
            //}
            if (roomsGoneBack > 0)
            {
                roomsGoneBack--;
            }
            mainCam.GetComponent<randomSpawner>().enemiesRemaining = mainCam.GetComponent<randomSpawner>().enemiesToSpawn;
            GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + mainCam.GetComponent<randomSpawner>().totalNumOfEnemies;
            targetDoor.x = mainCamGuider.transform.position.x - 7;
            isOctoRoom = false;

        }
        if (col.gameObject.tag == "DoorUp" && col.gameObject.name.Contains("back"))
        {
            mainCamGuider.GetComponent<Transform>().Translate(0, 11f, 0);
            roomsTraversed--;
            targetDoor.x = mainCamGuider.transform.position.x;
            targetDoor.y -= 4;
            StartCoroutine(Transition(true));
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y + 4;
            //if (col.gameObject.name.Contains("octo"))
            //{
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y;
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y + 2.5f;
            //}
            GetComponent<fireBullet>().notInAOE = true;
            for (int i = 0; i < mainCam.GetComponent<randomSpawner>().allDoors.Count; i++)
            {
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<Collider2D>().enabled = false;
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            if (roomsGoneBack > 0)
            {
                roomsGoneBack--;
            }

            mainCam.GetComponent<randomSpawner>().enemiesRemaining = 0;
            roomsGoneBack++;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin -= 2;
            GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + 0;
            targetDoor.y = mainCamGuider.transform.position.y - 3;
            isOctoRoom = false;

        }
        else if (col.gameObject.tag == "DoorUp")
        {
            mainCamGuider.GetComponent<Transform>().Translate(0, 11f, 0);
            roomsTraversed++;
            targetDoor.x = mainCamGuider.transform.position.x;
            targetDoor.y -= 4;
            StartCoroutine(Transition(true));
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y + 4;
            //if (col.gameObject.name.Contains("octo"))
            //{
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y;
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y + 2.5f;
            //}
            GetComponent<fireBullet>().notInAOE = true;
            for (int i = 0; i < mainCam.GetComponent<randomSpawner>().allDoors.Count; i++)
            {
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<Collider2D>().enabled = false;
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            if (roomsGoneBack > 0)
            {
                roomsGoneBack--;
            }

            mainCam.GetComponent<randomSpawner>().enemiesRemaining = mainCam.GetComponent<randomSpawner>().enemiesToSpawn;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin -= 2;
            GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + mainCam.GetComponent<randomSpawner>().totalNumOfEnemies;
            targetDoor.y = mainCamGuider.transform.position.y - 3;
            isOctoRoom = false;

        }

        if (col.gameObject.tag == "DoorLeft")
        {
            mainCamGuider.GetComponent<Transform>().Translate(-19, 0, 0);
            roomsTraversed--;
            targetDoor.y = mainCamGuider.transform.position.y;
            targetDoor.x -= 4;
            StartCoroutine(Transition(false));
            GetComponent<fireBullet>().notInAOE = true;
            for (int i = 0; i < mainCam.GetComponent<randomSpawner>().allDoors.Count; i++)
            {
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<Collider2D>().enabled = false;
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<SpriteRenderer>().enabled = false;
            }

            mainCam.GetComponent<randomSpawner>().enemySpawnXMin = mainCamGuider.transform.position.x - 8;
            mainCam.GetComponent<randomSpawner>().enemySpawnXMax = mainCamGuider.transform.position.x + 5;
            //if (col.gameObject.name.Contains("octo"))
            //{
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y - 3;
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y + 3;
            //}
            mainCam.GetComponent<randomSpawner>().enemiesRemaining = 0;
            roomsGoneBack++;
            GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + 0;
            targetDoor.x = mainCamGuider.transform.position.x + 7;
            isOctoRoom = false;

        }

        if (col.gameObject.tag == "DoorDown" && col.gameObject.name.Contains("back"))
        {
            mainCamGuider.GetComponent<Transform>().Translate(0, -11f, 0);
            targetDoor.x = mainCamGuider.transform.position.x;
            targetDoor.y += 4;
            roomsTraversed--;
            StartCoroutine(Transition(true));
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y - 4;

            //if (col.gameObject.name.Contains("octo"))
            //{
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y + 2.5f;
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y;
            //}
            GetComponent<fireBullet>().notInAOE = true;
            for (int i = 0; i < mainCam.GetComponent<randomSpawner>().allDoors.Count; i++)
            {
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<Collider2D>().enabled = false;
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            mainCam.GetComponent<randomSpawner>().enemiesRemaining = 0;
            roomsGoneBack++;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin += 2;
            GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + 0;
            targetDoor.y = mainCamGuider.transform.position.y + 3;
            isOctoRoom = false;

        }
       else if (col.gameObject.tag == "DoorDown")
        {
            mainCamGuider.GetComponent<Transform>().Translate(0, -11f, 0);
            roomsTraversed++;
            targetDoor.x = mainCamGuider.transform.position.x;
            targetDoor.y += 4;
            StartCoroutine(Transition(true));
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y - 4;

            //if (col.gameObject.name.Contains("octo"))
            //{
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y + 2.5f;
            //    mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y;
            //}
            GetComponent<fireBullet>().notInAOE = true;
            for (int i = 0; i < mainCam.GetComponent<randomSpawner>().allDoors.Count; i++)
            {
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<Collider2D>().enabled = false;
                mainCam.GetComponent<randomSpawner>().allDoors[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            if (roomsGoneBack > 0)
            {
                roomsGoneBack--;
            }

            mainCam.GetComponent<randomSpawner>().enemiesRemaining = mainCam.GetComponent<randomSpawner>().enemiesToSpawn;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin += 2;
            GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + mainCam.GetComponent<randomSpawner>().totalNumOfEnemies;
            targetDoor.y = mainCamGuider.transform.position.y + 3;
            isOctoRoom = false;

        }

            }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            GameObject.Find("pressSpace").GetComponent<Text>().enabled = false;
        }
        if (Input.GetMouseButton(0))
        {
            GameObject.Find("pressLeftClick").GetComponent<Text>().enabled = false;
        }
        //if (mainCam.GetComponent<randomSpawner>().killStreak > -1)
        //{
        //    if(colorIsChanging == false)
        //    {
        //        StartCoroutine(killStreakColorShift());
        //    }


        //}

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (!hasAimItem && GetComponent<fireBullet>().canShoot == true && inTown != true)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, myPos);
            lineRenderer.SetPosition(1, target);
        }

        if (hasAimItem)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);


            RaycastHit2D hit = Physics2D.Raycast(transform.position, target - (Vector2)transform.position, 20, 1 << LayerMask.NameToLayer("Default"));

            RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, target - (Vector2)transform.position, 20, 1 << LayerMask.NameToLayer("Enemy"), 2 << LayerMask.NameToLayer("RedEnemy"));

            //Debug.DrawRay(transform.position, target - (Vector2)transform.position, Color.green);

            if (hitEnemy)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hitEnemy.point);

            }
            else if (hit)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
                Vector2 dir = Vector2.Reflect((hit.point - (Vector2)transform.position), hit.normal);
                RaycastHit2D hitTwo = Physics2D.Raycast(hit.point + dir * .01f, dir, 20, 1 << LayerMask.NameToLayer("Default"));
                RaycastHit2D hitEnemyTwo = Physics2D.Raycast(hit.point + dir * .01f, dir, 20, 1 << LayerMask.NameToLayer("Enemy"), 2 << LayerMask.NameToLayer("RedEnemy"));
                Debug.DrawRay(hit.point + dir * .01f, dir, Color.green);
                if (hitEnemyTwo)
                {
                    lineRenderer.positionCount = 3;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                    lineRenderer.SetPosition(2, hitEnemyTwo.point);

                }
                else if (hitTwo.collider != null)
                {
                    lineRenderer.positionCount = 3;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                    lineRenderer.SetPosition(2, hitTwo.point);
                }
                else
                {
                    lineRenderer.positionCount = 2;
                }
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }

        if (GetComponent<fireBullet>().notInAOE == false)
        {
            lineRenderer.enabled = false;
        }
        else
        {
            lineRenderer.enabled = true;
        }
        if (GetComponent<fireBullet>().canShoot == false)
        {
            lineRenderer.positionCount = 0;
        }


        if (playerHP > maxPlayerHP)
        {
            playerHP = maxPlayerHP;
            GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + playerHP;
        }

        if (GetComponent<fireBullet>().notInAOE == false)
        {
            characterSpeed = 1f;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
        }
        else
        {
            if (colorIsChanging == false)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            }
            characterSpeed = 1.5f;

        }
        greenBullets = GameObject.FindGameObjectsWithTag("Bullet");

        target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        myPos = new Vector2(transform.position.x, transform.position.y);
        direction = target - myPos;
        direction.Normalize();

        if (playerHP <= 0)
        {
            characterSpeed = 0;
            GetComponent<fireBullet>().canShoot = false;
            StartCoroutine(ReloadScene());
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, -8);
        }

        //character movement

        if (controls == true)
        {

            //up
            //if (Input.GetKey(MoveUp) && playerHP > 0)
            //{
            //    gameObject.transform.Translate(new Vector2(0, 0.03f) * characterSpeed * Time.deltaTime * 100);
            //    //gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            //    changeState(prismUp);
            //}

            ////else
            ////{
            ////    gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            ////}

            ////down
            //if (Input.GetKey(MoveDown) && playerHP > 0)
            //{
            //    gameObject.transform.Translate(new Vector2(0, -0.03f) * characterSpeed * Time.deltaTime * 100);
            //    //gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            //    changeState(prismIdle);
            //}

            ////else
            ////{
            ////    gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            ////}

            ////right
            //if (Input.GetKey(MoveRight) && playerHP > 0)
            //{
            //    gameObject.transform.Translate(new Vector2(0.03f, 0f) * characterSpeed * Time.deltaTime * 100);
            //    changeState(prismRight);
            //    //gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            //    //gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //}

            ////else
            ////{
            ////    gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            ////}

            ////left
            //if (Input.GetKey(MoveLeft) && playerHP > 0)
            //{
            //    gameObject.transform.Translate(new Vector2(-0.03f, 0f) * characterSpeed * Time.deltaTime * 100);
            //    changeState(prismLeft);
            //    //gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            //    //gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //}

            //else
            //{
            //    gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            //}

            if (Input.GetKey(Replay))
            {
                Application.LoadLevel("PRISM");
            }
            if (Input.GetKey(Replay) && Input.GetKey(deleteNPC))
            {
                PlayerPrefs.DeleteKey("NPCsTalkedTo");
                Application.LoadLevel("PRISM");
            }
            if (camera.GetComponent<randomSpawner>().enemiesRemaining == 0 && inTown == false)
            {
                if (Input.GetKeyDown("space"))
                {
                    if (touching == "NPC(Clone)")
                    {

                        townButton.SetActive(false);
                        eatButton.SetActive(false);
                        controls = false;
                        rpgTalk.lineToStart = 1;
                        rpgTalk.lineToBreak = 2;
                        rpgTalk.NewTalk();
                        rpgTalk.shouldStayOnScreen = true;
                        rpgTalk.callbackFunction = "NPCChoice";

                    }
                }

            }

            if (camera.GetComponent<randomSpawner>().enemiesRemaining > 0)
            {
                if (touching == "NPC(Clone)")
                {
                    rpgTalkToFollow.lineToStart = 1;
                    rpgTalkToFollow.lineToBreak = 1;
                    rpgTalkToFollow.NewTalk();
                }


                if (touching == "")
                {
                    rpgTalkToFollow.EndTalk();
                }

            }
            if (camera.GetComponent<randomSpawner>().enemiesRemaining <= 0)

            {
                rpgTalkToFollow.EndTalk();
            }
        }


    }

    
    public void NPCChoice()
    {
        //if (hasAimItem && hasBombItem && hasDecoyItem)
        //{
        //    townButton.SetActive(false);
        //}
        //else
        //{
            townButton.SetActive(true);
        //}
        //if (maxPlayerHP < 9)
        //{
            eatButton.SetActive(true);
        //}

    }
    public void GiveBackControls()
    {
        rpgTalk.shouldStayOnScreen = false;
        controls = true;
        StartCoroutine(holUpAHotSecond());
        rpgTalk.EndTalk();

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "HealthPoint")
        {

            if (playerHP <= maxPlayerHP - 1)
            {
                healthPickup.Play();
                playerHP++;
                GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + playerHP;
                updateHPUI();
                Destroy(col.gameObject);
                //playerHurt.Play();
            }
            if (playerHP >= maxPlayerHP)
            {
                GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + playerHP;
                updateHPUI();
            }
            if (playerHP <= 0)
            {
                GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + 0;
                updateHPUI();
                //playerHurt.Play();
            }
        }
        if (col.gameObject.name.Contains("Octo"))
        {
            mainCam.GetComponent<randomSpawner>().enemySpawnYMin = mainCamGuider.transform.position.y - 3;
            mainCam.GetComponent<randomSpawner>().enemySpawnYMax = mainCamGuider.transform.position.y + 3;
            isOctoRoom = true;
        }

        if (col.gameObject.tag == "RedEnemy" || col.gameObject.tag == "BlueEnemy" || col.gameObject.tag == "YellowEnemy" || col.gameObject.tag == "StaticEnemy" || col.gameObject.tag == "GreenEnemy" || col.gameObject.tag == "Boss" || col.gameObject.tag == "SDEnemyRed")
        {
            if (canBeHit == true)
            {
                playerHP--;
                if (playerHP >= 1)
                {
                    GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + playerHP;
                    updateHPUI();
                    playerHurt.Play();
                    StartCoroutine(iFrames());
                    StartCoroutine(damageFeedback());
                    StartCoroutine(hitBounce(col.gameObject));
                }

                else if (playerHP <= 0)
                {
                    {

                        playerDeath.Play();
                        //mainCam.GetComponent<MetricManager>().writeMetrics();
                        changeState(prismDeath);
                        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + 0;
                        updateHPUI();
                    }
                }
                //if (col.gameObject.tag == "RedEnemy")
                //{
                //    mainCam.GetComponent<MetricManager>().AddToMetric1(1);
                //}
                //if (col.gameObject.tag == "BlueEnemy")
                //{
                //    mainCam.GetComponent<MetricManager>().AddToMetric2(1);
                //}
                //if (col.gameObject.tag == "GreenEnemy")
                //{
                //    mainCam.GetComponent<MetricManager>().AddToMetric3(1);
                //}
                //if (col.gameObject.tag == "YellowEnemy")
                //{
                //    mainCam.GetComponent<MetricManager>().AddToMetric4(1);
                //}
                //if (col.gameObject.tag == "StaticEnemy")
                //{
                //    mainCam.GetComponent<MetricManager>().AddToMetric5(1);
                //}
                //if (col.gameObject.tag == "Enemybullet")
                //{
                //    mainCam.GetComponent<MetricManager>().AddToMetric6(1);
                //}
            }

            
        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Contains("Detector"))
        {
            
            mainCam.GetComponent<randomSpawner>().doorsActive = true;
        }



        if (col.gameObject.tag == "NPCHolder")
        {
            currNPC = col.gameObject;
            if (mainCam.GetComponent<randomSpawner>().enemiesRemaining <= 0)
            {
                GameObject.Find("pressSpace").GetComponent<Text>().enabled = true;
            }
            if (mainCam.GetComponent<randomSpawner>().enemiesRemaining == 0)
            {
                GetComponent<fireBullet>().canShoot = false;
            }
        }


        if (col.gameObject.tag == "Enemybullet" || col.gameObject.tag == "bossBullet" || col.gameObject.name.Contains("Orb") || col.gameObject.name.Contains("wallRunner"))
        {
            if (!col.gameObject.name.Contains("Orb") && !col.gameObject.name.Contains("wallRunner"))
            {
                Destroy(col.gameObject);
            }
            if (canBeHit == true)
            {
                playerHP--;
                updateHPUI();
                if (playerHP >= 1)
                {
                    GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP = " + playerHP;
                    updateHPUI();
                    playerHurt.Play();
                    StartCoroutine(iFrames());
                    StartCoroutine(damageFeedback());
                    StartCoroutine(hitBounce(col.gameObject));
                }
                else if (playerHP == 0)
                {
                    playerDeath.Play();
                    //mainCam.GetComponent<MetricManager>().writeMetrics();
                    changeState(prismDeath);
                    GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP = " + 0;
                   updateHPUI();
                    //GameObject fire = (GameObject)Instantiate(Resources.Load("Fire1"));
                    //fire.transform.position = GetComponent<Transform>().position;

                }
            }
            StartCoroutine(hitBounce(col.gameObject));

        }
        if (col.gameObject.tag == "AimItem")
        {
            itemPickup.Play();
            hasAimItem = true;
            Destroy(col.gameObject);

        }
        if (col.gameObject.tag == "DecoyItem")
        {
            itemPickup.Play();
            decoyImage.enabled = true;
            decoyKey.enabled = true;
            decoyKey.color = new Color32(0, 200, 0, 255);
            decoyText.text = "E";
            hasDecoyItem = true;
            Destroy(col.gameObject);

        }
        if (col.gameObject.tag == "bombItem")
        {
            itemPickup.Play();
            bombImage.enabled = true;
            bombKey.enabled = true;
            bombKey.color = new Color32(0, 200, 0,255);
            bombText.text = "Q";
            hasBombItem = true;
            Destroy(col.gameObject);

        }
    }

    //if the player dies
    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("PRISM");
    }

    //used to set the transition, public value -- change in inspector
    public float transitionDuration = 1f;

    //where it will move to
    public Transform targetPos;

    //used when moving between doors and then spawns enemies
    IEnumerator Transition(bool yes)
    {
        mainCam.GetComponent<randomSpawner>().doorsActive = false;
        //the transition time
        float t = 0.0f;
        bullet.GetComponent<hit>().destroyBulletsInRoom();

        Vector3 startingPos = mainCam.transform.position;
        Vector3 playerPos = transform.position;
        
        canShake = false;
        mainCam.GetComponent<randomSpawner>().StopShaking();

        mainCam.GetComponent<randomSpawner>().decoyDroped = false;


        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            //move the main camera to the center of the rooms
            mainCam.transform.position = Vector3.Lerp(startingPos, targetPos.position, t);
            //move the players infront of the door
            transform.position = Vector3.Lerp(playerPos, targetDoor, t - .2f);

            yield return 0;
        }

        canShake = true;
        //spawn enemies once player is in the new room
        int numToSpawn = 0;
        if (yes)
        {
            if (roomsTraversed < 2)
            {
                numToSpawn = 2;
            }
            else if (roomsTraversed < 4)
            {
                numToSpawn = Random.Range(2, 5);
            }
            else if (roomsTraversed < 6)
            {
                numToSpawn = Random.Range(3, 6);
            }
            else
            {
                numToSpawn = Random.Range(4, 8);
            }
        }
            if (roomsGoneBack > 0)
            {
            numToSpawn = 0;
                mainCam.GetComponent<randomSpawner>().enemiesToSpawn = 0;
            }
        mainCam.GetComponent<randomSpawner>().spawnEnemies(numToSpawn);
        Destroy(currNPC);
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

    public void takenDamage()
    {
        StartCoroutine(iFrames());
        StartCoroutine(damageFeedback());
    }

    IEnumerator damageFeedback()
    {
        colorIsChanging = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSecondsRealtime(.15f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        yield return new WaitForSecondsRealtime(.15f);
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSecondsRealtime(.15f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        yield return new WaitForSecondsRealtime(.15f);
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSecondsRealtime(.15f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        yield return new WaitForSecondsRealtime(.15f);
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        yield return new WaitForSecondsRealtime(.15f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        colorIsChanging = false;
    }

    //IEnumerator killStreakColorShift()
    //{
    //    colorIsChanging = true;
    //    GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    //    yield return new WaitForSeconds(.5f);
    //    GetComponent<SpriteRenderer>().color = new Color32(255, 127, 80, 255);
    //    yield return new WaitForSeconds(.5f);
    //    GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
    //    yield return new WaitForSeconds(.5f);
    //    GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
    //    yield return new WaitForSeconds(.5f);
    //    GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
    //    yield return new WaitForSeconds(.5f);
    //    GetComponent<SpriteRenderer>().color = new Color32(255, 0, 255,255);
    //    yield return new WaitForSeconds(.5f);
    //    GetComponent<SpriteRenderer>().color = new Color32(148, 0, 211, 255);
    //    yield return new WaitForSeconds(.5f);
    //    colorIsChanging = false;
    //}

    //animations controller
    void changeState(int state)
    {

        if (currentState == state)
            return;

        switch (state)
        {

            case prismIdle:
                animator.SetInteger("PRISMstate", prismIdle);
                break;

            case prismLeft:
                animator.SetInteger("PRISMstate", prismLeft);
                break;

            case prismRight:
                animator.SetInteger("PRISMstate", prismRight);
                break;

            case prismUp:
                animator.SetInteger("PRISMstate", prismUp);
                break;


            case prismDeath:
                animator.SetInteger("PRISMstate", prismDeath);
                break;



        }

        currentState = state;
    }

    public void updateHPUI()
    {
        string tempHP = "";
        for (int i = 1; i <= playerHP; i++)
        {
            tempHP = "HP" + i;
            GameObject.Find(tempHP).GetComponent<SpriteRenderer>().enabled = true;
            tempHP = "EmptyHP" + i;
            GameObject.Find(tempHP).GetComponent<SpriteRenderer>().enabled = false;
        }
        for (int i = maxPlayerHP; i > playerHP; i--)
        {
            tempHP = "HP" + i;
            GameObject.Find(tempHP).GetComponent<SpriteRenderer>().enabled = false;
            tempHP = "EmptyHP" + i;
            GameObject.Find(tempHP).GetComponent<SpriteRenderer>().enabled = true;
        }
        for (int i = 9; i > maxPlayerHP; i--)
        {
            tempHP = "HP" + i;
            GameObject.Find(tempHP).GetComponent<SpriteRenderer>().enabled = false;
            tempHP = "EmptyHP" + i;
            GameObject.Find(tempHP).GetComponent<SpriteRenderer>().enabled = false;
        }
        if (inTown)
        {
            for (int i = 1; i <= playerHP; i++)
            {
                tempHP = "HP" + i;
                GameObject.Find(tempHP).GetComponent<SpriteRenderer>().enabled = false;
                tempHP = "EmptyHP" + i;
                GameObject.Find(tempHP).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    IEnumerator holUpAHotSecond()
    {
        yield return new WaitForEndOfFrame();
        isTalking = false;
    }

}
