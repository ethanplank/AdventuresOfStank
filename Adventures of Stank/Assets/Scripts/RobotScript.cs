using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RobotScript : MonoBehaviour
{
    Transform _transform;
    const float travelSpeed = 3;
    Rigidbody2D _rbody;
    int distance = 5;
    void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void FixedUpdate()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        Vector3 speed = new Vector3(PlayerPos.x - _transform.position.x, PlayerPos.y - _transform.position.y, 0);
        if (speed.magnitude > .2f && speed.magnitude<distance)
        {
            _rbody.velocity = travelSpeed * speed.normalized;
        }
        else
        {
            _rbody.velocity = Vector3.zero;
        }
    }


}
