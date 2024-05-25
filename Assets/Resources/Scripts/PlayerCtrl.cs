using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed;

    public float dashBoost = 2f;
    public float dashTime;

    private float _dashTime;
    private bool isDashing = false;
    private Vector3 moveInput;

    public SpriteRenderer characterSR;
    public GameObject ghostEffect;
    public float ghostDelaySecond;

    private Coroutine dashEffectCoroutine;

    private Rigidbody2D rb2d;
    private Animator animator;
    private Transform character;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        character = transform.Find("Character");
        if (character != null)
            animator = character.GetComponent<Animator>();
    }

    
    void Update()
    {
        HandleMoving();
    }
    private void HandleMoving()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        transform.position += moveInput * speed * Time.deltaTime;
        
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
         
        if (Input.GetKeyDown(KeyCode.Space) && _dashTime <= 0 && isDashing == false)
        {
            speed += dashBoost;
            _dashTime = dashTime;
            isDashing = true;
            StartDashEffect();
        }

        if(_dashTime <= 0 && isDashing == true)
        {
            speed -= dashBoost;
            isDashing = false;
            StopDashEffect();
        }
        else
        {
            _dashTime -= Time.deltaTime;
        }

        if(moveInput.x != 0)
        {
           
            if (moveInput.x > 0)
                characterSR.transform.localScale = new Vector3(1f, 1f, 0);
            else
                characterSR.transform.localScale = new Vector3(-1f, 1f, 0);
        }
    }

    void StopDashEffect()
    {
        if (dashEffectCoroutine != null)
        {
            StopCoroutine(dashEffectCoroutine);
        }
    }

    void StartDashEffect()
    {
        if(dashEffectCoroutine != null)
        {
            StopCoroutine(dashEffectCoroutine);
        }
        dashEffectCoroutine = StartCoroutine(DashEffectCoroutine());
    }
    IEnumerator DashEffectCoroutine()
    {
        while (true)
        {
            GameObject ghost = Instantiate(ghostEffect, transform.position, transform.rotation);
            Sprite currentSprite = characterSR.sprite;
            ghost.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite;

            Destroy(ghost, 0.5f);
            yield return new WaitForSeconds(ghostDelaySecond);
        }
    }

    public Health playerHealth;
    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }
    public void Health(float damage)
    {
        playerHealth.Healt(damage);
    }
    public void PlayerDeath()
    {
        animator.SetTrigger("Death");
    }
}
