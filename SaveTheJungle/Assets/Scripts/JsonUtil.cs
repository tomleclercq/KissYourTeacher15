using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;

public class JsonUtil : MonoBehaviour
{
    public static object loadJsonData<T>(JsonData jsonInput, string key, GameObject sender = null)
    {
        object result = null;
        try
        {
            if (jsonInput[key] != null)
            {
                if (typeof(T) == typeof(string)) result = (string)jsonInput[key];
                else if (typeof(T) == typeof(int)) result = (int)jsonInput[key];
                else if (typeof(T) == typeof(bool)) result = (bool)jsonInput[key];
                else if (typeof(T) == typeof(float)) result = (float)(double)jsonInput[key];
                else if (typeof(T) == typeof(JsonData)) result = (JsonData)jsonInput[key];
            }
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("Missing JSON parameter : " + key + (sender == null ? "" : (" on " + sender.name)));
        }
        return result;
    }
}
