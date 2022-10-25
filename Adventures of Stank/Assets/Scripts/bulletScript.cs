using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public MSM manager;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Robot")
        {
            Destroy(gameObject);
        }
    }
    public void fire(Vector2 direction)
    {
        print("Here!");
        rb.velocity = new Vector2(2, 0);
    }
}
