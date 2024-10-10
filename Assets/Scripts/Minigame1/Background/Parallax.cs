using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollSpeed;
    public bool canScroll = true;
    private Vector3 startPosition;
    private float textureHeight;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        textureHeight = (sprite.texture.height / sprite.pixelsPerUnit)*transform.localScale.y;

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canScroll)
        {
            float direction = scrollSpeed * Time.deltaTime;
            transform.position += new Vector3(0, -direction, 0);
        }

        if ((Mathf.Abs(transform.position.y) - textureHeight) > 0)
        {
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
        }
    }
}
