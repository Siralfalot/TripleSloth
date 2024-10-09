using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public System.Func<Rock> GetRock;

    public float m_TimeBetweenSpawns = 2f;
    public int m_ScreenID = -1;
    public Paths m_path;

    private float m_NextSpawn = 2f;
    private Vector3 m_InitialPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_InitialPosition = transform.position;
        m_NextSpawn = Time.time + m_TimeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > m_NextSpawn)
        {
            LaunchRock();
        }
    }

    void LaunchRock()
    {
        Rock rock = GetRock();
        if (rock != null)
        {
            rock.Launch(transform.position, m_ScreenID);
            m_NextSpawn = Time.time + m_TimeBetweenSpawns;
        }
        else
        {
            //We didn't get a trash. Wait half a second before trying again.
            m_NextSpawn = Time.time + 0.5f;
        }
    }
}
