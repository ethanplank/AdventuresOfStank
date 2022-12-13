using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D3Script : MonoBehaviour
{
    [SerializeField] Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            anim.Play("D3SlideClose");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            anim.Play("D3SlideOpen");
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
