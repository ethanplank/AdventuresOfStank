using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public GameObject bulletPrefab;


    public PlayerScript player;
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
     //   print(PlayerPrefs.GetInt("Hearts"));
        changeHearts();
        gemText.text = "= " + gems.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("Hearts", 6);
            UnityEditor.EditorApplication.isPlaying = false;

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            shootLaser();
        }



        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.P) && numHearts<5 && gems>=15){
                gems -= 15;
                numHearts+=2;
                changeHearts();
            }
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
        changeHearts();
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
    private void changeHearts()
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
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("Hearts", 6);
            UnityEditor.EditorApplication.isPlaying = false;

        }
    }
    public void purchase()
    {
        if (numHearts<3 && gems>=15)
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

    public void shootLaser()
    {
        Vector2 shotVector;
        if (player.direction == "north")
        {
            shotVector = new Vector2(0, 3);
        }
        else if (player.direction == "south")
        {
            shotVector = new Vector2(0, -3);
        }
        else if (player.direction == "west")
        {
            shotVector = new Vector2(-3, 0);
        }
        else if (player.direction == "east")
        {
            shotVector = new Vector2(3, 0);
        }
        else if (player.direction == "northwest")
        {
            shotVector = new Vector2(-3, 3);
        }
        else if (player.direction == "northeast")
        {
            shotVector = new Vector2(3, 3);
        }
        else if (player.direction == "southwest")
        {
            shotVector = new Vector2(-3, -3);
        }
        else if (player.direction == "southeast")
        {
            shotVector = new Vector2(3, -3);
        }
        else
        {
            shotVector = new Vector2(3, 0);
        }
        GameObject bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(1/2, 0) , Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shotVector;
    }

    public void addGem()
    {
        gems++;
    }
}
