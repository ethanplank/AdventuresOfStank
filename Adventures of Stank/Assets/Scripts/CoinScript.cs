using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public MSM msm;
    // Start is called before the first frame update
    void Start()
    {
        msm = FindObjectOfType<MSM>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    
        
    
        
    
        if (collision.gameObject.tag == "PlayerBox")
        {
           // msm.addGem();
            //Destroy(gameObject);
        }
    }
}
