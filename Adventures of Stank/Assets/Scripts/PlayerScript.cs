using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
    public MSM msm;
    int speed = 5;
    public Animator animate;

    //Animation stuff
    public string currentAnimation;
    const string northwest = "StankNorthWest";
    const string southwest = "StankSouthWest";
    const string southeast = "StankSouthEast";
    const string northeast = "StankNorthEast";
    const string north = "StankMoveBack";
    const string south = "RunForward";
    const string east = "StankMoveRight";
    const string west = "StankMoveLeft";
    const string idle = "StankStationary";
    const string sword = "StankSword";
    const string gunRight = "StankGunRight";
    const string gunLeft = "StankGunLeft";
    const string gunForward = "StankGunForward";
    public string direction;

    private float swordCooldown;
    Boolean isSwinging = false;
    Boolean isShooting = false;

    public Transform attackPoint;
    public float swordRange = 1.0f;
    public LayerMask enemyLayer;
    private float swordDelay;
    public bool activeSword;

    // Start is called before the first frame update
    void Start()
    {
        swordDelay = Time.time;
        activeSword = false;
        if (msm.hasSword == 1)
        {
            activeSword = true;
        }

        _rbody = GetComponent<Rigidbody2D>();
        swordCooldown = Time.time;

    }

    //// Update is called once per frame
    void Update()
    {

        checkSkin();
        _rbody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        checkDirection();
        if (Input.GetKeyDown(KeyCode.X) && !isSwinging && Time.time > swordCooldown + 1 && !isShooting
            && Time.time > swordDelay + 1 && msm.hasSword == 1)
        {
            isSwinging = true;
            msm.playSwordSound();
            swordCooldown = Time.time;
            if (activeSword)
            {
                //delay for sword
                sAttack();
                swordDelay = Time.time;
            }

        }
        changeSkin();




    }
    public void sAttack()
    {
        //Attack Animation
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, swordRange, enemyLayer);
        //Enemy damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<RobotScript>().TakeDamage(10);

        }
    }
    private void FixedUpdate()
    {

    }
    public void shootGun()
    {
        isShooting = true;
    }
    private void checkSkin()
    {
        if (msm.hasSword == 1)
        {
            //Put code here for allowing sword animation
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            msm.hitDoor();
        }
        if (collision.gameObject.tag == "SwordDoor")
        {
            msm.hitSwordDoor();
        }
        if (collision.gameObject.tag == "GunDoor")
        {
            msm.hitGunDoor();
        }
        if (collision.gameObject.tag == "SwordStone")
        {

            if (msm.hasSword == 0)
            {
                msm.PullSword();
            }
            activeSword = true;

        }
        if (collision.gameObject.tag == "GunHolster")
        {
            if (msm.hasGun == 0)
            {
                msm.PullGun();
            }

        }
        if (collision.gameObject.tag == "ShopDoor")
        {
            msm.openShop();
        }
        if (collision.gameObject.gameObject.tag == "ShopTable")
        {
            msm.purchase();
        }
        if (collision.gameObject.tag == "Sign")
        {
            msm.displaySignText();
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sign")
        {
            msm.hideSignText();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            msm.addGem();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Part1" || collision.gameObject.tag == "Part2"
            || collision.gameObject.tag == "Part3" || collision.gameObject.tag == "Part4"
            || collision.gameObject.tag == "Part5")
        {
            if (collision.gameObject.tag == "Part1")//removing parts if hit
            {
                PlayerPrefs.SetInt("Part1", 0);
            }
            if (collision.gameObject.tag == "Part2")
            {
                PlayerPrefs.SetInt("Part2", 0);
            }
            if (collision.gameObject.tag == "Part3")
            {
                PlayerPrefs.SetInt("Part3", 0);
            }
            if (collision.gameObject.tag == "Part4")
            {
                PlayerPrefs.SetInt("Part4", 0);
            }
            if (collision.gameObject.tag == "Part5")
            {
                PlayerPrefs.SetInt("Part5", 0);
            }
            Destroy(collision.gameObject);
        }
    }

    public void checkDirection()//Setting players direction for animations
    {
        if (_rbody.velocity.x > 0 && _rbody.velocity.y == 0)
        {
            direction = "east";
        }
        else if (_rbody.velocity.x > 0 && _rbody.velocity.y > 0)
        {
            direction = "northeast";
        }
        else if (_rbody.velocity.x > 0 && _rbody.velocity.y < 0)
        {
            direction = "southeast";
        }
        else if (_rbody.velocity.x < 0 && _rbody.velocity.y > 0)
        {
            direction = "northwest";
        }
        else if (_rbody.velocity.x < 0 && _rbody.velocity.y < 0)
        {
            direction = "southwest";
        }
        else if (_rbody.velocity.x < 0 && _rbody.velocity.y == 0)
        {
            direction = "west";
        }
        else if (_rbody.velocity.x == 0 && _rbody.velocity.y > 0)
        {
            direction = "north";
        }
        else if (_rbody.velocity.x == 0 && _rbody.velocity.y < 0)
        {
            direction = "south";
        }
        else
        {
            direction = direction;
        }
    }
    private void changeSkin()//Changing the animation based on direction
    {
        if (!isSwinging && !isShooting)
        {
            if (direction == "west")
            {
                animate.Play(west);
            }
            else if (direction == "east")
            {
                animate.Play(east);

            }
            else if ((direction == "north"))
            {
                animate.Play(north);

            }
            else if (direction == "south")
            {
                animate.Play(south);


            }
            else if (direction == "southeast")
            {

                animate.Play(southeast);


            }
            else if (direction == "southwest")
            {
                animate.Play(southwest);

            }
            else if (direction == "northeast")
            {
                animate.Play(northeast);

            }
            else if (direction == "northwest")
            {
                animate.Play(northwest);

            }
            else
            {
                animate.Play(idle);
            }
        }
        else if (!isShooting)
        {
            animate.Play(sword);
            Invoke("TurnOffSword", 1);//Sword animation

        }
        else if (!isSwinging)//Gun animation
        {
            if (direction == "south" || direction == "still")
            {
                animate.Play(gunForward);
            }
            else if (direction == "west" || direction == "southwest")
            {
                animate.Play(gunLeft);
            }
            else if (direction == "east" || direction == "southeast")
            {
                animate.Play(gunRight);
            }
            isShooting = true;
            Invoke("TurnOffGun", 1);
        }
    }
    public void OnParticleCollision(GameObject other)
    {
        msm.takeDamage(5);
    }

    private void TurnOffSword()
    {
        isSwinging = false;
    }
    private void TurnOffGun()
    {
        isShooting = false;
    }
}
