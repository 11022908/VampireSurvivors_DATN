using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using ShopSystem;
using System;
public class OfflineManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonLevelPrefab;
    [SerializeField] private Transform levelContent;
    [SerializeField] private GameObject panelLoadingEffect;
    [SerializeField] private ShopUI shopUI;
    public static OfflineManager Instance;
    public SaveLoadData saveLoadData;

    public int idCharacter;
    public float characterMoveSpeed;
    public float characterHealth;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        

        saveLoadData = GameObject.Find("SaveLoadData").gameObject.GetComponent<SaveLoadData>();
        if (saveLoadData != null)
            saveLoadData.Initialize();
        idCharacter = shopUI.GetCharacterSelected();
        characterMoveSpeed = shopUI.GetSpeedSelected(idCharacter);
        characterHealth = shopUI.GetHealthSelected(idCharacter);
        Debug.Log("info character" + idCharacter + " " + characterMoveSpeed + " " + characterHealth);
    }
    public void OnButtonEventReceived(int newIndex, float newSpeed, float newHealth)
    {
        idCharacter = newIndex;
        characterMoveSpeed = newSpeed;
        characterHealth = newHealth;
        Debug.Log("info character" + idCharacter + " " + characterMoveSpeed + " " + characterHealth);
        Debug.Log("Received: " + newIndex + ", " + newSpeed + ", " + newHealth);
    }

    public void OnLevelButtonClick()
    {
        panelLoadingEffect.SetActive(true);
    }
    
    public void OnBackButtonClick()
    {
        AudioManager.Instance.PlaySFX("button_click");
        SceneManager.LoadScene(0);
    }

}
