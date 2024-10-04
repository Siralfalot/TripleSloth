using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchoolUnit : MonoBehaviour
{
    public GameObject manager;
    public bool moveTowardsPosition = true;
    public bool spriteRotation = true;
    public float moveSpeed = 10f;
    public float spriteRotationSpeed = 1000f;
    public Vector3 initialOffset;

    //Rigidbody2D rb;
    Vector2 moveDir;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();

        //Flip sprite to face downwards if fish stays in position
        //if (!moveTowardsPosition)
        //{
        //    //rb.MoveRotation(180);
        //    transform.rotation = Quaternion.Euler(0, 0, 180);
        //}

        transform.rotation = Quaternion.Euler(0, 0, 180);

        initialOffset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTowardsPosition)
        {
            Vector3 offsetPosition = transform.parent.position + initialOffset;

            moveDir = offsetPosition - transform.position;
            moveDir.Normalize();

            //rb.velocity = moveDir * moveSpeed;
            //transform.position += (Vector3)(moveDir * moveSpeed * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, offsetPosition, moveSpeed * Time.deltaTime);
        }

        if(spriteRotation)
        {
            RotateSprite();
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        RotateSprite();
    //    }
    //}
    private void RotateSprite()
    {
        //if (rb.velocity.magnitude != 0)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(transform.forward, moveDir);
        //    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, spriteRotationSpeed * Time.deltaTime);

        //    rb.MoveRotation(rotation);
        //}

        //if (moveDir.magnitude != 0)
        //{
        //    // Calculate the angle based on the move direction
        //    float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        //    // Set the rotation of the transform
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), spriteRotationSpeed * Time.deltaTime);
        //}

        if (moveDir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, spriteRotationSpeed * Time.deltaTime);
        }
    }
}
