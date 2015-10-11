using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookScript : MonoBehaviour
{
    static float ANIM_SPEED = 1.0f;
    Vector3 pos;
    Vector3 scale;

    bool open = false;
    float lerpValue;

    int pageCount = 0;

    public List<GameObject> pages = new List<GameObject>();

    void Start()
    {
        pos = transform.localPosition;
        scale = transform.localScale;

        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.zero;
        foreach(GameObject p in pages)
            p.SetActive(false);
    }

    void AddNewCollection( string _animal )
    {
        foreach (LanguageTranslaterScript ts in GetComponentsInChildren<LanguageTranslaterScript>())
        {
            ts.currentAnimal = _animal;
            ts.Init();
        }
        pages[pageCount].SetActive(true);
        pageCount++;
        //Toggle(true);
    }
	/*
    public void Toggle( bool _forceOpen = false)
    {
        if (!open || _forceOpen)
        {
            gameObject.SetActive(true);
            pages[pageCount].SetActive(true);
            AddNewCollection("crocodile");
            StartCoroutine(OpenBookE());
            open = true;
        }
        else
        {
            StartCoroutine(CloseBookE());
            gameObject.SetActive(false);
            open = false;
        }
    }
*/
    IEnumerator OpenBookE()
    {
        lerpValue = 0;
        while (lerpValue < 1)
        {
            transform.localPosition = Vector3.Lerp(Vector3.zero, pos, lerpValue);
            transform.localScale = Vector3.Lerp(Vector3.zero, scale, lerpValue);
            lerpValue += Time.deltaTime * ANIM_SPEED;
            yield return null;
        }
    }


    IEnumerator CloseBookE()
    {
        lerpValue = 0;
        while (lerpValue < 1)
        {
            transform.localPosition = Vector3.Lerp(pos, Vector3.zero, lerpValue);
            transform.localPosition = Vector3.Lerp(pos, Vector3.zero, lerpValue);
            lerpValue += Time.deltaTime * ANIM_SPEED;
            yield return null;
        }
    }

}
