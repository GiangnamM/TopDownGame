using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwordAttack : MonoBehaviour
{
    public GameObject healthText;

    public float damage = 35f;

    public float knockbackForce = 500f;

    Collider2D swordCoillider;

    Vector2 attackkOffset;

    private void Start()
    {
        swordCoillider = GetComponent<Collider2D>();
        attackkOffset = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();

      
            // knock back 

            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction;

            direction = (Vector2)(-parentPosition + collision.gameObject.transform.position).normalized;
                        
            Vector2 knockback = direction * knockbackForce;

            enemy.Hit();
            Debug.Log("Enemy was Hit");
            if (enemy != null)
            {
                enemy.OnHit(damage, knockback);
            }

        }

    }

}
