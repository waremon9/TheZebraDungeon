using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorUp, doorRight, doorDown;
    
    [HideInInspector] public RoomType type = RoomType.NORMAL;
    [HideInInspector] public Vector2Int coord;
    [HideInInspector] public Room parent;

    /// <summary>
    /// Set type, coordinate and parent and open door to parent as well as parent door to child
    /// </summary>
    /// <param name="t">type of the room</param>
    /// <param name="c">coordinate of the room, not world</param>
    /// <param name="p">parent room</param>
    public void Init(RoomType t, Vector2Int c, Room p)
    {
        type = t;
        coord = c;
        parent = p;

        if(!parent) return;
        
        //Compare coord with parent to open door
        if (parent.coord - coord == Vector2Int.left)
        {
            doorLeft.SetActive(false);
            parent.doorRight.SetActive(false);
        }
        else if (parent.coord - coord == Vector2Int.up)
        {
            doorDown.SetActive(false);
            parent.doorUp.SetActive(false);
        }
        else if (parent.coord - coord == Vector2Int.right)
        {
            doorRight.SetActive(false);
            parent.doorLeft.SetActive(false);
        }
        else
        {
            doorUp.SetActive(false);
            parent.doorDown.SetActive(false);
        }
    }
}

public enum RoomType
{
    NULL,
    EMPTY,
    NORMAL
}