using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour
{
    CircleCollider2D circleCollider;
    public GameObject flamesParticles;
    public GameObject smoke;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();

        Invoke("Explode", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Explode()
    {
        smoke.SetActive(false);
        circleCollider.enabled = true;
        flamesParticles.SetActive(true);
        Invoke("destroyGrenade", .5f);
    }
    private void destroyGrenade()
    {
        Destroy(gameObject);
    }
}
