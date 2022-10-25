using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RobotScript : MonoBehaviour
{
    public MSM msm;
    Transform _transform;
    const float travelSpeed = 3;
    Rigidbody2D _rbody;
    int distance = 5;
    public int health;
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _transform = transform;
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            msm.takeDamage(1);

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
