using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sound : MonoBehaviour
{
    private static GameObject instance;

    public static AudioClip arrow, fireShoot, fireHit, cannonShoot, zap, mageShoot;
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

        arrow = Resources.Load<AudioClip>("arrow");
        fireShoot = Resources.Load<AudioClip>("fireShoot");
        fireHit = Resources.Load<AudioClip>("fireHit");
        cannonShoot = Resources.Load<AudioClip>("cannonShoot");
        zap = Resources.Load<AudioClip>("zap");
        mageShoot = Resources.Load<AudioClip>("mageShoot");

        audiosrcm = GetComponent<AudioSource>();

        Debug.Log(audiosrcm);

        audiosrcm.volume = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>().volume;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        if (clip == "arrow")
        {
            audiosrcm.PlayOneShot(arrow);
        }
        else if (clip == "fireShoot")
        {
            audiosrcm.PlayOneShot(fireShoot);
        }
        else if (clip == "fireHit")
        {
            audiosrcm.PlayOneShot(fireHit);
        }
        else if (clip == "cannonShoot")
        {
            audiosrcm.PlayOneShot(cannonShoot);
        }
        else if (clip == "zap")
        {
            audiosrcm.PlayOneShot(zap);
        }
        else if (clip == "mageShoot")
        {
            audiosrcm.PlayOneShot(mageShoot);
        }
        else
        {
            Debug.Log("FAILED TO PLAY CLIP WITH NAME: " + clip);
        }
    }

    public static void CancelSound()
    {
        audiosrcm.Stop();
    }

    public static void ChangeVolume(float vol) //not needed?
    {
        Debug.Log(audiosrcm);
        audiosrcm.volume = vol;
    }
}
