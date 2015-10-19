using UnityEngine;
using System.Collections;

public class OrderLayerActiveScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    void Start () 
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        UpdateLayer();
    }

    private void UpdateLayer()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y / 2.56) * -2 + 200;
    }

    void Update()
    {
        if (spriteRenderer != null && spriteRenderer.enabled)
            UpdateLayer();
    }
}
