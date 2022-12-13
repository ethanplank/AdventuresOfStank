using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public bool isSquid;
    [SerializeField] Animator anim;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            //anim.Play("SlideBack");
            if (isSquid)
            {
                anim.Play("SquidSlide");
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.Play("SquidBack");
        
    }
    public void SquidOpen()
    {
        anim.Play("SquidSlide");
        Destroy(gameObject);

    }
    
}
