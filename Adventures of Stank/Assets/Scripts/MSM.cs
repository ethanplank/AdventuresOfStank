using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MSM : MonoBehaviour
{
    public Sprite halfHeart;
    public Sprite oneheart;
    public Sprite oneandhalfheart;
    public Sprite twoheart;
    public Sprite twoandhalfheart;
    public Sprite threeheart;

    public Text gemText;
    public int gems;
    public Image currentHeartPic; 
    public int scene;

    public double numHearts;
    // Start is called before the first frame update
    void Start()
    {
        gemText.text = "= 0";
        numHearts = 3;
        currentHeartPic.sprite = threeheart;
        print(numHearts);
    }

    // Update is called once per frame
    void Update()
    {
      //  print(numHearts);
        changeHearts(numHearts);
        gemText.text = "= " + gems.ToString();
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
    public void PullSword()
    {
        //Enter code for animation of pulling sword out here.
        //Then add sword to item slot
    }
    private void changeHearts(double numHearts)
    {
        if (numHearts == 3)
        {
            currentHeartPic.sprite = threeheart;
        }
        else if (numHearts == 2.5)
        {
            currentHeartPic.sprite = twoandhalfheart;

        }
        else if (numHearts == 2)
        {
            currentHeartPic.sprite = twoheart;

        }
        else if (numHearts == 1.5)
        {
            currentHeartPic.sprite = oneandhalfheart;
        }
        else if (numHearts == 1)
        {
            currentHeartPic.sprite = oneheart;
        }
        else
        {
            currentHeartPic.sprite = halfHeart;
        }
    }
    public void takeDamage(double damage)
    {
        if (numHearts >= .5)
        {
            numHearts -= .5;
        }
        else
        {
             UnityEditor.EditorApplication.isPlaying = false;

        }
        print(numHearts);
    }
    public void addGem()
    {
        gems++;
    }
}
