using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public int m_ScreenID { get; private set; } = -1;

    private void Update()
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
