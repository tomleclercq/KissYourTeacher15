using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
using System.IO;

public class LanguageTranslaterScript : MonoBehaviour
{
    public bool onBookData;
    public string jsonKey;
    TextMesh displayTextMesh;
    Text displayText;
    AudioSource audioEmitter;

    public void Init()
    {
        JsonData languageData;
        if( onBookData )
        {
            languageData = ApplicationScript.current.jsonLanguage["Animals"][0][ApplicationScript.current.animalName + "Data"][(int)ApplicationScript.current.currentLanguage]["Data"];
        }
        else
            languageData = ApplicationScript.current.jsonLanguage["DataPerLanguage"][(int)ApplicationScript.current.currentLanguage]["Data"];

        displayText = GetComponent<Text>();
        if (displayText != null )
            displayText.text = (string)JsonUtil.loadJsonData<string>(languageData, jsonKey);
        else
        {
            displayTextMesh = GetComponent<TextMesh>();
            if (displayTextMesh != null)
                displayTextMesh.text = (string)JsonUtil.loadJsonData<string>(languageData, jsonKey);
        }
        if(!onBookData)
            StartCoroutine(LoadSound());
    }

    private IEnumerator LoadSound()
    {
        if (audioEmitter == null) audioEmitter = GetComponentInChildren<AudioSource>();
        if (audioEmitter != null)
        {
            while (audioEmitter.isPlaying)
                yield return null;

            string fileName = ApplicationScript.getDataFolder() + "audio/" + jsonKey + "_" + (int)ApplicationScript.current.currentLanguage + ".wav";
            WWW www = new WWW("file://" + fileName);

            AudioClip clip =  www.GetAudioClip(false);
            while (clip.loadState == AudioDataLoadState.Loading)
                yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                audioEmitter.clip = www.GetAudioClip(false);
                clip.name = Path.GetFileName(fileName);
            }
            else
                Debug.LogWarning(www.error);
        }
    }

    public void PlaySound()
    {
        StartCoroutine(PlaySoundE());
    }

    public IEnumerator PlaySoundE()
    {
        while (ApplicationScript.current.switchingLanguage || audioEmitter.clip.loadState == AudioDataLoadState.Loading )// (audioEmitter == null ) || (audioEmitter.clip == null ) || string.IsNullOrEmpty(audioEmitter.clip.name) )
            yield return null;
       // Debug.Log("Play sound " + );
        audioEmitter.PlayOneShot(audioEmitter.clip);
        //Debug.Log("Play sound "+audioEmitter.isPlaying);

    }

}
