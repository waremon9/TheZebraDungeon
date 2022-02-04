using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    
    private float horizontalInput;
    private float verticalInput;

    private Rigidbody2D rb;
    
    private void Start()
    {
        if(!TryGetComponent(out rb)) Debug.Log("no rigidbody "+name);
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("GameHorizontal");
        verticalInput = Input.GetAxisRaw("GameVertical");
    }

    private void FixedUpdate()
    {
        Move();
        AimAtCursor();
    }

    private void Move()
    {
        Vector2 newPosition = new Vector2(horizontalInput, verticalInput) * speed * Time.fixedDeltaTime + rb.position;
        rb.MovePosition(newPosition);
    }

    private void AimAtCursor()
    {
        Vector2 dir =  rb.position - (Vector2)Helper.Camera.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.SetRotation(Quaternion.Euler(new Vector3(0,0,angle+90)));
    }
}
