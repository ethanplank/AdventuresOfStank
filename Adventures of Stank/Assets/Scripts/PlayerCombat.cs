using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float swordRange = 0.5f;
    public LayerMask enemyLayer;
    public MSM msm;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            sAttack();
            print("x");

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
