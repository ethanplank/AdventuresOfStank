using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool isStuned;
    

    private void Start()
    {
        isStuned = false;
    }
    public void Stun()
    {
        //if (isSword)
        //{
            isStuned = true;
            Invoke("resetStun", 2);
        //}
    }
        

    private void resetStun()
    {
        if (isStuned)
        {
            isStuned = false;
        }
    }

}


