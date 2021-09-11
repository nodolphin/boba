using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemInfo
{
    [Header("Item info")]

    public Sprite sprite;
    public string name;

    [TextArea] public string description;

    [Header("Melt")]
    public short meltTemperature;
    public ItemType meltItemType;

    [Header("Freeze")]
    public short freezeTemperature;
    public ItemType freezeItemType;
}

public struct CookableInfo
{
    public ItemInfo itemInfo;
}

public class Globals : MonoBehaviour
{
    public static Globals instance = null;
    
    [SerializeField] private ItemInfo[] itemInfos;

    public LayerMask blockingLayer;
    
    public Dictionary<string, ItemType> itemCombinations = new Dictionary<string, ItemType>()
    {
        {"WATER+JASMYNG_TEA_LEAVES", ItemType.JASMYNG_TEA},
        {"WATER+CEYLONG_TEA_LEAVES", ItemType.CEYLONG_TEA},
        {"WATER+BOLONG_TEA_LEAVES", ItemType.BOLONG_TEA},
        {"WATER+PAROOT_TEA_LEAVES", ItemType.PAROOT_TEA},

        {"MILK+JASMYNG_TEA", ItemType.JASMYNG_MILK_TEA},
        {"MILK+CEYLONG_TEA", ItemType.CEYLONG_MILK_TEA},
        {"MILK+BOLONG_TEA", ItemType.BOLONG_MILK_TEA},
        {"MILK+PAROOT_TEA", ItemType.PAROOT_MILK_TEA},

        {"FISH_EGG+JASMYNG_MILK_TEA", ItemType.JASMYNG_FISH_EGG_MILK_TEA},
        {"FISH_EGG+CEYLONG_MILK_TEA", ItemType.CEYLONG_FISH_EGG_MILK_TEA},
        {"FISH_EGG+BOLONG_MILK_TEA", ItemType.BOLONG_FISH_EGG_MILK_TEA},
        {"FISH_EGG+PAROOT_MILK_TEA", ItemType.PAROOT_FISH_EGG_MILK_TEA},

        {"WATER+CARBON", ItemType.CARBON_TEA},
        {"MILK+CARBON_TEA", ItemType.CARBON_MILK_TEA}
    };

    void Awake()
    {
        MakeSingleton<Globals>(ref instance, gameObject);
    }

    public static void MakeSingleton<T>(ref T classInstance, GameObject currentGameObject)
        where T : Component
    {
        T currentInstance = currentGameObject.GetComponent<T>();
        if (classInstance == null)
        {
            classInstance = currentInstance;
        }
        else if (classInstance != currentInstance)
        {
            Destroy(currentGameObject);
        }
    }

    public static ItemInfo GetItemInfo (ItemType itemType)
    {
        return instance.itemInfos[(int) itemType];
    }

    public static T CheckCollision <T> (Vector2 position)
        where T : Component
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);

        foreach(Collider2D collider in colliders)
        {
            T component = collider.GetComponent<T>();
            if (component != null)
            {
                return component;
            }
        }

        return null;
    }

    public static string ConvertToKey(ItemType[] itemTypes)
    {
        Array.Sort(itemTypes, 0, itemTypes.Length);

        string ret = "";
        
        for (int i = 0; i < itemTypes.Length - 1; i++)
        {
            ret += itemTypes[i];
            ret += '+';
        }
        ret += itemTypes[itemTypes.Length - 1];

        return ret.ToString();
    }
}