using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class cameraTarget : MonoBehaviour
{
    public GameObject box;
    void Update()
    {
        Camera camera = GetComponent<Camera>();
        Vector2 mousPos = Input.mousePosition;
        print(mousPos);
        Vector3 p = camera.ScreenToViewportPoint(new Vector3(mousPos.x, mousPos.y, camera.nearClipPlane));
        p = camera.ViewportToWorldPoint(p);
        if( box )
        {
            box.GetComponent<MeshRenderer>().material.color = Color.yellow;
            box.transform.position = p;
        }
    }
}