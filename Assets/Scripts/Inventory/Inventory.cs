using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of the Inventory found");
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 10;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Inventory is full");
            return false;
        }

        items.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool Combine(Item origin, Item target)
    {
        foreach(string name in target.combine)
        {
            if(name == origin.name)
            {
                if (target.quantity > 0 && origin.quantity > 0)
                    target.quantity += origin.quantity;
                
                items.Remove(origin);
                
                return true;
            }
        }
        return false;
    }
}
