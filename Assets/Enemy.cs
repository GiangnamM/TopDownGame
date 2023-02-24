using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1;

    public float moveSpeed = 500f;

    Animator m_animator;
    Rigidbody2D m_rb;

    public DetectionZone detectionZone;

    public GameObject healthText;

    public float Health
    {
        set
        {
            if (value < health)
            {
                //Text Health

                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            health = value;
            if (health <=0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        if (detectionZone.detectedObjs.Count > 0)
        {
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;
            //Move towards detected Object;
            m_rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }

    public float health = 100;
    
   public void OnHit(float damege, Vector2 knockback)
    {
        Health -= damege;
         
        m_rb.AddForce(knockback);
        
    }
    public void Defeated()
    {
        m_animator.SetTrigger("Defeated");
    }

    public void Hit()
    {
        m_animator.SetTrigger("Hit");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    
}
