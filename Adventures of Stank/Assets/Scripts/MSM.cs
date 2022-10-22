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
    private int gems;

    public Image currentHeartPic; 
    public int scene;

    public int numHearts;

    private int hasSword;
    // Start is called before the first frame update
    void Start()
    {
        if ((SceneManager.GetActiveScene().buildIndex == 0))
        {
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("HasSword", 0);
            PlayerPrefs.SetInt("Hearts", 6);
        }
       
        loadData();
    }
    
    // Update is called once per frame
    void Update()
    {
        print(PlayerPrefs.GetInt("Hearts"));
        changeHearts(numHearts);
        gemText.text = "= " + gems.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("Hearts", 6);
            UnityEditor.EditorApplication.isPlaying = false;

        }
    }
    private void saveData()
    {
        PlayerPrefs.SetInt("Gems", gems);
        PlayerPrefs.SetInt("Hearts", numHearts);
        PlayerPrefs.SetInt("HasSword", hasSword);
    }
    private void loadData()
    {
        gems = PlayerPrefs.GetInt("Gems");
        numHearts = PlayerPrefs.GetInt("Hearts");
        hasSword = PlayerPrefs.GetInt("HasSword");
        changeHearts(numHearts);
        gemText.text = "= " + gems.ToString();
    }

    public void hitDoor()
    {
        saveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        loadData();
    }
    public void hitSwordDoor()
    {
        saveData();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(1);
        }
        loadData();
    }
    public void PullSword()
    {
        //Enter code for animation of pulling sword out here.
        //Then add sword to item slot
    }
    private void changeHearts(int numHearts)
    {
        if (numHearts == 6)
        {
            currentHeartPic.sprite = threeheart;
        }
        else if (numHearts == 5)
        {
            currentHeartPic.sprite = twoandhalfheart;

        }
        else if (numHearts == 4)
        {
            currentHeartPic.sprite = twoheart;

        }
        else if (numHearts == 3)
        {
            currentHeartPic.sprite = oneandhalfheart;
        }
        else if (numHearts == 2)
        {
            currentHeartPic.sprite = oneheart;
        }
        else
        {
            currentHeartPic.sprite = halfHeart;
        }
        
    }
    public void takeDamage(int damage)
    {
        if (numHearts >= 1)
        {
            numHearts -= 1;
        }
        else
        {
             UnityEditor.EditorApplication.isPlaying = false;

        }
    }
    public void purchase()
    {
        if (Input.GetKeyDown(KeyCode.P) && numHearts<3 && gems>=15)
        {
            numHearts++;
            gems -= 15;
        }

    }
    public void openShop()
    {
        saveData();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(3);
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(1);
        }
        loadData();
    }
    public void addGem()
    {
        gems++;
    }
}
