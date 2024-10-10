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
    Collider2D fishCollider;

    public bool isCollected = false;
    public bool moveToMouth = false;
    public bool mouthReached = false;
    public Vector2 whaleMouthPos;
    public FishSchoolUnit[] fishUnit;
    public Animator whaleAnimator;

    //public bool sideSwaying = true;
    //public float swaySpeed = .1f;
    //public float swayAmount = 0.05f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fishCollider = GetComponent<Collider2D>();

        fishUnit = GetComponentsInChildren<FishSchoolUnit>();

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
        if (!isCollected)
        {
            rb.velocity = direction * moveSpeed;
        }
        
        if(moveToMouth && !mouthReached)
        {
            rb.position = Vector3.Lerp(transform.position, whaleMouthPos, 5 * Time.deltaTime);

            float distanceToMouth = Vector3.Distance(transform.position, whaleMouthPos);

            if (distanceToMouth < 0.1f)
            {
                mouthReached = true;
            }
        }

        if (mouthReached)
        {
            whaleAnimator.ResetTrigger("WhaleCollectTrigger");
            whaleAnimator.SetTrigger("WhaleCollectTrigger");
            Destroy(this.gameObject);
        }

        if(spriteRotation)
        {
            RotateSprite();
        }

        //transform.position = rb.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject collidedObject = collision.gameObject;
        //Transform whaleMouthChild = collidedObject.GetComponentInChildren<Transform>();

        if (collision.gameObject.CompareTag("Wall"))
        {
            direction.x = -direction.x;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject collidedObject = collision.gameObject;

            whaleAnimator = collidedObject.GetComponent<Animator>();

            if (collidedObject.transform.childCount > 0)
            {
                Transform whaleMouthChild = collidedObject.transform.GetChild(0);
                whaleMouthPos = whaleMouthChild.position;
            }
            else
            {
                Debug.Log("No children found!");
            }

            fishCollider.enabled = false;
            StartCoroutine("FishCollection");
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

    private IEnumerator FishCollection()
    {
        Debug.Log("Player collision!!!!");
        isCollected = true;
        rb.velocity = Vector2.zero;
        //stop unit following

        foreach (FishSchoolUnit unit in fishUnit)
        {
            unit.whaleMouthPos = whaleMouthPos;
            unit.whaleAnimator = whaleAnimator;
            unit.leadIsCollected = true;
            yield return new WaitForSeconds(0.01f);
            Debug.Log("fishUnit happened!");
        }

        moveToMouth = true;
    }

    //Doesn't work, should be done with shaders or on sprite renderer directly anyways
    //private void SwaySideways()
    //{
    //    float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

    //    direction.x += sway;
    //}
}
