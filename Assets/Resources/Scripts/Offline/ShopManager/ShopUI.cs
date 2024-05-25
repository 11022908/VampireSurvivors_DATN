using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

namespace ShopSystem
{
    public class ShopUI : MonoBehaviour
    {

        [System.Serializable]
        public class ButtonEvent : UnityEvent<int, float, float> { }

        public ButtonEvent buttonEvent;

        public int totalCoins = 1500;
        public ShopData shopData;
        public GameObject[] characterModels;
        public TextMeshProUGUI unlockBtnText, upgradeBtnText, levelText, characterNameText;
        public TextMeshProUGUI speedText, healthText, totalCoinText;
        public Button unlockBtn, upgradeBtn, nextBtn, prevBtn;
        public SaveLoadData saveLoadData;

        private int currentIndex = 0;
        public int selectedIndex =  0;

        
        

        private void Start()
        {
            
            saveLoadData = GameObject.Find("SaveLoadData").gameObject.GetComponent<SaveLoadData>();
            if(saveLoadData != null)
                saveLoadData.Initialize();
            selectedIndex = shopData.selectedIndex;
            currentIndex = selectedIndex;
            totalCoinText.text = totalCoins.ToString();

            SetCharacterInfo();

            unlockBtn.onClick.AddListener(() => UnlockSelectedBtnMethod());
            upgradeBtn.onClick.AddListener(() => UpgardeSelectedBtnMethod());
            nextBtn.onClick.AddListener(() => NextBtnMethod());
            prevBtn.onClick.AddListener(() => PrevBtnMethod());

            for (int i = 0; i < shopData.shopItems.Length; i++)
            {
                if(i == selectedIndex)
                    characterModels[i].SetActive(true);
                else
                    characterModels[i].SetActive(false);
            }

            if (currentIndex == 0)
            {
                prevBtn.interactable = false;
            }
            if (currentIndex == shopData.shopItems.Length - 1) nextBtn.interactable = false;

            UpgradeBtnStatus();
            UnlockBtnStatus();
        }
        public int GetCharacterSelected()
        {
            return shopData.selectedIndex;
        }
        public float GetSpeedSelected(int selectedIndex)
        {
            int currentLevel = shopData.shopItems[selectedIndex].unlockLevel;
            return shopData.shopItems[selectedIndex].characterLevel[currentLevel].speed;
        }
        public float GetHealthSelected(int selectedIndex)
        {
            int currentLevel = shopData.shopItems[selectedIndex].unlockLevel;
            return shopData.shopItems[selectedIndex].characterLevel[currentLevel].health;
        }
        private void SetCharacterInfo()
        {
            characterNameText.text = shopData.shopItems[currentIndex].itemName;
            int currentLevel = shopData.shopItems[currentIndex].unlockLevel;
            levelText.text = "Level: " + (currentLevel + 1).ToString();
            speedText.text = "Speed: " + shopData.shopItems[currentIndex].characterLevel[currentLevel].speed;
            healthText.text = "Health: " + shopData.shopItems[currentIndex].characterLevel[currentLevel].health;
        }

        private void UnlockSelectedBtnMethod()
        {
            bool yesSelected = false;
            if (shopData.shopItems[currentIndex].isUnlocked)
            {
                yesSelected = true;
            }
            else
            {
                if(totalCoins >= shopData.shopItems[currentIndex].unlockCost)
                {
                    totalCoins -= shopData.shopItems[currentIndex].unlockCost;
                    totalCoinText.text = "" + totalCoins;
                    yesSelected = true;
                    shopData.shopItems[currentIndex].isUnlocked = true;
                    UpgradeBtnStatus();
                    saveLoadData.SaveData();
                    
                }
            }
            if (yesSelected)
            {
                unlockBtnText.text = "Selected";
                selectedIndex = currentIndex;
                shopData.selectedIndex = selectedIndex;
                unlockBtn.interactable = false;
                UpgradeBtnStatus();
                UnlockBtnStatus();
                saveLoadData.SaveData();
            }

            buttonEvent.Invoke(selectedIndex, GetSpeedSelected(selectedIndex), GetHealthSelected(selectedIndex));
            
        }
        private void UpgardeSelectedBtnMethod()
        {
            int nextLevelIndex = shopData.shopItems[currentIndex].unlockLevel + 1;
            if(totalCoins >= shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost)
            {
                totalCoins -= shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost;
                totalCoinText.text = "" + totalCoins;
                shopData.shopItems[currentIndex].unlockLevel++;

                if(shopData.shopItems[currentIndex].unlockLevel < shopData.shopItems[currentIndex].characterLevel.Length - 1)
                {
                    upgradeBtnText.text = "Upgrade " +
                        shopData.shopItems[currentIndex].characterLevel[nextLevelIndex + 1].unlockCost;
                }
                else
                {
                    upgradeBtn.interactable = false;
                    upgradeBtnText.text = "Max Level";
                }
                SetCharacterInfo();
                saveLoadData.SaveData();
                buttonEvent.Invoke(selectedIndex, GetSpeedSelected(selectedIndex), GetHealthSelected(selectedIndex));
            }
            
        }
        private void NextBtnMethod()
        {
            if(currentIndex < shopData.shopItems.Length - 1)
            {
                characterModels[currentIndex].SetActive(false);
                currentIndex++;
                characterModels[currentIndex].SetActive(true);
                SetCharacterInfo();

                if (currentIndex == shopData.shopItems.Length - 1) nextBtn.interactable = false;
                if (!prevBtn.interactable) prevBtn.interactable = true;

                UpgradeBtnStatus();
                UnlockBtnStatus();
            }
        }
        private void PrevBtnMethod()
        {
            if (currentIndex > 0)
            {
                characterModels[currentIndex].SetActive(false);
                currentIndex--;
                characterModels[currentIndex].SetActive(true);
                SetCharacterInfo();

                if (currentIndex == 0) prevBtn.interactable = false;
                if (!nextBtn.interactable) nextBtn.interactable = true;

                UpgradeBtnStatus();
                UnlockBtnStatus();
            }
        }

        private void UnlockBtnStatus()
        {
            if (shopData.shopItems[currentIndex].isUnlocked)
            {
                unlockBtn.interactable = selectedIndex != currentIndex;
                
                unlockBtnText.text = selectedIndex == currentIndex ? "Selected" : "Select";
            }
            else
            {
                unlockBtn.interactable = true;
                unlockBtnText.text = "Cost: " + shopData.shopItems[currentIndex].unlockCost;
            }
        }

        private void UpgradeBtnStatus()
        {
            if (shopData.shopItems[currentIndex].isUnlocked)
            {
                if (shopData.shopItems[currentIndex].unlockLevel < shopData.shopItems[currentIndex].characterLevel.Length - 1)
                {
                    int nextLevelIndex = shopData.shopItems[currentIndex].unlockLevel + 1;
                    upgradeBtn.interactable = true;
                    upgradeBtnText.text = "Upgrade " +
                        shopData.shopItems[currentIndex].characterLevel[nextLevelIndex].unlockCost;
                }
                else
                {
                    upgradeBtn.interactable = false;
                    upgradeBtnText.text = "Max Level";
                }
            }
            else
            {
                upgradeBtn.interactable = false;
                upgradeBtnText.text = "Locked";
            }
        }
    }
}

