using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 100;
    public int speedx = 0;
    public int speedy = -2;
    public MSM msm;
    public int time = 0;
    private Rigidbody2D rb;
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
            speedy = -2;
        }
        else if (collision.gameObject.tag == "BossBumpNE")
        {
            speedy = 0;
            speedx = -2;
        }
        else if (collision.gameObject.tag == "BossBumpSE")
        {
            speedy = 2;
            speedx = 0;
        }
        else if (collision.gameObject.tag == "BossBumpSW")
        {
            speedy = 0;
            speedx = 2;
        }
    }

public void TakeDamage(int damage)
    {
        print("taking damage");
        health -= damage;
        if (health == 0)
        {

            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
