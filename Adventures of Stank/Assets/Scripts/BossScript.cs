using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 100;
    public int speedx = 4;
    public int speedy = 3;
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
        speedx = -speedx;
        speedy = -speedy;
        if (collision.gameObject.tag == "Player")
        {
            msm.takeDamage(1);

        }
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(5);
        }

    }
    public void TakeDamage(int damage)
    {
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
