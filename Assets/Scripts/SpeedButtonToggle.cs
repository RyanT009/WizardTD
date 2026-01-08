using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour{
    public Sprite halfIcon;
    public Sprite normalIcon;
    public Sprite doubleIcon;

    [SerializeField] private PauseButton pauseButton; // this is done so basically when you change the speed it doesn't auto-resume

    private Image image;
    private int speed = 1; // where 0 = 0.5x, 1 = 1x, 2 = 2x

    void Start(){
        image = GetComponent<Image>();
        image.sprite = normalIcon;
    }

    void Update(){
        // S -> change game speed
        if (Input.GetKeyDown(KeyCode.S)){
            ChangeSpeed();
        }
    }


    public void ChangeSpeed(){

        speed = speed + 1;

        if (speed == 0){
            if (PauseButton.isPaused)
                pauseButton.lastSpeed = 0.5f; // store the speed for resume
            else
                Time.timeScale = 0.5f; // apply immediately if not paused
            image.sprite = halfIcon;
        }
        else if (speed == 1){
            if (PauseButton.isPaused)
                pauseButton.lastSpeed = 1f; // store the speed for resume
            else
                Time.timeScale = 1f; // apply immediately if not paused
            image.sprite = normalIcon;
        }
        else if (speed == 2){
            if (PauseButton.isPaused)
                pauseButton.lastSpeed = 2f; // store the speed for resume
            else
                Time.timeScale = 2f; // apply immediately if not paused
            image.sprite = doubleIcon;
        }
        if (speed == 3){ // reset the cycle
            speed = 0;
            if (PauseButton.isPaused)
                pauseButton.lastSpeed = 0.5f; // store the speed for resume
            else
                Time.timeScale = 0.5f; // apply immediately if not paused
            image.sprite = halfIcon;
        }

    }
}

