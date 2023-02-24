using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{ 

    PlayerController m_player;

    public float knockbackForce = 200f;
    public float m_MaxHealth = 5;
    private float m_curentHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_player = FindObjectOfType<PlayerController>();
        m_curentHealth = m_MaxHealth;
    }
   
    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.transform.tag == "Enemy")
        {
                     

            m_curentHealth -= 1;
            Debug.Log("Player was Hit");

            // knockback
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction;

            direction = (Vector2)(parentPosition - col.gameObject.transform.position).normalized;

            Vector2 knockback = direction * knockbackForce;
            m_player.LockMovement();
            m_player.OnHit(knockback);
            Debug.Log("player knockback:" + knockback);
            
            m_player.UnLockMovement();
           
        }
        if (m_curentHealth <= 0)
        {
            Debug.Log("Player Death");           
            m_player.Die();
            m_player.LockMovement();
        }
    }

    



}
