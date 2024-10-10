using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public System.Func<Rock> GetRock;
    public System.Func<FishSpawn> GetFish;

    public float m_TimeBetweenSpawns = 4f;
    public int m_ScreenID = -1;
    public Paths m_path;

    private Vector3 leftSpawn;
    private Vector3 centerSpawn;
    private Vector3 rightSpawn;

    private bool spawnedLeft = false;
    private bool spawnedCenter = false;
    private bool spawnedRight = false;

    public int randomRockNumber;
    public int randomFishNumber;

    private float m_NextSpawn = 4f;
    private Vector3 m_InitialPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_InitialPosition = transform.position;
        m_NextSpawn = Time.time + m_TimeBetweenSpawns;

        leftSpawn = new Vector3(m_path.pathPositions[0], transform.position.y, transform.position.z);
        centerSpawn = new Vector3(m_path.pathPositions[1], transform.position.y, transform.position.z);
        rightSpawn = new Vector3(m_path.pathPositions[2], transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > m_NextSpawn)
        {
            LaunchRock();
            LaunchFish();
            m_NextSpawn = Time.time + m_TimeBetweenSpawns;
        }
    }

    void LaunchRock()
    {
        Rock rock = GetRock();
        if (rock != null)
        {
            spawnedLeft = false;
            spawnedCenter = false;
            spawnedRight = false;

            randomRockNumber = Random.Range(0, 3);
            if (randomRockNumber == 0)
            {
                rock.Launch(leftSpawn, m_ScreenID);
                spawnedLeft = true;
            }
            else if (randomRockNumber == 1)
            {
                rock.Launch(centerSpawn, m_ScreenID);
                spawnedCenter = true;
            }
            else if(randomRockNumber == 2)
            {
                rock.Launch(rightSpawn, m_ScreenID);
                spawnedRight = true;
            }
            
        }
        else
        {
            //We didn't get a trash. Wait half a second before trying again.
            m_NextSpawn = Time.time + 0.5f;
        }
    }

    void LaunchFish()
    {
        FishSpawn fish = GetFish();
        if (fish != null)
        {
            randomFishNumber = Random.Range(0, 3);
            if(randomFishNumber == 0)
            {
                if (spawnedLeft)
                {
                    int randomBackup = Random.Range(1, 3);
                    if(randomBackup == 1)
                    {
                        fish.Launch(centerSpawn, m_ScreenID);
                    }
                }
                else
                {
                    fish.Launch(leftSpawn, m_ScreenID);
                }
            }
            else if(randomFishNumber == 1)
            {
                if(spawnedCenter)
                {
                    int randomBackup = Random.Range(0, 3);
                    while(randomBackup == 1)
                    {
                        randomBackup = Random.Range(0, 3);
                    }
                    if(randomBackup == 0)
                    {
                        fish.Launch(leftSpawn, m_ScreenID);
                    }
                    else if(randomBackup == 2)
                    {
                        fish.Launch(rightSpawn,m_ScreenID);
                    }
                }
                else
                {
                    fish.Launch(centerSpawn, m_ScreenID);
                }
            }
            else if(randomFishNumber == 2)
            {
                if (spawnedRight)
                {
                    int randomBackup = Random.Range(0, 2);
                    if (randomBackup == 0)
                    {
                        fish.Launch(leftSpawn, m_ScreenID);
                    }
                }
                else
                {
                    fish.Launch(centerSpawn, m_ScreenID);
                }
            }
        }
    }
}
