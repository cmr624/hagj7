using System;
using System.Collections;
using System.Collections.Generic;
using CubaJam.Audio;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
  public string InventoryID;


  private void Start()
  {
    // small leantween scale tween
    //LeanTween.scale(gameObject, new Vector3(.7f, .7f, .1f), 1f).setLoopClamp().setLoopPingPong();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      SfxAudioEventDriver.PlayClip("collect");
      PlayerManager.Instance.Inventory.AddToInventory(InventoryID);
      Destroy(gameObject);
    }
  }
}
