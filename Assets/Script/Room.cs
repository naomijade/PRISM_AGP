using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    
    public int _sizeRow;
    public int _sizeColl;

    public Room(int sizeRow, int sizeColl)
    {
        sizeRow = _sizeRow;
        sizeColl = _sizeColl;

    }

    public enum doorDirection
    {
        Up,
        Right,
        Down,
        Left,
    };

    public List<int> doorsInRoom;

    public List<Tile> tilesInRoom;

    public Room _room;

    void Awake()
    {

        _room = new Room(_sizeRow, _sizeColl);
    }

    public Room getRoom(int row, int coll) { return _room; }

}
