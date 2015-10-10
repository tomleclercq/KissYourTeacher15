using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;
using System.IO;

public class LanguageTranslaterScript : MonoBehaviour
{
    bool speachWord;
    public string jsonKey;
    TextMesh displayTextMesh;
    Text displayText;
    AudioSource audioEmitter;

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
        audioEmitter = GetComponent<AudioSource>();
        if (audioEmitter != null)
        {
            string fileName = ApplicationScript.getDataFolder()+"audio/"+jsonKey+"_"+(int)ApplicationScript.current.currentLanguage+".wav";
            StartCoroutine(LoadAudioFile(fileName));
        }
    }

    IEnumerator LoadAudioFile(string path)
    {
        WWW www = new WWW("file://" + path);
        //print("loading " + path);

        AudioClip clip =  www.GetAudioClip(true);
        while (clip.loadState == AudioDataLoadState.Loading)
            yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            audioEmitter.clip = www.GetAudioClip(true);
            clip.name = Path.GetFileName(path);
        }
        else
            Debug.LogWarning(www.error);
    }

    void Update()
    {
        if (speachWord) SpeachWord();
    }

    public void SpeachWord()
    {
        StartCoroutine( PlaySound() );
    }

    private IEnumerator PlaySound()
    {
		if (audioEmitter != null && audioEmitter.clip != null) {
			while ((audioEmitter.clip.loadState != AudioDataLoadState.Loaded) && (audioEmitter.clip.name == "")){
				yield return null;
			}

			audioEmitter.PlayOneShot (audioEmitter.clip);
			speachWord = false;
		}
    }
}
