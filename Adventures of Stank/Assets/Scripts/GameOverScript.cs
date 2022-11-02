using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Gems", 0);
        PlayerPrefs.SetInt("HasSword", 0);
        PlayerPrefs.SetInt("Hearts", 6);
        PlayerPrefs.SetInt("HasGun", 0);
        PlayerPrefs.SetInt("Spawn", 0);
        PlayerPrefs.SetInt("Part1", 1);
        PlayerPrefs.SetInt("Part2", 1);
        PlayerPrefs.SetInt("Part3", 1);
        PlayerPrefs.SetInt("Part4", 1);
        PlayerPrefs.SetInt("Part5", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.SetInt("Gems", 0);
                PlayerPrefs.SetInt("HasSword", 0);
                PlayerPrefs.SetInt("Hearts", 6);
                SceneManager.LoadScene(0);
            }

        }
        
            
           // UnityEditor.EditorApplication.isPlaying = false;

        
    }
}
