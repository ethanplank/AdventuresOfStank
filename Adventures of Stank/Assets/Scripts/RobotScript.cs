using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    public MSM msm;
    Transform _transform;
    public float travelSpeed = 3;
    Rigidbody2D _rbody;
    int distance =8;
    public int health;
    public EnemyScript ES;
    
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _transform = transform;
        health = 10;
        msm = FindObjectOfType<MSM>();
        ES = FindObjectOfType<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        Vector3 speed = new Vector3(PlayerPos.x - _transform.position.x, PlayerPos.y - _transform.position.y, 0);
        if ((speed.magnitude > .2f && speed.magnitude < distance) && !ES.isStuned)
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
        
        if (collision.gameObject.tag == "grenade")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            msm.takeDamage(1);
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
        if (isSword)
        {
            toStun();
        }
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        StartCoroutine(HitAnim());
        
    }
    public void toStun()
    {
        ES.Stun();
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
        msm.RobotDeathNoise();
        Destroy(gameObject);
    }
    private void RobotDie()
    {

    }
}
