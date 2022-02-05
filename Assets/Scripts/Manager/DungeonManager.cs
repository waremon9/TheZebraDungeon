using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DungeonManager : MySingleton<DungeonManager>
{
    public override bool DoDestroyOnLoad { get; }
    
    [Header("Dungeon layout")]
    public int nbRooms = 10;
    [Range(3, 6)] public int emptyRoomRatio = 3;
    
    [Header("Prefab")]
    public Room room;
    public GameObject corridor;
    
    [Header("parents")]
    public Transform roomParent;
    public Transform corridorParent;

    private Vector2Int[] direction = new Vector2Int[4];
    private RoomType[,] rooms;
    private List<Room> createdRoom;
    
    protected override void Awake()
    {
        base.Awake();
        
        //store all direction possible
        direction[0] = Vector2Int.up;
        direction[1] = Vector2Int.right;
        direction[2] = Vector2Int.down;
        direction[3] = Vector2Int.left;
    }

    public void GenerateDungeon()
    {
        rooms = new RoomType[nbRooms * 4, nbRooms * 4]; //set array size to avoid overflow
        Vector2Int core = Vector2Int.one * nbRooms * 2; //start at center of array
        createdRoom = new List<Room>();

        //add first room at center
        Room firstRoom = CreateRoom(core, RoomType.NORMAL, null);
        rooms[core.x, core.y] = RoomType.NORMAL;
        createdRoom.Add(firstRoom);

        //while not enough room created, create more
        int i = 0;
        while (createdRoom.Count < nbRooms)
        {
            //get a random existing room and check it has an empty neighbour for the new room
            Room randExistingRoom;
            do
            {
                randExistingRoom = Helper.RandomFromList(createdRoom);
            } while (HasEmptyNeighbour(randExistingRoom.coord) == false);
            
            Vector2Int newRoomCoord = GetRandomEmptyNeighbourCoord(randExistingRoom.coord);

            //define room type
            RoomType newRoomType = RoomType.NORMAL;
            if (i % emptyRoomRatio == 0) newRoomType = RoomType.EMPTY;

            //empty room are no room, they exist only to add empty space in dungeon and avoid a single block layout
            if (newRoomType != RoomType.EMPTY)
            {
                //create the room in scene
                Room newRoom = CreateRoom(newRoomCoord, newRoomType, randExistingRoom);
                createdRoom.Add(newRoom);
                
                //create the corridor to parent in scene
                Vector3 corridorPosition = RoomCoordToWorldCoord((Vector2)(newRoomCoord + newRoom.parent.coord) / 2f) ;
                 GameObject temp = Instantiate(corridor, corridorPosition, quaternion.identity, corridorParent);
                 if (newRoomCoord.x == newRoom.parent.coord.x)
                 {
                     temp.transform.rotation = Quaternion.Euler(Vector3.forward * 90);
                 }
            }
            
            rooms[newRoomCoord.x, newRoomCoord.y] = newRoomType;
            
            i++;
        }
    }

    /// <summary>
    /// Instantiate a new room and init it
    /// </summary>
    /// <param name="roomCoord">room coordinate, not world</param>
    /// <param name="type">room type</param>
    /// <param name="parent">room parent</param>
    /// <returns>return the created room</returns>
    private Room CreateRoom(Vector2Int roomCoord, RoomType type, Room parent)
    {
        Room temp = Instantiate(room, RoomCoordToWorldCoord(roomCoord), quaternion.identity, roomParent);
        temp.Init(type, roomCoord, parent);
        return temp;
    }

    /// <summary>
    /// Check if the room has an empty space for a new room
    /// </summary>
    /// <param name="coord">room coordinates</param>
    /// <returns>return true if at least one empty space is found</returns>
    private bool HasEmptyNeighbour(Vector2Int coord)
    {
        foreach (Vector2Int dir in direction)
        {
            Vector2Int testCoord = coord + dir;
            if (rooms[testCoord.x, testCoord.y] == RoomType.NULL) return true;
        }

        return false;
    }

    /// <summary>
    /// get a random empty space around a existing room for a new room. Assuming there is at least 1. use "HasEmptyNeighbour" before.
    /// </summary>
    /// <param name="coord">room coordinates</param>
    /// <returns>return coordinates of a random empty space</returns>
    private Vector2Int GetRandomEmptyNeighbourCoord(Vector2Int coord)
    {
        List<Vector2Int> availableRoom = new List<Vector2Int>();
        foreach (Vector2Int dir in direction)
        {
            Vector2Int testCoord = coord + dir;
            if (rooms[testCoord.x, testCoord.y] == RoomType.NULL) availableRoom.Add(testCoord);
        }

        return Helper.RandomFromList(availableRoom);
    }

    /// <summary>
    /// Turn room coordinates into world space coordinates
    /// </summary>
    /// <param name="coord">room coordiantes</param>
    /// <returns>world space cooriantes</returns>
    private Vector3 RoomCoordToWorldCoord(Vector2 coord)
    {
        return (coord - Vector2Int.one * nbRooms * 2) * (Helper.CalculateBoundsCollider2D(room.gameObject).size.x + Helper.CalculateBoundsSpriteRenderer(corridor).size.y);
    }
    
    /// <summary>
    /// Select a random room for the player to spawn
    /// </summary>
    /// <returns>random room world coordinates</returns>
    public Vector2 GetPlayerSpawn()
    {
        return RoomCoordToWorldCoord(Helper.RandomFromList(createdRoom).coord);
    }
}