using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public GameObject render;
    public string teclas = "Horizontal";

    [Header("Movimiento")]
    private float horizontalMovement = 0f;

    [SerializeField] public float movementSpeed = 10f;
    [SerializeField] public float forceController = 30f;
    [SerializeField] public float airForce = 2f;
    [SerializeField] public bool touchingFloor = true;

    private bool lookingRight = true;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw(teclas);

        if (horizontalMovement > 0 && !lookingRight)
        {
            Turn();
        }
        else if (horizontalMovement < 0 && lookingRight)
        {
            Turn();
        }
    }

    private void FixedUpdate()
    {
        if (horizontalMovement != 0)
        {
            ApplyForce(horizontalMovement);
        }
        else
        {
            BreakMovement();
        }
    }

    private void ApplyForce(float direction)
    {
        float actualSpeed = rb2D.velocity.x;
        float targetSpeed = direction * movementSpeed;
        float speedDiff = targetSpeed - actualSpeed;

        Vector2 force = new Vector2(speedDiff * forceController, 0);
        rb2D.AddForce(force, ForceMode2D.Force);
    }

    private void BreakMovement()
    {
        float horizontalSpeed = rb2D.velocity.x;

        if (Mathf.Abs(horizontalSpeed) < 0.1f)
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        }
    }

    private void Turn()
    {
        lookingRight = !lookingRight;
        Vector3 scale = render.transform.localScale;
        scale.x *= -1;
        render.transform.localScale = scale;
    }
}
