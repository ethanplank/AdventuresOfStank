using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour
{
    CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 1);
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Explode()
    {
        circleCollider.enabled = true;
        Invoke("destroyGrenade", 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
    private void destroyGrenade()
    {
        Destroy(gameObject);
    }
}
