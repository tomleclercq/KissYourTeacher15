using UnityEngine;
using System.Collections;

public class MapGeneratorScript : MonoBehaviour {

    public float countX;
    public float countY;

    public GameObject grass;
    GameObject tile;
    Vector2 position;
    Vector2 grassTileSize;

    void Start()
    {
        Init();
        CreateBaseMap();
    }

    void Init()
    {
        grassTileSize = grass.GetComponent<SpriteRenderer>().sprite.rect.size * 0.01f;
    }

    void CreateBaseMap()
    {
        for( int x= 0; x < countX ; x++ )
        {
            for( int y= 0; y < countY ; y++ )
            {
                tile = GameObject.Instantiate(grass) as GameObject;
                position.x = x * grassTileSize.x - ((countX / 2) * grassTileSize.x - grassTileSize.x/2);
                position.y = y * grassTileSize.y - ((countY / 2) * grassTileSize.y - grassTileSize.y/2);
                tile.transform.position = position;
                tile.transform.SetParent(transform);
            }
        }
    }
}
