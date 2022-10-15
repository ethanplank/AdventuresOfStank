using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rbody;
   public MSM msm;
    int speed = 5;
    public Sprite SwordSkank;
    public bool sSkin;

    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        checkSkin();
    }

    // Update is called once per frame
    void Update()
    {
        _rbody.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, Input.GetAxis("Vertical")*speed);
    }
    private void checkSkin()
    {
        if (sSkin == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = SwordSkank;
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
            this.gameObject.GetComponent<SpriteRenderer>().sprite = SwordSkank;
            sSkin = true;
        }
        if (collision.gameObject.tag == "Gem")
        {
            msm.addGem();
            Destroy(collision.gameObject);
        }
    }
}
