using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempName : MonoBehaviour
{
    public static TempName instance;

    public string playerName;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        DontDestroyOnLoad(instance);
    }
}
