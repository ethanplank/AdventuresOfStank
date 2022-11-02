using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("HasSword", 0);
            PlayerPrefs.SetInt("Hearts", 6);
            UnityEditor.EditorApplication.isPlaying = false;

        }
    }
}
