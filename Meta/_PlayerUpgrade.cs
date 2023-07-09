using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

[System.Serializable]
public struct LevelData
{
    [TextArea(3, 5)]
    public string description;
    public int cost;
}

[CreateAssetMenu(fileName = "PlayerUpgrade", menuName = "Meta/PlayerUpgrade")]
public class _PlayerUpgrade : _MetaData
{
    //Sprite icon;

    public int level = 0;
    public List<LevelData> levels = new();
    
}
