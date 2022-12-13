using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 100;
    public int speedx = 0;
    public int speedy = -2;
    public int speedNorm = 2;
    public int fastSpeed = 3;
    public MSM msm;
    public int time = 0;
    private Rigidbody2D rb;
    public SlidingDoor quidy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speedx, speedy);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
   
        if (collision.gameObject.tag == "Player")
        {
            msm.takeDamage(1);

        }
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(5);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BossBumpNW")
        {
            speedx = 0;
            speedy = -speedNorm;
            if (health < 36)
            {
                speedy = -fastSpeed;
            }
        }
        else if (collision.gameObject.tag == "BossBumpNE")
        {
            speedy = 0;
            speedx = -speedNorm;
            if (health < 30)
            {
                speedx = -fastSpeed;
            }
        }
        else if (collision.gameObject.tag == "BossBumpSE")
        {
            speedy = speedNorm;
            speedx = 0;
            if (health < 30)
            {
                speedy = fastSpeed;
            }
        }
        else if (collision.gameObject.tag == "BossBumpSW")
        {
            speedy = 0;
            speedx = speedNorm;
            if (health < 30)
            {
                speedx = fastSpeed;
            }
        }
    }

public void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            quidy.SquidOpen();
            Die();
        }
        StartCoroutine(HitAnim());
    }
    IEnumerator HitAnim()
    {
        //travelSpeed = 0;
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, .2f);
            yield return new WaitForSeconds(.2f);
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(.2f);
        }
        //travelSpeed = 3;
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
