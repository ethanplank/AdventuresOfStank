using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
   public MSM msm;
    int speed = 5;
    public Animator animate;

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
           

        } else if (direction == "southeast")
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
        else if (Input.GetKeyDown(KeyCode.X))

        {
            animate.Play(sword);

            //     gameObject.GetComponent<SpriteRenderer>().sprite = ShootLaser;

        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
        }
        else
        {
            animate.Play(idle);
           


        }
    }
}
