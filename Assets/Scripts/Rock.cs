using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Vector2 m_velocity = Vector2.zero;
    public float moveSpeed = 2f;

    public int m_ScreenID { get; private set; } = -1;

    Vector2 direction;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction += Vector2.down.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ScreenUtility.IsOnScreen(transform.position, -1))
        {
            gameObject.SetActive(false);
        }
    }

    public void Launch(Vector2 position, int screenID)
    {
        m_ScreenID = screenID;
        transform.position = position;
        gameObject.SetActive(true);
    }

}
