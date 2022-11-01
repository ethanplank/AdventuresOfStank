using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float swordRange = 0.5f;
    public LayerMask enemyLayer;
    public MSM msm;
    private float swordDelay;
    // Update is called once per frame
    private void Start()
    {
        swordDelay = Time.time;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && Time.time> swordDelay+1)
        {
            sAttack();
            swordDelay = Time.time;
          //  print("x");

        }
    }
    private void FixedUpdate()
    {
        
    }
    public void sAttack()
    {
        //Attack Animation
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, swordRange, enemyLayer);
        //Enemy damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<RobotScript>().TakeDamage(10);
            
        }
    }
    
}
