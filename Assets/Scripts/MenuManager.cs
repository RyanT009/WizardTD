using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] Settings settings;

    // Start is called before the first frame update
    void Start()
    {
        settings = GameObject.Find("Settings").GetComponent<Settings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
