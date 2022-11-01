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

    public int hasSword;
    private Inventory inventory;
    [SerializeField] private UI_Inventory UI_Inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory();
        
        if ((SceneManager.GetActiveScene().buildIndex == 0))
        {
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("HasSword", 0);
            PlayerPrefs.SetInt("Hearts", 6);
        }
       
        loadData();
        UI_Inventory.SetInventory(inventory);
    }
    
    // Update is called once per frame
    void Update()
    {
        print(hasSword);
     //   print(PlayerPrefs.GetInt("Hearts"));
        changeHearts();
        gemText.text = "= " + gems.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("HasSword", 0);
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
        if (hasSword == 1)
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.LaserSword, amount = 1 });
        }
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
        inventory.AddItem(new Item { itemType = Item.ItemType.LaserSword, amount = 1 });
        hasSword = 1;
        saveData();
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
        int bulletSpeed = 7;
        GameObject bullet;
        //GameObject bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(1, 0), Quaternion.identity);
        shotVector = new Vector2(bulletSpeed, 0);
        if (player.direction == "north")
        {
             bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(0, 1), Quaternion.identity);
           
            shotVector = new Vector2(0, bulletSpeed);
            bullet.GetComponent<Rigidbody2D>().rotation = 90;

        }
        else if (player.direction == "south")
        {
             bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(0, -1), Quaternion.identity);

            shotVector = new Vector2(0, -bulletSpeed);
            bullet.GetComponent<Rigidbody2D>().rotation = 90;

        }
        else if (player.direction == "west")
        {
             bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(-1, 0), Quaternion.identity);

            shotVector = new Vector2(-bulletSpeed, 0);
        }
        else if (player.direction == "east")
        {
             bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(1, 0), Quaternion.identity);

            shotVector = new Vector2(bulletSpeed, 0);
        }
        else if (player.direction == "northwest")
        {
             bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(-1, 1), Quaternion.identity);

            shotVector = new Vector2(-bulletSpeed, bulletSpeed);
            bullet.GetComponent<Rigidbody2D>().rotation = -45;

        }
        else if (player.direction == "northeast")
        {
             bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(1 ,1), Quaternion.identity);

            shotVector = new Vector2(bulletSpeed, bulletSpeed);
            bullet.GetComponent<Rigidbody2D>().rotation = 45;

        }
        else if (player.direction == "southwest")
        {
             bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(-1, -1), Quaternion.identity);

            shotVector = new Vector2(-bulletSpeed, -bulletSpeed);
            bullet.GetComponent<Rigidbody2D>().rotation = 45;

        }
        else if (player.direction == "southeast")
        {
             bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(1, -1), Quaternion.identity);

            shotVector = new Vector2(bulletSpeed, -bulletSpeed);
            bullet.GetComponent<Rigidbody2D>().rotation = -45;

        }
        else
        {
            bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(1, 0), Quaternion.identity);
        }

        bullet.GetComponent<Rigidbody2D>().velocity = shotVector;
    }

    public void addGem()
    {
        gems++;
    }
    
}
