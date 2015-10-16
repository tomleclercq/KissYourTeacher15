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
    [HideInInspector]
    public bool switchingLanguage = false;
    public JsonData jsonLanguage;
    public Languages currentLanguage;
    public GameObject[] textsRoot;
    private Languages previousLanguage;
    public UIScript UIRoot;
    [HideInInspector]
    public string animalName;

    void Start ()
    {
        string file = File.ReadAllText( getDataFolder()+"Languages.json");
        jsonLanguage = JsonMapper.ToObject(file);
        if( UIRoot != null )
            UIRoot.SetMenuUI();

        UpdateTextTranslaters();
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
        yield return new WaitForSeconds(0.75f);
        yield return StartCoroutine(UIRoot.DarkenScreen());
        yield return StartCoroutine(UIRoot.StartCreditScroll());
        yield return StartCoroutine(UIRoot.LightenScreen());
        UIRoot.SetMenuUI();
    }

    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            ExitGame();

        if( currentLanguage != previousLanguage )
        {
            UpdateTextTranslaters();
            previousLanguage = currentLanguage;
        }
        if (Input.GetKeyDown( KeyCode.L ))
            SwitchLanguage( true );
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
