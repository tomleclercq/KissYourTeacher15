using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System;

public class ApplicationScript : MonoBehaviour
{
    static private ApplicationScript currentApp;
    static public ApplicationScript current
    {
        get
        {
            if( currentApp == null )currentApp = GameObject.Find("/Application").GetComponent<ApplicationScript>();
            return currentApp;
        }
    }

    public static string getDataFolder()
    {
        string folder = Application.dataPath + @"/../Data/";
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        return folder;
    }

    public UIScript UIRoot;
    public GameObject player;
    public GameObject endPoint;
    public int requestedAnimals;
    public GameObject[] textsRoot;
    public Languages currentLanguage;
    private Languages previousLanguage;

    [HideInInspector]
    public bool questAccompleted;
    [HideInInspector]
    public JsonData jsonLanguage;
    [HideInInspector]
    public string animalName;
    [HideInInspector]
    public bool switchingLanguage = false;

    void Start ()
    {
        string file = File.ReadAllText( getDataFolder()+"Languages.json");
        jsonLanguage = JsonMapper.ToObject(file);
        
        if (UIRoot != null)
            UIRoot.SetMenuUI();
        UpdateTextTranslaters();

        if (Application.loadedLevel != 0)
        {
            if (UIRoot != null)
                UIRoot.SetGameUI();
        }
        previousLanguage = currentLanguage;
    }

    public void LaunchGame()
    {
        StartCoroutine(StartGame());
    }

    public void LaunchCredits()
    {
        StartCoroutine(ShowCredits());
    }

    public void ExitGame()
    {
        StartCoroutine(QuitGame());
    }

    IEnumerator QuitGame()
    {
        yield return StartCoroutine( UIRoot.DarkenScreen() );
        yield return StartCoroutine(UIRoot.StartCreditScroll());
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.75f);
        UIRoot.SetGameUI();
    }

    IEnumerator ShowCredits()
    {
        Debug.Log("credits");
       // yield return new WaitForSeconds(0.75f);
        yield return StartCoroutine(UIRoot.DarkenScreen());
        yield return StartCoroutine(UIRoot.StartCreditScroll());
        yield return StartCoroutine(UIRoot.LightenScreen());
        UIRoot.SetMenuUI();
    }

    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            ExitGame();
        if (Input.GetKeyDown(KeyCode.Space))
            if (Application.loadedLevel + 1 < Application.levelCount)
                Application.LoadLevel(Application.loadedLevel + 1);
            else
            {
                LaunchCredits();
            }

        if( currentLanguage != previousLanguage )
        {
            UpdateTextTranslaters();
            previousLanguage = currentLanguage;
        }

        if( endPoint != null && questAccompleted && player != null )
        {
            float dist = Vector3.Distance(player.transform.position, endPoint.transform.position);
            if( dist < 2 )
            {
                Debug.Log("Near tree");
                if (Application.loadedLevel + 1 < Application.levelCount)
                    Application.LoadLevel(Application.loadedLevel + 1);
                else
                {
                    LaunchCredits();
                }
            }
        }
    }
    public void SwitchLanguage( bool more)
    {
        //Debug.Log("switchingLanguage");
        switchingLanguage = true;
        if (more )
        currentLanguage = (int)currentLanguage < Enum.GetNames(typeof(Languages)).Length - 1 ? currentLanguage + 1 : 0; 
        else
            currentLanguage = (int)currentLanguage >= 1 ? (currentLanguage - 1) : (Languages)(Enum.GetNames(typeof(Languages)).Length - 1);
        StartCoroutine(Wait());
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds( 0.1f ) ;
        switchingLanguage = false;
    }

    private void UpdateTextTranslaters()
    {
        if (textsRoot != null )
        foreach (GameObject go in textsRoot)
            foreach (LanguageTranslaterScript ts in go.GetComponentsInChildren<LanguageTranslaterScript>(true) )
            {
                ts.Init();
            }
    }
}
