using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerSprite
{
   Player,
   Jeep,
   Boat,
   Plane
}
public class MapTileData : MonoBehaviour
{
    public string MapDisplayName;
    public string ID;
    public Transform FastTravelSpawnPoint;

    public PlayerSprite PlayerSprite;

}
