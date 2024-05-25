using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AwardManager : MonoBehaviour
{
    

    [SerializeField] private Image[] starArray;
    [SerializeField] private Color lockColor, unlockColor;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject awardPanel;

    public void GameOver(int starCount)
    {
        if(starCount > 0)
        {
            winPanel.SetActive(true);
            awardPanel.SetActive(true);
            LevelSystemManager.Instance.LevelComplete(starCount);
        }
        else
        {
            awardPanel.SetActive(true);
            losePanel.SetActive(true);
            LevelSystemManager.Instance.LevelComplete(starCount);
        }
        SetStar(starCount);
    }


    private void SetStar(int starAchieved)
    {
        for (int i = 0; i < starArray.Length; i++)
        {
            if (i < starAchieved)
            {
                starArray[i].color = unlockColor;
            }
            else
            {
                starArray[i].color = lockColor;
            }
        }
    }
}
