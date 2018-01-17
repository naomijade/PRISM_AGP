using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public Tile (Vector2 position)
    {
        position = _position;
        tileSpaceUsed = false; // starts as false
    }

    public Room _roomOwner;

    public Vector2 _position;

    public Vector2 getPosition() { return _position; }
    public Room getRoom() { return _roomOwner; }

    public bool tileSpaceUsed;

    public bool[] doorsConnectedToTile;

    void Awake()
    {
        doorsConnectedToTile = new bool[4]; //0 = up, 1 is right, 2 is down, 3 is left
    }
}

