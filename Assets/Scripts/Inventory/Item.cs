using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType { Equip, Consume, Static }
    public ItemType itemType;

    new public string name = "New Item";
    public string description;

    public int quantity;

    public string[] combine = new string[2];

    public Sprite icon = null;


}
