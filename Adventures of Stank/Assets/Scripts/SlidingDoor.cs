using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("SlideBack");
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.Play("DoorSlide");
        
    }
    
}
