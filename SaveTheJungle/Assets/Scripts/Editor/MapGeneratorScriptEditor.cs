using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGeneratorScript)), CanEditMultipleObjects]
public class MapGeneratorScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Create map"))
        {
            foreach (UnityEngine.Object obj in targets)
            {
                MapGeneratorScript mapGenerator = (MapGeneratorScript)obj;
                mapGenerator.Init();
                mapGenerator.CreateBaseMap();
            }
        }
        if (GUILayout.Button("Clear map"))
        {
            foreach (UnityEngine.Object obj in targets)
            {
                MapGeneratorScript mapGenerator = (MapGeneratorScript)obj;
                mapGenerator.ClearMap();
            }
        }
    }
}
