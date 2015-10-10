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

    public JsonData jsonLanguage;
    public Languages currentLanguage;

    public GameObject textsRoot;

    private Languages previousLanguage;

    void Start ()
    {
        string file = File.ReadAllText( getDataFolder()+"Languages.json");
        JsonData json = JsonMapper.ToObject(file);
        jsonLanguage = (JsonData)JsonUtil.loadJsonData<JsonData>(json, "DataPerLanguage");

        UpdateTextTranslaters();
        previousLanguage = currentLanguage;
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
        if (more )
        currentLanguage = (int)currentLanguage < Enum.GetNames(typeof(Languages)).Length - 1 ? currentLanguage + 1 : 0; 
        else
            currentLanguage = (int)currentLanguage >= 1 ? (currentLanguage - 1) : (Languages)(Enum.GetNames(typeof(Languages)).Length - 1);
    }

    private void UpdateTextTranslaters()
    {
        if (textsRoot != null )
            foreach (LanguageTranslaterScript ts in textsRoot.GetComponentsInChildren<LanguageTranslaterScript>())
            {
                ts.Init();
            }
    }

}
