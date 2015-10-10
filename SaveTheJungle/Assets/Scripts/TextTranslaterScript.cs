using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;

public class TextTranslaterScript : MonoBehaviour
{
    public string jsonKey;
    TextMesh displayTextMesh;
    Text displayText;

    public void Init()
    {
        JsonData languageData = ApplicationScript.current.jsonLanguage[(int)ApplicationScript.current.currentLanguage]["Data"];
        displayText = GetComponent<Text>();
        if (displayText != null )
            displayText.text = (string)JsonUtil.loadJsonData<string>(languageData, jsonKey);
        else
        {
            displayTextMesh = GetComponent<TextMesh>();
            if (displayTextMesh != null)
                displayTextMesh.text = (string)JsonUtil.loadJsonData<string>(languageData, jsonKey);
        }
    }

}
