using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

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

    public JsonData jsonLanguage;
    public Languages currentLanguage;

    public GameObject textsRoot;

    private Languages previousLanguage;

    void Start ()
    {
        string file = File.ReadAllText(@"D:\Users\Tom\Desktop\KissYourTeach2015\SaveTheJungle\Data\Languages.json");
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

    private void UpdateTextTranslaters()
    {
        foreach (TextTranslaterScript ts in textsRoot.GetComponentsInChildren<TextTranslaterScript>())
        {
            ts.Init();
        }
    }

}
