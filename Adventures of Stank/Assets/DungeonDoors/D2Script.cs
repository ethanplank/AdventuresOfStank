using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2Script : MonoBehaviour
{
    [SerializeField] Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            anim.Play("D2SlideClose");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            anim.Play("D2SlideOpen");
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
