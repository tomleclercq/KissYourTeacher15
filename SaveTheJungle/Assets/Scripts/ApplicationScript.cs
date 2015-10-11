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

    public GameObject Menu;
    public GameObject Book;
    public GameObject UI;

    void Start ()
    {
        string file = File.ReadAllText( getDataFolder()+"Languages.json");
        jsonLanguage = JsonMapper.ToObject(file);

        UpdateTextTranslaters();
        previousLanguage = currentLanguage;

        Menu.SetActive(true);
        Book.SetActive(false);
        UI.SetActive(false);
        //Time.timeScale = 0;
    }

    public void LaunchGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.75f);
        Menu.SetActive(false);
        Book.SetActive(false);
        UI.SetActive(true);
        Time.timeScale = 1;

        yield return null;
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if( currentLanguage != previousLanguage )
        {
            UpdateTextTranslaters();
            previousLanguage = currentLanguage;
        }
    }

    public void SwitchLanguage( bool more)
    {
        Debug.Log("switchingLanguage");
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
            foreach (LanguageTranslaterScript ts in go.GetComponentsInChildren<LanguageTranslaterScript>())
            {
                ts.Init();
            }
    }
}
