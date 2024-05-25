using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelBtnScript : MonoBehaviour
{
    [SerializeField] private GameObject lockObj, unlockObj;
    [SerializeField] private Image[] starArray;
    [SerializeField] private TextMeshProUGUI levelIndexText;
    [SerializeField] private Color lockColor, unlockColor;
    [SerializeField] private Button btn;
    [SerializeField] private GameObject activeLevelIndicator;


    private int levelIndex;

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            OnClick();
        });
    }

    public void SetLevelButton(LevelItem value, int index, bool activeLevel)
    {
        if (value.unlocked)
        {
            activeLevelIndicator.SetActive(activeLevel);
            levelIndex = index + 1;
            btn.interactable = true;
            lockObj.SetActive(false);
            unlockObj.SetActive(true);
            SetStar(value.startAchieved);
            levelIndexText.text = levelIndex.ToString();
        }
        else
        {
            btn.interactable = false;
            lockObj.SetActive(true);
            unlockObj.SetActive(false);
        }
    }
    private void SetStar(int starAchieved)
    {
        for(int i = 0; i < starArray.Length; i++)
        {
            if(i < starAchieved)
            {
                starArray[i].color = unlockColor;
            }
            else
            {
                starArray[i].color = lockColor; 
            }
        }
    }
    void OnClick()
    {
        AudioManager.Instance.PlaySFX("button_click");
        AudioManager.Instance.PlayMusic("background_in_level");
        SceneManager.LoadScene($"Level {levelIndex}");
        OfflineManager.Instance.OnLevelButtonClick();
        LevelSystemManager.Instance.CurrentLevel = levelIndex - 1;
    }
}
