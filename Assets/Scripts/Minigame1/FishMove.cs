using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    public bool moveHorizontal = true;
    public bool moveVertical = true;
    public bool spriteRotation = true;

    public float moveSpeed = 2f;
    public float spriteRotationSpeed = 1000f;

    Vector2 direction;
    Rigidbody2D rb;

    //public bool sideSwaying = true;
    //public float swaySpeed = .1f;
    //public float swayAmount = 0.05f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //Can add random ranges instead
        if (moveHorizontal)
        {
            direction += Vector2.right.normalized;
        }

        if (moveVertical)
        {
            direction += Vector2.down.normalized;
        }

        //Flip sprite to face downwards if fish stays in position
        if (!moveHorizontal && !moveVertical)
        {
            //transform.Rotate(0f, 0f, 180f);
            rb.MoveRotation(180);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = direction * moveSpeed;

        if(spriteRotation)
        {
            RotateSprite();
        }

        transform.position = rb.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction.x = -direction.x;
        }
    }

    private void RotateSprite()
    {
        //if (rb.velocity.magnitude != 0)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(transform.forward, direction);
        //    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, spriteRotationSpeed * Time.deltaTime);

        //    rb.MoveRotation(rotation);
        //}

        if (direction != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, spriteRotationSpeed * Time.deltaTime);
        }
    }

    //Doesn't work, should be done with shaders or on sprite renderer directly anyways
    //private void SwaySideways()
    //{
    //    float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

    //    direction.x += sway;
    //}
}
