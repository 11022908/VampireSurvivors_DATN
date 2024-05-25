using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using ShopSystem;

public class SaveLoadData : MonoBehaviour
{
    
    private static SaveLoadData instance;
    public ShopUI shopUI;
    public static SaveLoadData Instance
    {
        get => instance;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            SaveData();
        }
    }
    private void Start()
    {
        shopUI = GameObject.Find("ShopUI").GetComponent<ShopUI>();
        if (shopUI != null)
            Debug.Log("found shop ui");
        else Debug.Log("not found shop ui");
    }
    public void Initialize()
    {
        if(PlayerPrefs.GetInt("GameStartedFirstTime") == 1)
        {
            LoadData();
        }
        else
        {
            SaveData();
            PlayerPrefs.SetInt("GameStartedFirstTime", 1);
        }
    }

    public void SaveData()
    {
        string levelDataString = JsonUtility.ToJson(LevelSystemManager.Instance.levelData);
        string shopDataString = JsonUtility.ToJson(shopUI.shopData);
        
        try
        {
            System.IO.File.WriteAllText(Application.persistentDataPath + "/LevelData.json", levelDataString);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/ShopData.json", shopDataString);
            
            Debug.Log("data saved");
        }
        catch(System.Exception e)
        {
            Debug.Log("err save" + e);
            throw;
        }
        PlayerPrefs.SetInt("PLAYER_COIN", shopUI.totalCoins);
    }
    public void LoadData()
    {
        try
        {
            string levelDataString = System.IO.File.ReadAllText(Application.persistentDataPath + "/LevelData.json");
            string shopDataString = System.IO.File.ReadAllText(Application.persistentDataPath + "/ShopData.json");
            
            LevelData levelData = JsonUtility.FromJson<LevelData>(levelDataString);
            if(levelData != null)
            {
                LevelSystemManager.Instance.levelData.levelItemArray = levelData.levelItemArray;
                LevelSystemManager.Instance.levelData.lastUnlockedLevel = levelData.lastUnlockedLevel;
            }
            shopUI.shopData = new ShopData();
            shopUI.shopData = JsonUtility.FromJson<ShopData>(shopDataString);
            


            Debug.Log("data loaded");
        }
        catch (System.Exception e)
        {
            Debug.Log("err load" + e);
            throw;
        }

        shopUI.totalCoins = PlayerPrefs.GetInt("PLAYER_COIN");
    }
}
