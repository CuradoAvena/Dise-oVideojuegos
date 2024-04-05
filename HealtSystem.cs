using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealtSystem : MonoBehaviour, IDamageable
{
    #region UNITY METHODS
       private void Start()
       {
        //EditHealth(MaxHealth);
          LoadHealth();
       }
    #endregion

    #region VARIABLES
    public int Health{ private set; get; } =10;

    [SerializeField] private int MaxHealth = 100;

    public UnityEvent OnDie, OnHealthChange;
    #endregion

    #region PUBLIC METHODS
    public void TakeDamage(int damage)
    {
        EditHealth(-damage);
    }
    public void LoadHealth()
    {

        Health = 0;
        var savedFile = PlayerSave.Instance.GetObject<HealthSave>("health.json");
        EditHealth(savedFile.health);

        OnHealthChange?.Invoke();
    }
    public void EditHealth(int amount)
    {
        Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
        OnHealthChange?.Invoke();
        if (Health <= 0) Die();
    }

    public void Restart()
    {
        EditHealth(MaxHealth);
    }
    #endregion

    private void Die()
    {
        OnDie?.Invoke();
    }
}
