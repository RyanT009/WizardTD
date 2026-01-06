using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private static GameObject instance;

    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
