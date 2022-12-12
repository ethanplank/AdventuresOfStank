using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("SlideUp");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.Play("SlideDown");
    }

}
