using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private CinemachineTargetGroup targetGroup;

    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int worth = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
        targetGroup = GameObject.Find("Target Group").GetComponent<CinemachineTargetGroup>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;

        targetGroup.RemoveMember(transform);

        PlayerStats.Money += worth;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
