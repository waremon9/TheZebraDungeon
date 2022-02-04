using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class CameraMovement : MySingleton<CameraMovement>
{
    public override bool DoDestroyOnLoad
    {
        get { return true; }
    }

    private Rigidbody2D targetRb;
    
    private Transform roomT;

    [Header("Camera speed")]
    public float speed = 0.7f;
    private Vector2 velSpeed;

    [Header("Zoom")]
    public float defaultZoom = 8f;
    public float maxZoom = 12f;
    public float zoomSpeed = 1f;
    private float velZoom;

    void FixedUpdate () {
        if (targetRb == null) return;
		
        //Movement
        Vector2 rbVelocity = targetRb.velocity;
        float playerSpeed = rbVelocity.magnitude; 
        Vector2 posOffset = rbVelocity / 1.75f;
        Vector2 myPos = targetRb.position + posOffset;

        //Stay within room bounds
        if (roomT != null) {
            Vector2 roomPos = roomT.position;
            float offset = 10f;
            if (myPos.x > roomPos.x + offset) myPos = new Vector2(roomPos.x + offset, myPos.y);
            else if (myPos.x < roomPos.x - offset) myPos = new Vector2(roomPos.x - offset, myPos.y);
            if (myPos.y > roomPos.y + offset) myPos = new Vector2(myPos.x, roomPos.y + offset);
            else if (myPos.y < roomPos.y - offset) myPos = new Vector2(myPos.x, roomPos.y - offset);
        }

        transform.position = Vector2.SmoothDamp(transform.position, myPos, ref velSpeed, speed);

        //Zoom
        float desiredZoom = Mathf.Clamp(defaultZoom + (playerSpeed / 4), defaultZoom, maxZoom);
        
        Helper.Camera.orthographicSize = Mathf.SmoothDamp(Helper.Camera.orthographicSize, desiredZoom, ref velZoom, zoomSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void SetPlayerRigidBody(Rigidbody2D rb)
    {
        targetRb = rb;
    }

    public void SetRoom(Transform pos)
    {
        roomT = pos;
    }
}