using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed = .5f;
    public bool canScrollBackground = true;
    public bool isOriginal = false;
    public GameObject backgroundPrefab;

    SpriteRenderer sprite;
    //GameObject duplicateBG;
    //ScrollBackground parentScript;
    float spriteLength;
    Rigidbody2D rb;
    Vector2 scrollDirection;
    Vector3 spriteLengthOffset;

    //private static bool duplicateCreated = false;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //parentScript = GetComponentInParent<ScrollBackground>();
        
        spriteLength = sprite.bounds.size.y;
        scrollDirection = Vector2.down.normalized;
        spriteLengthOffset = new Vector3(0, spriteLength - 5f, 0);

        //Vector3 duplicatePos = this.transform.position + spriteLengthOffset;
        //if (!duplicateCreated)
        //{
        //    //Debug.Log("Parent does not have ScrollBackground script");
        //    Instantiate(this, this.transform.position + spriteLengthOffset, transform.rotation, this.transform);
        //    duplicateCreated = true;
        //}

        if (isOriginal)
        {
            Vector3 duplicatePosition = this.transform.position + spriteLengthOffset;
            GameObject duplicate = Instantiate(backgroundPrefab, duplicatePosition, transform.rotation) as GameObject;

            //duplicate.GetComponent<ScrollBackground>().isOriginal = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canScrollBackground)
        {
            rb.velocity = scrollDirection * scrollSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BottomWall"))
        {
            //Debug.Log("BottomWall hit");
            //Vector3 newPos = new Vector3(transform.position.x, spriteLength, transform.position.z);

            Vector2 preTeleportVelocity = rb.velocity;
            transform.position += spriteLengthOffset * 2;

            rb.velocity = preTeleportVelocity;
            //Debug.Log("preTeleportVelocity applied");
        }
    }
}
