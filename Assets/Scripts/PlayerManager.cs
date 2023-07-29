using System;
using System.Collections;
using System.Collections.Generic;
using IndieMarc.TopDown;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    // singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public PlayerCharacter Player;
    public PlayerInventory Inventory;
}
