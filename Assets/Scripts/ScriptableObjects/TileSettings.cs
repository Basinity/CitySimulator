using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CityGenerator/TileSettings")]
public class TileSettings : ScriptableObject
{
    public string tileName;
    public CityTile tile;
    [NonReorderable] public List<CityTileProperties> upNeighbours;
    [NonReorderable] public List<CityTileProperties> downNeighbours;
    [NonReorderable] public List<CityTileProperties> leftNeighbours;
    [NonReorderable] public List<CityTileProperties> rightNeighbours;
    public float probability;
}