using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Database")]
public class Database : ScriptableObject
{
    public Item[] database;
}

[Serializable]
public class Item
{
    public string name;
    public int id;
    public Sprite icon;
    public Craftable craftable;
    public CraftingType craftingType;
    public TypeOfItem typeOfItem;
    /*public Item(string name, int id, Sprite icon, Craftable craftable, CraftingType craftingType, TypeOfItem typeOfItem)
    {
        this.name = name;
        this.id = id;
        this.icon = icon;
        this.craftable = craftable;
        this.craftingType = craftingType;
        this.typeOfItem = typeOfItem;
    }*/
}

public enum Craftable
{
    Craftable = 0,
    NotCraftable
}


public enum CraftingType
{
    None = 0,
    Inventory,
    CraftingBench,
    Furnace
}

public enum TypeOfItem
{
    Item = 0,
    Station,
    NotAnItem
}



#if UNITY_EDITOR
[CustomEditor(typeof(Item))]
public class CraftingTypeEditor : Editor
{
    private CraftingType craftingType = CraftingType.Inventory;
    private TypeOfItem typeOfItem = TypeOfItem.Item;
    private Craftable craftable = Craftable.Craftable;
    public override void OnInspectorGUI()
    {
        /*craftingType = (CraftingType)EditorGUILayout.EnumPopup("", craftingType);
        typeOfItem = (TypeOfItem)EditorGUILayout.EnumPopup("Type Of Item/Station", typeOfItem);
        craftable = (Craftable)EditorGUILayout.EnumPopup("", craftable);*/
        base.OnInspectorGUI();
    }
}
#endif