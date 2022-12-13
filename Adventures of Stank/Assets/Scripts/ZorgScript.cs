using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class ZorgScript : MonoBehaviour
{
    Transform _transform;
    private float distance = 15f;
    public float travelSpeed = 1f;
    private Rigidbody2D _rbody;
    public int health;
    private MSM msm;
    public GameObject robot;
    System.Random _rand = new System.Random();

    private Boolean notLowHealth=true;
    private Boolean isClose = false;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        msm = FindObjectOfType<MSM>();
        _transform = transform;

        health = 100;
        StartCoroutine(HandleSpawning());

    }
    void FixedUpdate()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        Vector3 speed = new Vector3(PlayerPos.x - _transform.position.x, PlayerPos.y - _transform.position.y, 0);
        if (speed.magnitude > .2f && speed.magnitude < distance)
        {
            _rbody.velocity = travelSpeed * speed.normalized;
            isClose = true;
        }
        else
        {
            isClose = false;
            _rbody.velocity = Vector3.zero;
        }

    }
    private void Update()
    {
        if (health < 30)
        {
            notLowHealth = false;
        }

       
       
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
        StartCoroutine(HitAnim());
    }
    IEnumerator HitAnim()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, .2f);
            yield return new WaitForSeconds(.2f);
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(.2f);
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator HandleSpawning()
    {
        while (true)
        {
            if (isClose)
            {
                SpawnRobot();
            }
            if (notLowHealth)
            {
                yield return new WaitForSeconds(5);
            }
            else
            {
                yield return new WaitForSeconds(2);
            }
        }
    }
    void SpawnRobot()
    {

        Instantiate(robot);
        robot.transform.position=new Vector2(_rand.Next(35,50), _rand.Next(-38, -21));
    }
}
