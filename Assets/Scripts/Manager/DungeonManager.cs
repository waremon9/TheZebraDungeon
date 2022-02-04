using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DungeonManager : MySingleton<DungeonManager>
{
    public override bool DoDestroyOnLoad { get; }
    
    public int nbRooms = 10;
    [Range(3, 6)] public int emptyRoomRatio = 3;
    public GameObject room;
    public Transform roomParent;

    private Vector2Int[] direction = new Vector2Int[4];
    private Room[,] rooms;
    private List<Room> createdRoom;
    
    private void Awake()
    {
        base.Awake();
        
        direction[0] = Vector2Int.up;
        direction[1] = Vector2Int.right;
        direction[2] = Vector2Int.down;
        direction[3] = Vector2Int.left;
    }

    public void GenerateDungeon()
    {
        rooms = new Room[nbRooms * 4, nbRooms * 4];
        Vector2Int core = Vector2Int.one * nbRooms * 2;
        createdRoom = new List<Room>();

        Room firstRoom = new Room(RoomType.NORMAL, core, null);
        rooms[core.x, core.y] = firstRoom;
        createdRoom.Add(firstRoom);

        int i = 0;
        while (createdRoom.Count < nbRooms)
        {
            Room randExistingRoom;
            do
            {
                randExistingRoom = Helper.RandomFromList(createdRoom);
            } while (HasEmptyNeighbour(randExistingRoom.coord) == false);
            
            Vector2Int newRoomCoord = GetRandomEmptyNeighbourCoord(randExistingRoom.coord);

            RoomType newRoomType = RoomType.NORMAL;
            if (i % emptyRoomRatio == 0) newRoomType = RoomType.EMPTY;
            
            Room newRoom = new Room(newRoomType, newRoomCoord, randExistingRoom);
            rooms[newRoomCoord.x, newRoomCoord.y] = newRoom;
            if(newRoom.type != RoomType.EMPTY) createdRoom.Add(newRoom);
            
            i++;
        }

        foreach (Room r in createdRoom)
        {
            Instantiate(room, RoomCoordToWorldCoord(r.coord), quaternion.identity, roomParent);
        }
    }

    private bool HasEmptyNeighbour(Vector2Int coord)
    {
        foreach (Vector2Int dir in direction)
        {
            Vector2Int testCoord = coord + dir;
            if (rooms[testCoord.x, testCoord.y] == null) return true;
        }

        return false;
    }

    private Vector2Int GetRandomEmptyNeighbourCoord(Vector2Int coord)
    {
        List<Vector2Int> availableRoom = new List<Vector2Int>();
        foreach (Vector2Int dir in direction)
        {
            Vector2Int testCoord = coord + dir;
            if (rooms[testCoord.x, testCoord.y] == null) availableRoom.Add(testCoord);
        }

        return Helper.RandomFromList(availableRoom);
    }

    private Vector3 RoomCoordToWorldCoord(Vector2Int coord)
    {
        return (Vector2) (coord - Vector2Int.one * nbRooms * 2) * Helper.CalculateBounds(room).size.x * 1.4f;
    }
    
    public Vector2 GetPlayerSpawn()
    {
        return RoomCoordToWorldCoord(Helper.RandomFromList(createdRoom).coord);
    }
}
