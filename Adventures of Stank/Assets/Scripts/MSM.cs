using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSM : MonoBehaviour
{
    public int scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitDoor()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void hitSwordDoor()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void pullSword()
    {
        //Enter code for animation of pulling sword out here.
        //Then add sword to item slot
    }
}
