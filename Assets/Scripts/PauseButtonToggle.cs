using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour{
    public Sprite pauseIcon;
    public Sprite playIcon;

    private Image image;
    public float lastSpeed = 1f;
    public static bool isPaused = false;

    void Start(){
        image = GetComponent<Image>();
    }

    void Update(){
        // P -> pause / unpause
        if (Input.GetKeyDown(KeyCode.P)){
            TogglePause();
        }
    }


    public void TogglePause(){
       // Debug.Log("AAAA");
        if (Time.timeScale == 0f){
            isPaused = false;
            Time.timeScale = lastSpeed;
            image.sprite = pauseIcon;
        }
        else{
            isPaused = true;
            lastSpeed = Time.timeScale;
            Time.timeScale = 0f;
            image.sprite = playIcon;
        }
    }
}
