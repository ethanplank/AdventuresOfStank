using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public MSM msm;
    Transform _transform;
    public float travelSpeed;
    Rigidbody2D _rbody;
    int distance;
    public int health;
 


    void FixedUpdate()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        Vector3 speed = new Vector3(PlayerPos.x - _transform.position.x, PlayerPos.y - _transform.position.y, 0);
        if (speed.magnitude > .2f && speed.magnitude < distance)
        {
            _rbody.velocity = travelSpeed * speed.normalized;
        }
        else
        {
            _rbody.velocity = Vector3.zero;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            msm.takeDamage(1);
        }
        if (collision.gameObject.tag == "grenade")
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(5);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
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


