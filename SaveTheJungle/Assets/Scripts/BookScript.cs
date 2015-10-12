using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BookScript : MonoBehaviour
{
    static float ANIM_SPEED = 6f;
    public static bool openned = false;
    Vector3 pos;
    Vector3 scale;

    float lerpValue = 0;

    int currentKnownPageID=-1;
    int currentPageID = -1;
    GameObject currentPage = null;

    public Image darker;

    public GameObject buttonOpen;
    public GameObject buttonNext;
    public GameObject buttonPrevious;
    public GameObject buttonQuit;
    public GameObject textRoot;

    public List<GameObject> pages = new List<GameObject>();
    [HideInInspector]
    public List<int> knownPageIds = new List<int>();

    void Start()
    {
        pos = transform.localPosition;
        scale = transform.localScale;

        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.zero;
        foreach(GameObject p in pages)
            p.SetActive(false);
        buttonOpen.SetActive(false);
        buttonNext.SetActive(false);
        buttonPrevious.SetActive(false);
        buttonQuit.SetActive(false);
    }

    public void AddNewCollection(string _animal)
    {

        gameObject.SetActive(true);
        ApplicationScript.current.animalName = _animal;

        currentPage = null;
        currentPageID = -1;

        foreach (GameObject go in pages)
            if (go.name.ToLower().Contains(ApplicationScript.current.animalName.ToLower()))
                currentPage = go;

        if (currentPage != null)
        {
            currentPageID = pages.IndexOf(currentPage);
            if (!knownPageIds.Contains(currentPageID))
                knownPageIds.Add(currentPageID);
            currentKnownPageID = knownPageIds.Count-1;
            OpenTHEBook();
        }
    }

    public void OpenTHEBook()
    {
        gameObject.SetActive(true);
        if (currentPage == null)
        {
            if (knownPageIds.Count > 0)
            {
                currentKnownPageID = 0;
                currentPageID = knownPageIds[currentKnownPageID];
            }
            if (currentPageID >=0 && currentPageID < pages.Count)
                currentPage = pages[currentPageID];
        }

        if (currentPage != null && !BookScript.openned)
        {
            UpdateData();
            StartCoroutine(openAnimation());
            ButtonNavVisibility();

            buttonOpen.SetActive(!(knownPageIds.Count > 0));
            buttonQuit.SetActive(true);
        }
    }

    private void ButtonNavVisibility()
    {
        buttonNext.SetActive(currentKnownPageID < knownPageIds.Count - 1);
        buttonPrevious.SetActive(currentKnownPageID > 0);
    }

    IEnumerator openAnimation()
    {
        gameObject.SetActive(true);
        currentPage.SetActive(true);
        lerpValue = 0;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * ANIM_SPEED;
            transform.localPosition = Vector3.Lerp(Vector3.zero, pos, lerpValue);
            transform.localScale = Vector3.Lerp(Vector3.zero, scale, lerpValue);
            yield return null;
        }
        BookScript.openned = true;
        lerpValue = 0;
        Color color = darker.color;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * ANIM_SPEED;
            color.a = Mathf.Lerp(0f, 0.75f, lerpValue);
            darker.color = color;
            yield return null;
        }
        Time.timeScale = 0f;
        
        yield return null;

    }

    public void CloseTHEBook()
    {
        if (BookScript.openned)
        {
            Time.timeScale = 1f;
            StartCoroutine(closeAnimation());
        }
    }

    IEnumerator closeAnimation()
    {
        lerpValue = 0;
        Color color = darker.color;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * ANIM_SPEED;
            color.a = Mathf.Lerp(0.75f, 0f, lerpValue);
            darker.color = color;
            yield return null;
        }

        lerpValue = 0;
        while (lerpValue < 1)
        {
            lerpValue += Time.deltaTime * ANIM_SPEED;
            transform.localPosition = Vector3.Lerp(pos, Vector3.zero, lerpValue);
            transform.localScale = Vector3.Lerp(scale, Vector3.zero, lerpValue);
            yield return null;
        }
        BookScript.openned = false;

        currentPage.SetActive(false);
        gameObject.SetActive(false);
        currentPageID = -1;
        currentPage = null;

        buttonNext.SetActive(false);
        buttonPrevious.SetActive(false);
        buttonOpen.SetActive( knownPageIds.Count > 0 );
        buttonQuit.SetActive(false);
        yield return null;
    }

    private void UpdateData()
    {
        foreach (LanguageTranslaterScript ts in textRoot.GetComponentsInChildren<LanguageTranslaterScript>())
            ts.Init();

        ButtonNavVisibility();
    }

    public void NextPage()
    {
        currentKnownPageID = currentKnownPageID < knownPageIds.Count - 1 ? currentKnownPageID + 1 : knownPageIds.Count - 1;
        currentPageID = knownPageIds[currentKnownPageID];

        currentPage.SetActive(false);
        currentPage = pages[currentPageID];
        ApplicationScript.current.animalName = currentPage.name;
        currentPage.SetActive(true);
        UpdateData();
    }

    public void PreviousPage()
    {
        currentKnownPageID = currentKnownPageID > 0 ? currentKnownPageID - 1 : 0;
        currentPageID = knownPageIds[currentKnownPageID];

        currentPage.SetActive(false);
        currentPage = pages[currentPageID];
        ApplicationScript.current.animalName = currentPage.name;
        currentPage.SetActive(true);
        UpdateData();
    }

}
