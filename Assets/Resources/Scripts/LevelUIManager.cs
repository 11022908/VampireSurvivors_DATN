using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField]private GameObject levelBtnHolder;
    [SerializeField]private LevelBtnScript levelBtnPrefab;

    private void Start()
    {
        InitializeUI();
        Debug.Log("method start in level ui manager");
    }
    public void InitializeUI()
    {
        LevelItem[] levelItems = LevelSystemManager.Instance.levelData.levelItemArray;

        for (int i = 0; i < levelItems.Length; i++)
        {
            LevelBtnScript levelBtn = Instantiate(levelBtnPrefab, levelBtnHolder.transform);
            levelBtn.SetLevelButton(levelItems[i], i, i == LevelSystemManager.Instance.levelData.lastUnlockedLevel);
        }
    }
}
