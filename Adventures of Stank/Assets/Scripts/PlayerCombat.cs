using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float swordRange = 0.4f;
    public LayerMask enemyLayer;
    public MSM msm;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
            
        }
    }
    void Attack()
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
