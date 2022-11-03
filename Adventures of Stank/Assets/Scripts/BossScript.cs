using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 100;
    public MSM msm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
