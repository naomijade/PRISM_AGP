using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBehavior : MonoBehaviour
{

    public AudioSource eatThemUpSound;
    public AudioSource eatThemScream;


    public Texture2D cursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    GameObject camera;
    public int NPCHP;
    public GameObject Player;
    public bool hasTalkedTo;
    int RNG;
    int itemsLeftToGive;
    public bool hasItemsLeft;
    public GameObject decoyItem;
    public GameObject aimItem;
    public GameObject bombItem;
    public GameObject bloodStain;

    bool hasGottenDecoy;
    bool hasGottenBomb;
    bool hasGottenAim;
    public int dialogueSection;
    // Use this for initialization
    void Start()
    {
        dialogueSection = Random.Range(3, 40);
        itemsLeftToGive = 3;
        NPCHP = 3;
        hasItemsLeft = true;
        hasTalkedTo = false;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        RNG = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCHP <= 0)
        {
            Instantiate(bloodStain, GetComponent<Transform>().position, Quaternion.identity);
            Player.GetComponent<PlayerMovement>().GiveBackControls();
            GetComponent<Collider2D>().enabled = false;
            GameObject.Find("pressSpace").GetComponent<Text>().enabled = false;
            Player.GetComponent<PlayerMovement>().touching = "";
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);

    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void HasTalkedTo()
    {
        if (camera.GetComponent<randomSpawner>().enemiesRemaining == 0 && hasTalkedTo == false)
        {
            camera.GetComponent<randomSpawner>().NPCTalkedTo++;
            //hasTalkedTo = true;
            Player.GetComponent<fireBullet>().canShoot = true;
            Player.GetComponent<PlayerMovement>().currNPC.GetComponent<Collider2D>().enabled = false;
            Player.GetComponent<PlayerMovement>().touching = "";
            //hasTalkedTo = false;
            if (itemsLeftToGive > 0)
            {
                spawnItem(RNG);
            }
            else
            {
                Destroy(Player.GetComponent<PlayerMovement>().currNPC);
            }


        }
    }

    public void HasEaten()
    {
        if (camera.GetComponent<randomSpawner>().enemiesRemaining == 0 && hasTalkedTo == false)
        {
            eatThemScream.Play();
            if (Player.GetComponent<PlayerMovement>().maxPlayerHP < 9)
            {
                Player.GetComponent<PlayerMovement>().maxPlayerHP += 2;
            }
            for (int i = 0; i < 2; i++)
            {
                if (Player.GetComponent<PlayerMovement>().playerHP < 9)
                {
                    Player.GetComponent<PlayerMovement>().playerHP += 1;
                }
            }

            Instantiate(bloodStain, Player.GetComponent<PlayerMovement>().currNPC.transform.position, Quaternion.identity);

            //hasTalkedTo = true;
            GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "HP: " + Player.GetComponent<PlayerMovement>().playerHP;
            Player.GetComponent<PlayerMovement>().updateHPUI();
            Player.GetComponent<PlayerMovement>().currNPC.GetComponent<Collider2D>().enabled = false;
            Player.GetComponent<fireBullet>().canShoot = true;
            Player.GetComponent<PlayerMovement>().touching = "";
            //hasTalkedTo = false;
            eatThemUpSound.Play();
            Player.GetComponent<PlayerMovement>().currNPC.SetActive(false);
            Destroy(Player.GetComponent<PlayerMovement>().currNPC,3);
        }
    }

    void spawnItem(int random)
    {
        if (random == 0)
        {
            if (!Player.GetComponent<PlayerMovement>().hasDecoyItem)
            {
                Instantiate(decoyItem, Player.GetComponent<PlayerMovement>().currNPC.transform.position, Quaternion.identity);
                itemsLeftToGive--;
                RNG = Random.Range(0, 3);
            }
            else
            {
                random = Random.Range(0, 3);
                spawnItem(random);
            }
        }
        else if (random == 1)
        {
            if (!Player.GetComponent<PlayerMovement>().hasAimItem)
            {
                Instantiate(aimItem, Player.GetComponent<PlayerMovement>().currNPC.transform.position, Quaternion.identity);
                RNG = Random.Range(0, 3);
                itemsLeftToGive--;
            }
            else
            {
                random = Random.Range(0, 3);
                spawnItem(random);
            }
        }
        else if (random == 2)
        {
            if (!Player.GetComponent<PlayerMovement>().hasBombItem)
            {
                Instantiate(bombItem, Player.GetComponent<PlayerMovement>().currNPC.transform.position, Quaternion.identity);
                RNG = Random.Range(0, 3);
                itemsLeftToGive--;
            }
            else
            {
                random = Random.Range(0, 3);
                spawnItem(random);
            }
        }
        Destroy(Player.GetComponent<PlayerMovement>().currNPC);
        
    }
}
