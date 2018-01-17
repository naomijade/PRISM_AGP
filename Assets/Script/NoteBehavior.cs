using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public Texture2D cursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public GameObject note;
    public GameObject note1;
    GameObject camera;
    string touching;
    // Use this for initialization
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.GetComponent<randomSpawner>().enemiesRemaining == 0)
        {
            if (touching == "Player")
            {

                if (Input.GetButtonDown("Fire1"))
                {

                    note.SetActive(true);

                }
            }
        }
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        touching = col.name;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        touching = "";
    }

    public void turnOff()
    {
        note.SetActive(false);
        note1.SetActive(false);
    }

    }

