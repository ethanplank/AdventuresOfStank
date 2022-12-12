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


    const string sword = "StankSword";
    const string gunRight = "StankGunRight";
    const string gunLeft = "StankGunLeft";
    const string gunForward = "StankGunForward";

    const string pullsword = "StankSwordPull 0";


    public string direction;
    private float weaponCooldown;

    Boolean isAttacking = false;
   
    Boolean idle = true;

    public GameObject StankTrigger;
    public Transform attackPoint;
    int x;
    int y;
    public float swordRange = 1.0f;
    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {


        _rbody = GetComponent<Rigidbody2D>();
        weaponCooldown = Time.time;
        
        
    }

    //// Update is called once per frame
    void Update()
    {
        SetAttackPoint();
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && Time.time > weaponCooldown + 1
            && msm.hasSword == 1)
        {

            sAttack();

        }
        if (Input.GetKeyDown(KeyCode.V) && !isAttacking && Time.time > weaponCooldown + 1 && msm.hasGun == 1)
        {
            shootGun();
        }
        if (!isAttacking)
        {
            _rbody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
            checkDirection();
            changeSkin();
            
            // print(direction + " , " + idle);
        }//testing
        if(!isAttacking && _rbody.velocity.x==0 && _rbody.velocity.y==0)
        {
            animate.speed = 0;
        }
        }

    public void sAttack()
    {
        animate.speed = 0;
        _rbody.velocity = new Vector2(0, 0);
        animate.Play(sword);
        animate.speed = 1;
        Invoke("TurnOffSword", 1);
        Invoke("StopAnim", 1);
        isAttacking = true;
        msm.playSwordSound();
        weaponCooldown = Time.time;
        //Attack Animation
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, swordRange, enemyLayer);
        
        //Enemy damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<RobotScript>().TakeDamage(10);

        }

    }
    private void StopAnim()
    {
        animate.speed = 0;
    }
    public void shootGun()
    {
        animate.speed = 0;
        _rbody.velocity = new Vector2(0, 0);
        animate.speed = 1;
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
        isAttacking = true;
        Invoke("TurnOffGun", 1);
        msm.shootLaser();
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
                pullSwordAnimation();
                msm.PullSword();
            }
            msm.hasSword = 1;
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
            x = 1;
            y = 0;
            idle = false;
        }
        else if (_rbody.velocity.x > 0 && _rbody.velocity.y > 0)
        {
            direction = "northeast";
            x = 1;
            y = 1;
            idle = false;
        }
        else if (_rbody.velocity.x > 0 && _rbody.velocity.y < 0)
        {
            direction = "southeast";
            x = 1;
            y = -1;
            idle = false;

        }
        else if (_rbody.velocity.x < 0 && _rbody.velocity.y > 0)
        {
            direction = "northwest";
            x = -1;
            y = 1;
            idle = false;

        }
        else if (_rbody.velocity.x < 0 && _rbody.velocity.y < 0)
        {
            direction = "southwest";
            x = -1;
            y = -1;
            idle = false;


        }
        else if (_rbody.velocity.x < 0 && _rbody.velocity.y == 0)
        {
            direction = "west";
            x = -1;
            y = 0;
            idle = false;


        }
        else if (_rbody.velocity.x == 0 && _rbody.velocity.y > 0)
        {
            direction = "north";
            y = 2;
            x = 0;
            idle = false;


        }
        else if (_rbody.velocity.x == 0 && _rbody.velocity.y < 0)
        {
            direction = "south";
            y = -2;
            x = 0;
            idle = false;


        }
        else if(_rbody.velocity.x == 0 && _rbody.velocity.y == 0)
        {
            idle = true;
        }
    }
    private void changeSkin()//Changing the animation based on direction
    {
        if (direction == "west")
        {
            animate.speed = 1;
            
            animate.Play(west);
        }
        else if (direction == "east")
        {
            animate.speed = 1;

            animate.Play(east);

        }
        else if ((direction == "north"))
        {
            animate.speed = 1;

            animate.Play(north);

        }
        else if (direction == "south")
        {
            animate.speed = 1;

            animate.Play(south);


        }
        else if (direction == "southeast")
        {
            animate.speed = 1;

            animate.Play(southeast);


        }
        else if (direction == "southwest")
        {
            animate.speed = 1;

            animate.Play(southwest);

        }
        else if (direction == "northeast")
        {
            animate.speed = 1;

            animate.Play(northeast);

        }
        else if (direction == "northwest")
        {
            animate.speed = 1;

            animate.Play(northwest);

        }else if (idle)
        {
            animate.speed = 0;
        }

    }
   
    public void getHit()
    {
        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, .5f);

        StankTrigger.SetActive(false);
        Invoke("TurnOnCollider", 1);
        Invoke("ColorChange", 1);
    }
    private void TurnOnCollider()
    {
        StankTrigger.SetActive(true);
    }
    private void ColorChange()
    {
        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);

    }
    private void TurnOffSword()
    {
        isAttacking = false;
    }
    private void TurnOffGun()
    {
        isAttacking = false;
    }
    public void pullSwordAnimation()
    {
        animate.speed = 1;
        
        isAttacking = true;
        animate.Play(pullsword);
        

        Invoke("StopAnim",4);
        
        Invoke("TurnOffSword", 4);
        

    }
  
    void SetAttackPoint()
    {
        attackPoint.transform.position = attackPoint.parent.TransformPoint(x, y, 0);
    }
    
}
