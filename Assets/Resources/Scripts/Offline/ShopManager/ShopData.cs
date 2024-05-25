using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    [System.Serializable]
    public class ShopData 
    {
        public int selectedIndex;
        public ShopItem[] shopItems;
    }
    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public bool isUnlocked;
        public int unlockCost;
        public int unlockLevel;
        public CharacterInfo[] characterLevel;
    }

    [System.Serializable]
    public class CharacterInfo
    {
        public int unlockCost;
        public int speed;
        public int health;
    }
}
