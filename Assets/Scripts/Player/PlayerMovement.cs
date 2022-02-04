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

    private Rigidbody2D _rigidbody2D;
    public Rigidbody2D rigidbody2D
    {
        get { return _rigidbody2D; }
    }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
        Vector2 velocity = new Vector2(horizontalInput, verticalInput).normalized * speed * Time.fixedDeltaTime;
        _rigidbody2D.velocity = velocity;
    }

    private void AimAtCursor()
    {
        Vector2 dir =  _rigidbody2D.position - (Vector2)Helper.Camera.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _rigidbody2D.SetRotation(Quaternion.Euler(new Vector3(0,0,angle+90)));
    }
}
