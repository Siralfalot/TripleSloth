using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen_Player : MonoBehaviour
{
    [SerializeField] private Image    m_ScoreIcon;

    [SerializeField] private TMP_Text[]   m_ScoreText;

    [SerializeField] private Image[]     m_playerImages;

    [SerializeField] public Sprite[]     m_playerSprites;

    [SerializeField] private TMP_Text   m_PlayerTimeText;

    [SerializeField] private Image      m_TeamIcon;
    [SerializeField] private TMP_Text   m_TeamTimeText;



    public void ShowWinScreen(GameScoreData scoreData, int index)
    {
        /*m_ScoreIcon.sprite = ScoreIcon;
        m_ScoreText.text = scoreData.PlayerScores[index].ToString() + scoreData.ScoreSuffix;

        //m_PlayerIcon;
        m_PlayerTimeText.text = scoreData.PlayerTimes[index].ToString() + "s";

        //m_TeamIcon;
        m_TeamTimeText.text = scoreData.TeamTime.ToString() + "s";*/

        Debug.Log("ShowWinScreen");

        detectRankings(scoreData);        
    }

    public void detectRankings(GameScoreData gsd)
    {
        int[] scoreOrder = { 0, 0, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            scoreOrder[i] = gsd.PlayerScores[i];
        }

        Array.Sort(scoreOrder);
        Array.Reverse(scoreOrder);

        for (int i = 0; i < 4; i++)
        {
            Debug.Log(scoreOrder[i] + " " + i);
        }

        changePlayerScores(scoreOrder);

        changePlayerIcons(gsd, scoreOrder);
    }

    public void changePlayerIcons(GameScoreData gsd, int[]scoreOrder)
    {
        for (int i=0; i < 4; i++)
        {
            if (gsd.PlayerScores[i] == scoreOrder[0])
            {
                m_playerImages[0].sprite = m_playerSprites[i];
            }
            else if (gsd.PlayerScores[i] == scoreOrder[1])
            {
                m_playerImages[1].sprite = m_playerSprites[i];
            }
            else if (gsd.PlayerScores[i] == scoreOrder[2])
            {
                m_playerImages[2].sprite = m_playerSprites[i];
            }
            else if(gsd.PlayerScores[i] == scoreOrder[3])
            {
                m_playerImages[3].sprite = m_playerSprites[i];
            }
        }
    }

    public void changePlayerScores(int[] scoreOrder)
    {
        int totalScore = 0;

        for (int i = 0; i < 4; i++)
        {
            m_ScoreText[i].text = scoreOrder[i].ToString();

            totalScore += scoreOrder[i];
        }        
    }

    public void countUp(GameScoreData gsd)
    {
        if (this.isActiveAndEnabled)
        {
            StartCoroutine(countUpTeamScore(gsd));
        }
    }

    IEnumerator countUpTeamScore(GameScoreData gsd)
    {
        int totalScore = 0;

        for (int i = 0; i < 4;i++)
        {
            totalScore = totalScore + gsd.PlayerScores[i];
        }

        if (this.isActiveAndEnabled)
        {
            for (int i = 0; i < totalScore + 1; i++)
            {
                yield return new WaitForSeconds(0.01f);

                m_TeamTimeText.text = i.ToString();
            }
        }
        else
        {
            Debug.Log("SceneNotActive");
        }

        
    }

}
