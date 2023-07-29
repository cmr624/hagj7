using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
  public string InventoryID;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      PlayerManager.Instance.Inventory.AddToInventory(InventoryID);
      Destroy(gameObject);
    }
  }
}
