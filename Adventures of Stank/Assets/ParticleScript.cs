using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public MSM msm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            msm.takeDamage(5);
        }
    }
    public void bossDead()
    {
        Destroy(gameObject);
    }
}
