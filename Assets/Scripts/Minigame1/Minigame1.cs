using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SimpleExampleGame {

    public class Minigame1 : MinigameBase
    {
        [Header("Game Specific variables")]
        //public Bullet pf_Bullet;
        //public Trash pf_Trash;
        public Rock pf_Rock;
        public FishSpawn pf_SmallFishSchool;
        public FishSpawn pf_MediumFishSchool;
        public FishSpawn pf_LargeFishSchool;


        [SerializeField] private PlayerBoat[] m_Players;
        [SerializeField] private List<Spawner> m_Spawner;
        [SerializeField] public Paths[] m_Paths;
        
        private List<Rock> m_Rock;
        private List<FishSpawn> m_SmallFish;
        private List<FishSpawn> m_MediumFish;
        private List<FishSpawn> m_LargeFish;

        private int[] m_Scores;

        public float gameSpeed = 2.0f;

        private void Awake()
        {
            MinigameLoaded.AddListener(InitialiseGame);
        }

        public override void OnDirectionalInput(int playerIndex, Vector2 direction)
        {
            m_Players[playerIndex].HandleDirectionalInput(direction);
        }

        public override void OnPrimaryFire(int playerIndex)
        {
            m_Players[playerIndex].HandleButtonInput(0);
        }

        public override void OnSecondaryFire(int playerIndex)
        {
            m_Players[playerIndex].HandleButtonInput(1);
        }

        public override void TimeUp()
        {
            OnGameComplete(true);
        }

        protected override void OnResetGame()
        {
            //InitialiseGame();
        }

        public override GameScoreData GetScoreData()
        {
            int teamTime = 0;
            GameScoreData gsd = new GameScoreData();
            for (int i = 0; i < 4; i++)
            {
                if (PlayerUtilities.GetPlayerState(i) == Player.PlayerState.ACTIVE)
                {
                    gsd.PlayerScores[i] = m_Scores[i];
                    gsd.PlayerTimes[i] = Mathf.Min(m_Scores[i]/2, 1);
                    teamTime += gsd.PlayerTimes[i];
                }
            }
            gsd.ScoreSuffix = " cleaned";
            gsd.TeamTime = teamTime;
            return gsd;
        }

        public void InitialiseGame()
        {
            Debug.Log("Initialising mini game");

            m_Scores = new int[4]{0,0,0,0};


            //Build pool of bullets
            /*m_Bullets = new List<Bullet>();
            Bullet b;
            for (int i =0; i < 50; i++)
            {
                b = Instantiate<Bullet>(pf_Bullet);
                b.gameObject.SetActive(false);
                m_Bullets.Add(b);
            }*/

            //Build pool of rocks
            m_Rock = new List<Rock>();
            Rock rock;
            for (int i = 0; i < 100; i++)
            {
                rock = Instantiate<Rock>(pf_Rock);
                rock.gameObject.SetActive(false);
                m_Rock.Add(rock);
            }

            m_SmallFish = new List<FishSpawn>();
            FishSpawn smallFish;
            for (int i = 0; i < 100; i++)
            {
                smallFish = Instantiate<FishSpawn>(pf_SmallFishSchool);
                smallFish.gameObject.SetActive(false);
                m_SmallFish.Add(smallFish);
            }

            m_MediumFish = new List<FishSpawn>();
            FishSpawn mediumFish;
            for (int i = 0; i < 100; i++)
            {
                mediumFish = Instantiate<FishSpawn>(pf_MediumFishSchool);
                mediumFish.gameObject.SetActive(false);
                m_MediumFish.Add(mediumFish);
            }

            m_LargeFish = new List<FishSpawn>();
            FishSpawn largeFish;
            for (int i = 0; i < 100; i++)
            {
                largeFish = Instantiate<FishSpawn>(pf_LargeFishSchool);
                largeFish.gameObject.SetActive(false);
                m_LargeFish.Add(largeFish);
            }

            /*for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].GetBullet = GetBullet;
                m_Players[i].m_ScreenID = i;
            }*/
            for (int i = 0; i < m_Spawner.Count; i++)
            {
                m_Spawner[i].GetRock = GetRock;
                m_Spawner[i].GetSFish = GetSFish;
                m_Spawner[i].GetMFish = GetMFish;
                m_Spawner[i].GetLFish = GetLFish;
                m_Spawner[i].m_ScreenID = i;
            }

            /*for (int i = 0; i < m_Paths.Length; i++) 
            {
                m_Paths[i]
            }*/
        }

        /*Bullet GetBullet()
        {
            Bullet returnBullet = null;
            for (int i = 0; i < m_Bullets.Count; i++)
            {
                if (m_Bullets[i].gameObject.activeSelf)
                    continue;
                returnBullet = m_Bullets[i];
            }
            return returnBullet;
        }*/

        Rock GetRock()
        {
            Rock returnRock = null;
            for (int i = 0; i < m_Rock.Count; i++)
            {
                if (m_Rock[i].gameObject.activeSelf)
                    continue;
                returnRock = m_Rock[i];
            }
            return returnRock;
        }

        FishSpawn GetSFish()
        {
            FishSpawn returnFish = null;
            for (int i = 0; i < m_SmallFish.Count; i++)
            {
                if (m_SmallFish[i].gameObject.activeSelf)
                    continue;
                returnFish = m_SmallFish[i];
            }
            return returnFish;
        }

        FishSpawn GetMFish()
        {
            FishSpawn returnFish = null;
            for (int i = 0; i < m_MediumFish.Count; i++)
            {
                if (m_MediumFish[i].gameObject.activeSelf)
                    continue;
                returnFish = m_MediumFish[i];
            }
            return returnFish;
        }

        FishSpawn GetLFish()
        {
            FishSpawn returnFish = null;
            for (int i = 0; i < m_LargeFish.Count; i++)
            {
                if (m_LargeFish[i].gameObject.activeSelf)
                    continue;
                returnFish = m_LargeFish[i];
            }
            return returnFish;
        }

        private void FixedUpdate()
        {
            foreach (Rock r in m_Rock)
            {
                if (!r.gameObject.activeSelf)
                    continue;
                if (Vector3.SqrMagnitude(m_Players[r.m_ScreenID].transform.position - r.transform.position) < 0.25f)
                {
                    m_Scores[r.m_ScreenID]--;
                    r.gameObject.SetActive(false);
                    break;
                }
                
            }
            foreach (FishSpawn f in m_SmallFish)
            {
                if (!f.gameObject.activeSelf)
                    continue;
                if (Vector3.SqrMagnitude(m_Players[f.m_ScreenID].transform.position -  f.transform.position) < 0.25f)
                {
                    int i = 0;
                    while(i < 4)
                    {
                        m_Scores[f.m_ScreenID]++;
                        i++;
                    }
                    f.gameObject.SetActive(false);
                    break;
                }
            }
            foreach (FishSpawn f in m_MediumFish)
            {
                if (!f.gameObject.activeSelf)
                    continue;
                if (Vector3.SqrMagnitude(m_Players[f.m_ScreenID].transform.position - f.transform.position) < 0.25f)
                {
                    int i = 0;
                    while (i < 9)
                    {
                        m_Scores[f.m_ScreenID]++;
                        i++;
                    }
                    f.gameObject.SetActive(false);
                    break;
                }
            }
            foreach (FishSpawn f in m_LargeFish)
            {
                if (!f.gameObject.activeSelf)
                    continue;
                if (Vector3.SqrMagnitude(m_Players[f.m_ScreenID].transform.position - f.transform.position) < 0.25f)
                {
                    int i = 0;
                    while (i < 14)
                    {
                        m_Scores[f.m_ScreenID]++;
                        i++;
                    }
                    f.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}
