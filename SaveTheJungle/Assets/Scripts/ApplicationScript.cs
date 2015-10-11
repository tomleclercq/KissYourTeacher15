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
    [HideInInspector]
    public string animalName;

    void Start ()
    {
        string file = File.ReadAllText( getDataFolder()+"Languages.json");
        jsonLanguage = JsonMapper.ToObject(file);

        UpdateTextTranslaters();
        previousLanguage = currentLanguage;

        if (Menu != null) Menu.SetActive(true);
        if (Book != null) Book.SetActive(false);
        if (UI != null)UI.SetActive(false);
    }

    public void LaunchGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.75f);
        if( Menu != null )Menu.SetActive(false);
        if (Book != null) Book.SetActive(false);
        if (UI != null) UI.SetActive(true);
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
        /*if( Input.GetKeyDown( KeyCode.Z ) )
        {
            Debug.Log("Add to collection");
            Book.GetComponent<BookScript>().AddNewCollection("zebra");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Add to collection");
            Book.GetComponent<BookScript>().AddNewCollection("lion");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Add to collection");
            Book.GetComponent<BookScript>().AddNewCollection("snake");
        }*/
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
            foreach (LanguageTranslaterScript ts in go.GetComponentsInChildren<LanguageTranslaterScript>())
            {
                ts.Init();
            }
    }
}
