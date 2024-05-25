using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBoss
{
    summon, healing, shell
}
public class Boss : MonoBehaviour
{
    public TypeBoss type;

    public Health healthBoss;
    public List<GameObject> enemiesSummon;

    public float timeColdDown;
    public int maxCountSummon;
    public int maxCountHealth;

    private float currentHealth;
    private int enemyCountSummon;
    private float duration;

    private void Start()
    {
        duration = timeColdDown;
        healthBoss = GetComponent<Health>();
        currentHealth = healthBoss.GetCurrentHealth();
        healthBoss.OnHealthChanged += OnHealthChanged;
    }
    
    private void OnHealthChanged(float newHealth)
    {
        if(newHealth/currentHealth < 0.5f)
        {
            if(type == TypeBoss.summon)
            {
                SkillSummon();
                Debug.Log("Summon ");
            }
            if(type == TypeBoss.healing)
            {
                SkillHealing();
            }
        }
    }
    private void SkillSummon()
    {
        AudioManager.Instance.PlaySFX("boss_summon");
        StartCoroutine(IESummon(1f));
    }
    IEnumerator IESummon(float time)
    {
        yield return new WaitForSeconds(time);

        while (enemyCountSummon < maxCountSummon)
        {
            SpawnEnemy();

            enemyCountSummon++;
        }
    }
    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemiesSummon.Count);

        GameObject enemyPrefab = enemiesSummon[randomIndex];

        Instantiate(enemyPrefab, transform.position * Random.Range(-2, 2), Quaternion.identity);
    }
    private void SkillHealing()
    {
        AudioManager.Instance.PlaySFX("boss_healing");
        StartCoroutine(IEHealing(1f));
    }
    IEnumerator IEHealing(float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {

            HealingEnemy();

            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
    }
    void HealingEnemy()
    {
        healthBoss.Healt(maxCountHealth / duration);
    }
    private void SkillMakeShell()
    {

    }

}
