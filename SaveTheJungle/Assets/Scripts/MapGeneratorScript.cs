using UnityEngine;
using System.Collections;

public class MapGeneratorScript : MonoBehaviour {

    public int countX;
    public int countY;

    public int seaSize;

    int countAndSeaX;
    int countAndSeaY;

    public GameObject grass;
    public GameObject bottom;
    public GameObject bottomRight;
    public GameObject bottomLeft;
    public GameObject top;
    public GameObject topRight;
    public GameObject topLeft;
    public GameObject left;
    public GameObject right;
    public GameObject sea;

    GameObject tile;
    Vector2 position;
    Vector2 grassTileSize;

    bool generated = false;

    public void Init()
    {
        if (this.transform.childCount == 0)
        {
            countAndSeaX = countX + seaSize;
            countAndSeaY = countY + seaSize;

            generated = false;
            grassTileSize = grass.GetComponent<SpriteRenderer>().sprite.rect.size * 0.01f;
        }
    }

    public void ClearMap()
    {
        Transform[] childs = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < childs.Length; i++)
            if (childs[i] != transform )
                DestroyImmediate(childs[i].gameObject);
        generated = false;
    }

    public void CreateBaseMap()
    {
        if (!generated)
        {
            int halfSeaSize = seaSize / 2;
            int halfSeaSizeEndX = countAndSeaX - (seaSize / 2) - 1;
            int halfSeaSizeEndY = countAndSeaY - (seaSize / 2) - 1;

            for (int x = 0; x < countAndSeaX; x++)
            {
                for (int y = 0; y < countAndSeaY; y++)
                {
                    if (y < halfSeaSize || x < halfSeaSize || x > halfSeaSizeEndX || y > halfSeaSizeEndY)
                        tile = GameObject.Instantiate(sea) as GameObject;
                    else
                    {
                        if (y == halfSeaSize)
                        {
                            if (x == halfSeaSize)
                                tile = GameObject.Instantiate(bottomLeft) as GameObject;
                            else
                                if (x == halfSeaSizeEndX)
                                    tile = GameObject.Instantiate(bottomRight) as GameObject;
                                else
                                    tile = GameObject.Instantiate(bottom) as GameObject;
                        }
                        else if (y == halfSeaSizeEndY)
                            if (x == halfSeaSize)
                                tile = GameObject.Instantiate(topLeft) as GameObject;
                            else
                                if (x == halfSeaSizeEndX)
                                    tile = GameObject.Instantiate(topRight) as GameObject;
                                else
                                    tile = GameObject.Instantiate(top) as GameObject;
                        else
                            if (x == halfSeaSize)
                                tile = GameObject.Instantiate(left) as GameObject;
                            else
                                if (x == halfSeaSizeEndX)
                                    tile = GameObject.Instantiate(right) as GameObject;
                                else
                                    tile = GameObject.Instantiate(grass) as GameObject;
                    }
                    position.x = x * grassTileSize.x - ((countAndSeaX / 2) * grassTileSize.x - grassTileSize.x / 2);
                    position.y = y * grassTileSize.y - ((countAndSeaY / 2) * grassTileSize.y - grassTileSize.y / 2);
                    tile.transform.position = position;
                    tile.transform.SetParent(transform);
                }
            }
            generated = true;
        }
    }
}
