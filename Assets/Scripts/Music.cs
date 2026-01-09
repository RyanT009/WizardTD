using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static GameObject instance;
    static AudioSource audiosrcm;

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

        audiosrcm = GetComponent<AudioSource>();

        audiosrcm.volume = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>().volume;


        Invoke("PlaySound", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound()
    {
        audiosrcm.Play();
    }
    public static void StopSound()
    {
        audiosrcm.Stop();
    }
    public static void ChangeVolume(float vol)
    {
        audiosrcm.volume = vol;
    }
}
