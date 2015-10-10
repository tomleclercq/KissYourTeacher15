using UnityEngine;
using System.Collections;

public class MapGeneratorScript : MonoBehaviour {

    public float countX;
    public float countY;

    public GameObject grass;
    GameObject tile;
    Vector2 position;
    Vector2 grassTileSize;

    bool generated = false;

    public void Init()
    {
        if (this.transform.childCount == 0)
        {
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
            for (int x = 0; x < countX; x++)
            {
                for (int y = 0; y < countY; y++)
                {
                    tile = GameObject.Instantiate(grass) as GameObject;
                    position.x = x * grassTileSize.x - ((countX / 2) * grassTileSize.x - grassTileSize.x / 2);
                    position.y = y * grassTileSize.y - ((countY / 2) * grassTileSize.y - grassTileSize.y / 2);
                    tile.transform.position = position;
                    tile.transform.SetParent(transform);
                }
            }
            generated = true;
        }
    }
}
