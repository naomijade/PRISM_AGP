using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should these all be on a gameobject called gamecontroller
//how do i referemce these other scrips (getcomponent?)


public class Grid : MonoBehaviour
{
    public int totalRows = 4;
    public int totalColls = 4;

    //void Awake()
    //{
    //    _grid = new Tile(totalRows, totalColls);
    //}

    void Start()
    {
        loadBoard();
    }

    public Grid(int row, int coll)
    {
        twodArray = new Tile[row][];
        unoccupiedTiles = new List<Tile>();
        //load all tiles into unoccupied
        //unoccupiedTiles = _grid;
        //clear takenTiles
        takenTiles = new List<Tile>();
        takenTiles.Clear();
    }

    //public Tile GetTile(int row, int coll) { return _grid[row][coll]; }



    public Tile[][] twodArray;             //2darray of all tiles
    private List<Tile> unoccupiedTiles;  //list of all unoccupied tiles
    private List<Tile> takenTiles;      //list of occupied tiles (opposite of the other list)
    private HashSet<Room> allRooms;     //contains all possible rooms

    private void loadBoard()
    {
        Grid _grid = new Grid(totalRows, totalColls); //grid of all the tiles
        while (_grid.unoccupiedTiles.Count > 0)
        {
            generateRoom(typeOfRoomToGenerate());
        }
    }

    private Room typeOfRoomToGenerate()
    {
        //find where to generate
        Room roomToGenerate = new Room(1,1); //RANDASDASDAsd
        var tiles = roomToGenerate.tilesInRoom;
        for (int i = 0; i < roomToGenerate.tilesInRoom.Count; i++)
        {
            if (tiles[i].doorsConnectedToTile[0] == true && tiles[i].doorsConnectedToTile[1] == false && tiles[i].doorsConnectedToTile[2] == false && tiles[i].doorsConnectedToTile[3] == false)
            {
                //spawn rooms with doors down
                
            }
            else if (tiles[i].doorsConnectedToTile[0] == false && tiles[i].doorsConnectedToTile[1] == true && tiles[i].doorsConnectedToTile[2] == false && tiles[i].doorsConnectedToTile[3] == false)
            {
                //spawn rooms with doors left
            }
            else if (tiles[i].doorsConnectedToTile[0] == false && tiles[i].doorsConnectedToTile[1] == false && tiles[i].doorsConnectedToTile[2] == true && tiles[i].doorsConnectedToTile[3] == false)
            {
                //spawn rooms with doors down and to the left
            }
            else if (tiles[i].doorsConnectedToTile[0] == false && tiles[i].doorsConnectedToTile[1] == false && tiles[i].doorsConnectedToTile[2] == false && tiles[i].doorsConnectedToTile[3] == true)
            {
                //spawn rooms with doors down and to the left
            }
        }
        return roomToGenerate;
    }
    private void generateRoom(Room roomToSpawn)
    {
        //insantiate room in the right place
        for (int i = 0; i < roomToSpawn.tilesInRoom.Count; i++)
        {         //best way to do this?
            takenTiles.Add(roomToSpawn.tilesInRoom[i]);
            unoccupiedTiles.Remove(roomToSpawn.tilesInRoom[i]);
        }
    }

    //private void loadListOfRooms()
    //{
    //    allRooms.Add
    //}

}
