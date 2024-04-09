using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    #region UNITY METHODS
    private void Awake()
    {
        EditHealth(MaxHealth);
    }

    #endregion


    #region VARIABLES
    public int Health { get; private set; } = 10;
    [SerializeField]    private int MaxHealth=100;

    public UnityEvent OnDie, OnHealthChange;
    #endregion

    #region PUBLIC METHODS
    public void EditHealth(int amount)
    { 
      Health = Mathf.Clamp(Health+amount,0,MaxHealth);
        OnHealthChange?.Invoke();
        if (Health <= 0)   Die();
    }

    public void Restart() {

        EditHealth(MaxHealth);
    
    }
    #endregion

    private void Die() { OnDie?.Invoke(); }

}
