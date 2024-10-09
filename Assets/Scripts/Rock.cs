using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Vector2 m_velocity = Vector2.zero;
    public float m_Gravity = -9.8f;

    public int m_ScreenID { get; private set; } = -1;

    // Update is called once per frame
    void Update()
    {
        m_velocity.y += m_Gravity * Time.deltaTime;

        transform.position += (Vector3)m_velocity * Time.deltaTime;

        if (!ScreenUtility.IsOnScreen(transform.position, -1))
        {
            gameObject.SetActive(false);
        }
    }

    public void Launch(Vector2 position, int screenID)
    {
        m_ScreenID = screenID;
        gameObject.SetActive(true);
    }

}
