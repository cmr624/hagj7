using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> inventory;

    private void Awake()
    {
        inventory = new List<string>();
    }

    public void AddToInventory(string name)
    {
        inventory.Add(name);
        Debug.Log("Added " + name + " to inventory");
    }
}
