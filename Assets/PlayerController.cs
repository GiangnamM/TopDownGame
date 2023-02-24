using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Ins;

    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    public GameObject healthText;

    private bool canMove= true;

    Vector2 movementInput;

    SpriteRenderer m_spriteRenderer;

    Rigidbody2D m_rb;

    Animator m_animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Collider2D m_Col;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_Col = GetComponent<Collider2D>();

        
    }

  
    private void FixedUpdate()
    {
        if (canMove)
        {


            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                m_animator.SetFloat("Speed", movementInput.sqrMagnitude);
                m_animator.SetFloat("Horizontal", movementInput.x);
                m_animator.SetFloat("Vertical", movementInput.y);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }


            }
            else if (movementInput == Vector2.zero)
            {
                m_animator.SetFloat("Speed", movementInput.sqrMagnitude);
            }
        }

    }

    private bool TryMove(Vector2 direction)
    {
        int count = m_rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            m_rb.MovePosition(m_rb.position +  direction * moveSpeed * Time.fixedDeltaTime);
            return true; 
        } else
        {
            return false;
        }
    }

    
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    void OnFire()
    {
        m_animator.SetTrigger("AttackDown");
    }
    public void LockMovement()
    {
        canMove = false;
    }
    public void UnLockMovement()
    {
        canMove = true;
    }

    public void Die()
    {
        m_animator.SetTrigger("Die");
        m_Col.enabled = false;              
    }
    public void OnHit(Vector2 knockback)
    {
        RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        textTransform.SetParent(canvas.transform);

        m_rb.AddForce(knockback,ForceMode2D.Impulse);
    }

 

}
