using UnityEngine;

public class GameSpeed : MonoBehaviour{

    private float recentSpeed = 1f; // for resumption

    public void SetSpeed(float speed){ // Custom Speeds
        recentSpeed = speed;
        Time.timeScale = speed;
    }

    public void Pause(){ // No movement (paused)
        if (Time.timeScale > 0f){
            recentSpeed = Time.timeScale;
        }

        Time.timeScale = 0f;
    }

    public void Resume(){  // Back to whatever it was before
        Time.timeScale = recentSpeed;
    }
}