using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Collectible Data", menuName = "Game Collectibles/Data", order = 0)]
    [Serializable]
    public class CollectibleData : ScriptableObject
    {
        public string itemName;
        public Sprite itemSprite;
    [TextArea]
        public string itemDescription;
    }
