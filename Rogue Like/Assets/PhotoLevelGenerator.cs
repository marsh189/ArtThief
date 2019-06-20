using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoLevelGenerator : MonoBehaviour
{

    public Texture2D map;

    public ColorToPrefab[] colorMappings;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateSprite(x,y);
            }

        }
    }

    private void GenerateSprite(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            //transparent
            return;
        }

        foreach (ColorToPrefab color in colorMappings)
        {
            if (color.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(color.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
