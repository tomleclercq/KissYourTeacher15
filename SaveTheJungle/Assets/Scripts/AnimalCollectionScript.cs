using UnityEngine;
using System.Collections;

public class AnimalCollectionScript : MonoBehaviour
{
    static private AnimalCollectionScript collection;
    static public AnimalCollectionScript current
    {
        get
        {
            if (collection == null) collection = GameObject.Find("/AnimalCollection").GetComponent<AnimalCollectionScript>();
            return collection;
        }
    }

    public LanguageTranslaterScript[] animalCollection;

    void Start()
    {
        foreach (LanguageTranslaterScript ts in animalCollection)
            ts.Init();
    }

}
