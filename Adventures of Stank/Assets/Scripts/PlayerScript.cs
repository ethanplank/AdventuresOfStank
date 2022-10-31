using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
   public MSM msm;
    int speed = 5;
    public Animator animate;

    public string direction;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        checkSkin();
        _rbody.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, Input.GetAxis("Vertical")*speed);
        checkDirection();
        changeSkin();

    }
    private void FixedUpdate()
    {
        
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
        if(collision.gameObject.tag == "SwordDoor")
        {
            msm.hitSwordDoor();
        }
        if(collision.gameObject.tag == "SwordStone")
        {
            msm.PullSword();            
        }
        if (collision.gameObject.tag == "ShopDoor")
        {
            msm.openShop();
        }
        if (collision.gameObject.gameObject.tag == "ShopTable")
        {
            msm.purchase();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            msm.addGem();
            Destroy(collision.gameObject);
        }
    }
    public void checkDirection()
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
            direction = "still";
        }
    }
    private void changeSkin()
    {
        if (direction=="west")
        {
            animate.SetInteger("moveX", -1);
                }
        else if (direction=="east")
        {
            animate.SetInteger("moveX", 1);
        }
        else if ((direction == "north" || direction == "northeast" || direction == "northwest"))
        {
            animate.SetInteger("moveY", 1);
        }
        else if (direction== "south" || direction=="southwest" || direction== "southeast")
        {
            animate.SetInteger("moveY", -1);

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
       //     gameObject.GetComponent<SpriteRenderer>().sprite = ShootLaser;

        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = SwingSword;

        }
        else
        {
            animate.SetInteger("moveX", 0);
            animate.SetInteger("moveY", 0);


        }
    }
}
