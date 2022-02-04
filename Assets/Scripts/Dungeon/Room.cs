using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public RoomType type;
    public Vector2Int coord;
    public Room parent;

    public Room(RoomType t, Vector2Int c, Room p)
    {
        type = t;
        coord = c;
        parent = p;
    }
}

public enum RoomType
{
    EMPTY,
    NORMAL
}