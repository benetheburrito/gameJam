using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform hitbox; 
    public float attackRange = .5f;
    public LayerMask enemyLayers;

    public float attackCooldown;
    private float nextAttackTime;

    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldown;
                Attack();
            }
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack1");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitbox.position, attackRange, enemyLayers);
        List<string> names = new List<string>();
        foreach (Collider2D enemy in hitEnemies)
        {
            
            if(names.Contains(enemy.name) == false)
            {
                names.Add(enemy.name);
                //Debug.Log("hit enemy poggies " + enemy.name);
                enemy.gameObject.GetComponent<MeleeEnemyAI>().getHit(1);
            }
        }
    }
}

