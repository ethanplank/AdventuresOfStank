using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class MSM : MonoBehaviour
{
    public Sprite halfHeart;
    public Sprite oneheart;
    public Sprite oneandhalfheart;
    public Sprite twoheart;
    public Sprite twoandhalfheart;
    public Sprite threeheart;

    public GameObject bulletPrefab;
    public GameObject pauseScreenFade;
    public GameObject pauseText;

    public PlayerScript player;
    public Text gemText;
    private int gems;

    public Image currentHeartPic;
    public Image isSword;
    public Image isGun;
    public int scene;

    public int numHearts;

    private int cooldown =1;
    private float timeStamp;
    private float swordDelay;
    public int hasSword;
    public int hasGun;
    private Inventory inventory;

    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;

    private AudioSource audiosource;
    public AudioClip heartGain;
    public AudioClip laserShot;
    public AudioClip getHurt;
    public AudioClip useSword;
    public AudioClip getGem;
    [SerializeField] private UI_Inventory UI_Inventory;
    // Start is called before the first frame update
    void Start()
    {
        timeStamp = Time.time;//Sword cooldown
        inventory = new Inventory();
        audiosource = gameObject.GetComponent<AudioSource>();
       
        hasSword = 0;
        hasGun = 0;//Refreshing variables
       
        loadData();
        UI_Inventory.SetInventory(inventory);//Resetting inventory, spawn
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.GetInt("Spawn") == 0)
            {
                player.gameObject.transform.position = new Vector3(-4, -1, 0);
            }
            if (PlayerPrefs.GetInt("Spawn") == 1)
            {
                player.gameObject.transform.position = new Vector3(1, 2, 0);

            }
            if (PlayerPrefs.GetInt("Spawn") == 2)
            {
                player.gameObject.transform.position = new Vector3(2, -9, 0);

            }
            if (PlayerPrefs.GetInt("Spawn") == 3)
            {
                player.gameObject.transform.position = new Vector3(3, -18, 0);

            }
            
           

        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0)
            {
                pauseScreenFade.SetActive(false);
                pauseText.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseScreenFade.SetActive(true);
                pauseText.SetActive(true);

                Time.timeScale = 0;
            }
        }
        print(PlayerPrefs.GetInt("Part1"));
        if (PlayerPrefs.GetInt("Part1") == 1)
        {
            part1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Part2") == 1)
        {
            part2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Part3") == 1)
        {
            part3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Part4") == 1)
        {
            part4.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Part5") == 1)//Checking if parts active
        {
            part5.SetActive(true);
        }
        changeHearts();
        gemText.text = "= " + gems.ToString();

        
        //Shooting laser
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > timeStamp+ cooldown && hasGun == 1)
        {
            shootLaser();
            player.shootGun();
            audiosource.PlayOneShot(laserShot);
            timeStamp = Time.time;
        }
       //Changing scenes if all parts collected
        if(PlayerPrefs.GetInt("Part1")==0 && PlayerPrefs.GetInt("Part2")==0 
            && PlayerPrefs.GetInt("Part3")==0 && PlayerPrefs.GetInt("Part4") == 0 
            && PlayerPrefs.GetInt("Part5") == 0)
        {
            SceneManager.LoadScene(6);
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)//Purchasing hearts
        {
            if (Input.GetKeyDown(KeyCode.P) && numHearts<5 && gems>=15){
                gems -= 15;
                numHearts+=2;
                audiosource.PlayOneShot(heartGain);
                changeHearts();
            }
        }
    }
    private void saveData()
    {
        PlayerPrefs.SetInt("Gems", gems);
        PlayerPrefs.SetInt("Hearts", numHearts);
        PlayerPrefs.SetInt("HasSword", hasSword);
        PlayerPrefs.SetInt("HasGun", hasGun);
    }
    private void loadData()
    {
        gems = PlayerPrefs.GetInt("Gems");
        numHearts = PlayerPrefs.GetInt("Hearts");
        hasSword = PlayerPrefs.GetInt("HasSword");
        hasGun = PlayerPrefs.GetInt("HasGun");
        changeHearts();
        gemText.text = "= " + gems.ToString();
        if (hasSword == 1)//Checking to see if player has gotten sword
        {
            inventory.AddItem(new Item { itemType = Item.ItemType.LaserSword, amount = 1 });
        }
        if (hasGun == 1)
        {
            inventory.addGun();
        }
        if (hasSword == 1)
        {
            isSword.gameObject.SetActive(true);
        }
        if (hasGun == 1)
        {
            isGun.gameObject.SetActive(true);
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
        PlayerPrefs.SetInt("Spawn", 1);
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
    public void hitGunDoor()
    {
        PlayerPrefs.SetInt("Spawn", 3);
        saveData();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(5);
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene(1);
        }
        
        loadData();
    }
    public void PullSword()
    {
        //Enter code for animation of pulling sword out here.
        //Then add sword to item slot
        inventory.addSword();
        isSword.gameObject.SetActive(true);
        //audiosource.PlayOneShot(useSword);
        hasSword = 1;
        UI_Inventory.SetInventory(inventory);
        saveData();
    }
    public void PullGun()
    {
        inventory.addGun();
        isGun.gameObject.SetActive(true);
        hasGun = 1;
        UI_Inventory.SetInventory(inventory);
        saveData();
    }
    private void changeHearts()
    {
        if (numHearts == 6)
        {
            currentHeartPic.sprite = threeheart;//Changing health bar
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
        audiosource.PlayOneShot(getHurt);
        if (numHearts >= 1)
        {
            numHearts -= 1;
            player.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Invincibility", 1);

        }
        else
        {
            PlayerPrefs.SetInt("Spawn", 0);
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("Hearts", 6);
            SceneManager.LoadScene(4);

        }
    }
    public void Invincibility()
    {
        player.gameObject.GetComponent<BoxCollider2D>().enabled = true;

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
        PlayerPrefs.SetInt("Spawn", 2);
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
        if (player.direction == "north")//Making laser go correct rotation and direction
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
            bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(0, -1), Quaternion.identity);

            shotVector = new Vector2(0, -bulletSpeed);
            bullet.GetComponent<Rigidbody2D>().rotation = 90;
        }

        bullet.GetComponent<Rigidbody2D>().velocity = shotVector;
    }

    public void addGem()
    {
        audiosource.PlayOneShot(getGem);
        gems++;
    }
   
    public void playSwordSound()
    {
        audiosource.PlayOneShot(useSword);
    }
}
