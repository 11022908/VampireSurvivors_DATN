using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //public bool healthPlayer;
    [SerializeField] public float maxHealth;
    float currentHealth;
    public HealthBar healthBar;

    public UnityEvent OnDeath;
    public event Action<float> OnHealthChanged;
    private void OnEnable()
    {
        OnDeath.AddListener(Death);
    }
    private void OnDisable()
    {
        OnDeath.RemoveAllListeners();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath.Invoke();
        }
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if(OnHealthChanged != null)
        {
            OnHealthChanged(currentHealth);
        }
    }
    public void Healt(float damage)
    {
        currentHealth += damage;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    public void PlayerDie()
    {
        InLevelManager.Instance.LoseGame();
    }
}
