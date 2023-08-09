using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CityGenerator : MonoBehaviour
{
    private enum TileType
    {
        Free = -1,
        Road = 0
    }
    
    [SerializeField] private int mapHeight;
    [SerializeField] private int mapWidth;
    [SerializeField, Range(0, 100)] private int streetProbability;

    private int[,] map;

    private void Awake()
    {
        InitializeMap();
        GenerateMap();

        Debug.Log(GetMapLog());
    }

    private void InitializeMap()
    {
        map = new int[mapWidth, mapHeight];

        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidth; x++)
            {
                map[x, y] = (int)TileType.Free;
            }
        }
    }

    private void GenerateMap()
    {
        var startPos = new Vector2Int((int)(mapWidth * 0.5f), (int)(mapHeight * 0.5f));
        map[startPos.x, startPos.y] = 0;

        var currentCount = 0;
        var positionsToExpand = new Queue<Vector2Int>();
        positionsToExpand.Enqueue(startPos);

        while (positionsToExpand.Count > 0)
        {
            var positionToExpand = positionsToExpand.Dequeue();

            var positionsToCheck = new[]
            {
                positionToExpand + Vector2Int.up,
                positionToExpand + Vector2Int.down,
                positionToExpand + Vector2Int.left,
                positionToExpand + Vector2Int.right
            };

            foreach (var positionToCheck in positionsToCheck)
            {
                // Check if in the map borders
                if (positionToCheck.x < 0 || positionToCheck.x >= mapWidth ||
                    positionToCheck.y < 0 || positionToCheck.y >= mapHeight)
                    continue;

                // Check if is not already a street
                if (map[positionToCheck.x, positionToCheck.y] != (int)TileType.Free)
                    continue;

                // Random percentage
                //if (Random.Range(0, 100) > streetProbability)
                //    continue;
                
                // Check neighbours
                var neighbourCount = GetNeighbourCount(positionToCheck);
                if (neighbourCount < 2)
                {
                    if (Random.Range(0, 100) > streetProbability)
                        continue;
                }

                map[positionToCheck.x, positionToCheck.y] = (int)TileType.Road;
                positionsToExpand.Enqueue(positionToCheck);
                currentCount++;
            }
        }
    }

    private int GetNeighbourCount(Vector2Int position)
    {
        var count = 0;

        var positionsToSearch = new[]
        {
            position + Vector2Int.up,
            position + Vector2Int.down,
            position + Vector2Int.left,
            position + Vector2Int.right
        };

        foreach (var positionToSearch in positionsToSearch)
        {
            if (positionToSearch.x < 0 || positionToSearch.x >= mapWidth ||
                positionToSearch.y < 0 || positionToSearch.y >= mapHeight)
                continue;

            if (map[positionToSearch.x, positionToSearch.y] != (int)TileType.Free)
                count++;
        }
        
        return count;
    }

    private string GetMapLog()
    {
        var output = "";

        for (var y = 0; y < mapHeight; y++)
        {
            output += "|";
            for (var x = 0; x < mapWidth; x++)
            {
                var num = map[x, y];
                if (num >= 0)
                    output += " ";
                output += $"{num}|";
            }

            output += "\n";
        }

        return output;
    }
}
