using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable, ITargetable
{
    [Header("Components")]
    public SpriteFlashDamage SpriteFlashDamage;
    public Animator Animator;
    public PathFollower PathFollower;
    public CircleCollider2D Collider;

    [Header("Values")]
    public int MaxHealth = 5;
    public int HealthPerTime = 1;
    public int Health;
    public bool isAlive = true;

    [Header("Upgrades By Time")]
    [SerializeField] private float _currentTimeToIncreaseHealthEnemies = 0;
    [SerializeField] private float _timeToIncreaseHealthEnemies = 5f;

    public bool IsAlive { get => isAlive;}

    private void OnEnable()
    {
        Health = MaxHealth;
        isAlive = true;
        Collider.enabled = true;
        PathFollower.CanMove = true;
        PathFollower.ResetWayPoint();
    }

    public void OnDeath()
    {
        GameManager.OnEnemyKilledHandle.Invoke();
        isAlive = false;
        PathFollower.CanMove = false;
        Collider.enabled = false;
        Animator.SetBool("Death", true);
    }

    public void Deactive()
    {
        Animator.SetBool("Death", false);
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        SpriteFlashDamage.FlashDamage();

        if (Health <= 0)
        {
            OnDeath();
        }
    }

    private void IncreaseHealth()
    {
        MaxHealth += HealthPerTime;
    }

    private void Update()
    {
        _currentTimeToIncreaseHealthEnemies += Time.deltaTime;

        if (_currentTimeToIncreaseHealthEnemies >= _timeToIncreaseHealthEnemies)
        {
            IncreaseHealth();
            _currentTimeToIncreaseHealthEnemies = 0f;
        }
    }
}