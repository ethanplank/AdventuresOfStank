using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MSM : MonoBehaviour
{
    public Sprite zeroHeart;
    public Sprite halfHeart;
    public Sprite oneheart;
    public Sprite oneandhalfheart;
    public Sprite twoheart;
    public Sprite twoandhalfheart;
    public Sprite threeheart;

    public GameObject bulletPrefab;
    public GameObject pauseScreenFade;
    public GameObject pauseText;
    public GameObject grenadePrefab;
    public PlayerScript player;
    public Text gemText;
    private int gems;

    public Text grenadeText;
    public Text partText;
    private float partCount = 5;
    public Text blueText;
    public Text purpText;
    public Text yellowText;
    public Text caveText;
    public Text shopSignText;
    public Text gunSignText;
    public Text potionSignText;
    public Image currentHeartPic;
    public Image isSword;
    public Image isGun;
    public int scene;

    public int numHearts;

    private int cooldown = 1;
    private int grenadeCount;
    private float timeStamp;
    private float swordDelay;
    public int hasSword;
    public int hasGun;
    private Inventory inventory;

    private int smallPotion;
    private int speedPotion;
    private int invincibilityPotion;
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;

    Camera _mainCamera;

    private AudioSource audiosource;
    public AudioClip robotDeath;
    public AudioClip heartGain;
    public AudioClip buyStuff;
    public AudioClip laserShot;
    public AudioClip getHurt;
    public AudioClip useSword;
    public AudioClip GetPart;
    public AudioClip drinkPotion;
    public AudioClip getGem;
    public AudioClip grenadeSound;
    public AudioClip hissFuse;
    public AudioClip shopSound;
    [SerializeField] private UI_Inventory UI_Inventory;
    // Start is called before the first frame update
    void Start()
    {
        
        _mainCamera = Camera.main;

        if (PlayerPrefs.HasKey("grenadeCount"))
        {
            grenadeCount = PlayerPrefs.GetInt("grenadeCount");
        }
        else
        {
            grenadeCount = 0;
            PlayerPrefs.SetInt("grenadeCount", 0);
        }
        if (PlayerPrefs.HasKey("PartsLeft"))
        {
            partCount = PlayerPrefs.GetFloat("PartsLeft");
        }
        else
        {
            PlayerPrefs.SetFloat("PartsLeft", 5);
        }
        if (PlayerPrefs.HasKey("smallPotion"))
        {
            smallPotion = PlayerPrefs.GetInt("smallPotion");
        }
        else
        {
            smallPotion = 0;
            PlayerPrefs.SetInt("smallPotion", 0);
        }
        if (PlayerPrefs.HasKey("speedPotion"))
        {
            speedPotion = PlayerPrefs.GetInt("speedPotion");
        }
        else
        {
            speedPotion = 0;
            PlayerPrefs.SetInt("speedPotion", 0);
        }
        if (PlayerPrefs.HasKey("invincibilityPotion"))
        {
            invincibilityPotion = PlayerPrefs.GetInt("invincibilityPotion");
        }
        else
        {
            invincibilityPotion = 0;
            PlayerPrefs.SetInt("invincibilityPotion", 0);
        }
        timeStamp = Time.time;//Sword cooldown
        inventory = new Inventory();
        audiosource = gameObject.GetComponent<AudioSource>();

        hasSword = 0;
        hasGun = 0;//Refreshing variables

        loadData();
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            UI_Inventory.SetInventory(inventory);//Resetting inventory, spawn

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
                player.gameObject.transform.position = new Vector3(-6, -9, 0);

            }
            if (PlayerPrefs.GetInt("Spawn") == 3)
            {
                player.gameObject.transform.position = new Vector3(3, -18, 0);

            }
            if (PlayerPrefs.GetInt("Spawn") == 4)
            {
                player.gameObject.transform.position = new Vector3(51, -19, 0);

            }
            if (PlayerPrefs.GetInt("Spawn") == 5){
                player.gameObject.transform.position = new Vector3(2, -25, 0);

            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            partCount = 5;
        }
        partCount = PlayerPrefs.GetFloat("PartsLeft");
        blueText.text = smallPotion.ToString();
        purpText.text = speedPotion.ToString();
        yellowText.text = invincibilityPotion.ToString();
        grenadeText.text = grenadeCount.ToString();
        if (!part1)
        {
            partCount--;
        }
        if (!part2)
        {
            partCount--;
        }
        if (!part3)
        {
            partCount--;
        }
        if (!part4)
        {
            partCount--;
        }
        if (!part5)
        {
            partCount--;
        }
        partText.text = partCount.ToString();
        
        //TODO
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetMouseButtonDown(0) && grenadeCount > 0)
            {
                Invoke("playGrenadeSound", 2);
                audiosource.PlayOneShot(hissFuse);

                //Need to save it via playerPrefs.
                Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

                Instantiate(grenadePrefab, new Vector3(p.x, p.y, 0), Quaternion.identity);
                grenadeCount--;
                PlayerPrefs.SetInt("grenadeCount", grenadeCount);

                // Instantiate(grenadePrefab, Camera.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) && smallPotion>0)
            {
                audiosource.PlayOneShot(drinkPotion);

                player.makeSmall();
                smallPotion--;
                PlayerPrefs.SetInt("smallPotion", smallPotion);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && speedPotion>0)
            {
                audiosource.PlayOneShot(drinkPotion);

                player.addSpeed();
                speedPotion--;
                PlayerPrefs.SetInt("speedPotion", speedPotion);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && invincibilityPotion>0)
            {
                audiosource.PlayOneShot(drinkPotion);

                player.makeInvincible();
                invincibilityPotion--;
                PlayerPrefs.SetInt("invincibilityPotion", invincibilityPotion);
            }

        }
       
        if (Input.GetKeyDown(KeyCode.Escape))//Quit
        {
            wipeData();
           // UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();//CHANGEME uncomment FOR BUILD
        }
        if (Input.GetKeyDown(KeyCode.P))//Pause
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
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > timeStamp + cooldown && hasGun == 1)
        {
            //shootLaser();
            //player.shootGun();
           // timeStamp = Time.time;
        }
        //Changing scenes if all parts collected
        if (PlayerPrefs.GetInt("Part1") == 0 && PlayerPrefs.GetInt("Part2") == 0
            && PlayerPrefs.GetInt("Part3") == 0 && PlayerPrefs.GetInt("Part4") == 0
            && PlayerPrefs.GetInt("Part5") == 0)
        {
            SceneManager.LoadScene(6);
        }
        if (SceneManager.GetActiveScene().buildIndex == 8)//Item store
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && gems >= 15)
            {
                smallPotion++;
                gems -= 15;
                PlayerPrefs.SetInt("gems", gems);
                audiosource.PlayOneShot(buyStuff);
                PlayerPrefs.SetInt("smallPotion", smallPotion);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && gems >= 15)
            {
                speedPotion++;
                gems -= 15; 
               audiosource.PlayOneShot(buyStuff);
                PlayerPrefs.SetInt("gems", gems);
                PlayerPrefs.SetInt("speedPotion", speedPotion);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && gems >= 15)
            {
                invincibilityPotion++;
                audiosource.PlayOneShot(buyStuff);
                gems -= 15;
                PlayerPrefs.SetInt("gems", gems);
                PlayerPrefs.SetInt("invincibilityPotion", invincibilityPotion);
            }
        }


        if (SceneManager.GetActiveScene().buildIndex == 3)//Item store
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && numHearts < 5 && gems >= 15)
            {
                gems -= 15;
                numHearts += 2;
                audiosource.PlayOneShot(heartGain);
                changeHearts();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && numHearts < 5 && gems >=25)
            {
                gems -= 25;
                numHearts =6;
                audiosource.PlayOneShot(heartGain);
                changeHearts();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)&& gems >= 20)
            {
                grenadeCount++;
                PlayerPrefs.SetInt("grenadeCount", grenadeCount);
                gems -= 20;
                audiosource.PlayOneShot(buyStuff);

            }
        }
    }
    private void wipeData()
    {
        PlayerPrefs.SetInt("Gems", 0);
        PlayerPrefs.SetInt("Hearts", 6);
        PlayerPrefs.SetInt("HasSword", 0);
        PlayerPrefs.SetInt("HasGun", 0);
        PlayerPrefs.SetInt("grenadeCount", 0);
        PlayerPrefs.SetInt("invincibilityPotion", 0);
        PlayerPrefs.SetInt("speedPotion", 0);
        PlayerPrefs.SetInt("smallPotion", 0);
        PlayerPrefs.SetFloat("PartsLeft", 5);

    }
    private void saveData()
    {
        PlayerPrefs.SetInt("Gems", gems);
        PlayerPrefs.SetInt("Hearts", numHearts);
        PlayerPrefs.SetInt("HasSword", hasSword);
        PlayerPrefs.SetInt("HasGun", hasGun);
        PlayerPrefs.SetInt("grenadeCount", grenadeCount);
        PlayerPrefs.SetFloat("PartsLeft", partCount);
    }
    private void loadData()
    {
        gems = PlayerPrefs.GetInt("Gems");
        numHearts = PlayerPrefs.GetInt("Hearts");
        hasSword = PlayerPrefs.GetInt("HasSword");
        hasGun = PlayerPrefs.GetInt("HasGun");
        grenadeCount = PlayerPrefs.GetInt("grenadeCount");
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
    public void decrementParts()
    {
        partCount-=.5f;
        PlayerPrefs.SetFloat("PartsLeft", partCount);
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
        if (SceneManager.GetActiveScene().buildIndex == 2)
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
    //TODO: pull gun animation
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
        else if (numHearts == 1)
        {
            currentHeartPic.sprite = halfHeart;
        }
        else
        {
            currentHeartPic.sprite = zeroHeart;

        }

    }

    public void takeDamage(int damage)
    {
        audiosource.PlayOneShot(getHurt);
        if (numHearts >= 1)
        {
            numHearts -= 1;
            player.getHit();
        }
        else
        {
            PlayerPrefs.SetInt("Spawn", 0);
            PlayerPrefs.SetInt("Gems", 0);
            PlayerPrefs.SetInt("Hearts", 6);
            SceneManager.LoadScene(4);

        }
    }

    public void purchase()
    {
        if (numHearts < 3 && gems >= 15)
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
    public void openPotions()
    {
        PlayerPrefs.SetInt("Spawn", 4);//CHANGEME
        saveData();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(8);
        }
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            SceneManager.LoadScene(1);
        }
        loadData();
    }
    public void openDungeon()
    {
        PlayerPrefs.SetInt("Spawn", 5);
        saveData();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(10);
        }
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            SceneManager.LoadScene(1);
        }
        loadData();
    }
    public void shootLaser()
    {
        audiosource.PlayOneShot(laserShot);

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
            bullet = Instantiate(bulletPrefab, player.GetComponent<Rigidbody2D>().position + new Vector2(1, 1), Quaternion.identity);

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
    private void playGrenadeSound()
    {
        audiosource.PlayOneShot(grenadeSound);
    }
    public void displaySignText()
    {
        caveText.gameObject.SetActive(true);
    }
    public void displayGunSignText()
    {
        gunSignText.gameObject.SetActive(true);
    }
    public void GetPartSound()
    {
        audiosource.PlayOneShot(GetPart);

    }
    public void hideSignText()
    {
        caveText.gameObject.SetActive(false);
    }
    public void hidePotionSignText()
    {
        potionSignText.gameObject.SetActive(false);
    }
    public void hideShopSignText()
    {
        shopSignText.gameObject.SetActive(false);


    }
    public void hideGunSignText()
    {
        gunSignText.gameObject.SetActive(false);


    }
    public void RobotDeathNoise()
    {
        Invoke("RobotDie", 1f);
    }
    private void RobotDie()
    {
        //audiosource.PlayOneShot(robotDeath);
    }
    public void showShopSignText()
    {
        shopSignText.gameObject.SetActive( true); 
    }
    public void showPotionSignText()
    {
        potionSignText.gameObject.SetActive( true);
    }
}
