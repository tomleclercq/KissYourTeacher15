using UnityEngine;
using System.Collections;

public class SpriteHiderScript : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
