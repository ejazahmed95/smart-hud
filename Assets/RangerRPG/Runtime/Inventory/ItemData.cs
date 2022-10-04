using System.Collections;
using System.Collections.Generic;
using RangerRPG.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject {
    [Header("Base Data")]
    public string id;
    public string displayName;
    public ItemType type;
    
    [Header("UI Data")]
    public Sprite icon;
    public GameObject prefab;

    public Tab tab;
    
    public string genre;
    public bool useable;
    public bool sellable;
    public int weight;
    public int limitNumber;
}
