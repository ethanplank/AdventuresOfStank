using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostyScript : MonoBehaviour
{

    public MSM msm;
    Transform _transform;
    const float travelSpeed = 3;
    Rigidbody2D _rbody;
    int distance = 15;
    public int health;
    public EnemyScript ES;
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _transform = transform;
        health = 20;
        msm = FindObjectOfType<MSM>();
        ES = FindObjectOfType<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("PlayerBox").transform.position;

        Vector3 speed = new Vector3(PlayerPos.x - _transform.position.x, PlayerPos.y - _transform.position.y, 0);
        if (speed.magnitude > .2f && speed.magnitude < distance && !ES.isStuned)
        {
            gameObject.GetComponent<AIPath>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<AIPath>().enabled = false;
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
            TakeDamage(5,false);
        }
    }
    public void TakeDamage(int damage, bool isSword)
    {
        if(isSword)
        {
            ES.Stun();
        }
        
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

}
