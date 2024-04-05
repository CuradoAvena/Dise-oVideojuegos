using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private void Start()
    {
  
    }


    [SerializeField] private Transform container;
    private Image[] hearts;


    public void UpdateHealth(HealtSystem hp)
    {
        SetHearts();
        HideHearts();

        var totalHearts = Mathf.Min(hearts.Length, hp.Health / 10);
        for (int i = 0; i < totalHearts; i++)
        {
            hearts[i].color = Color.red;
        }
    }

    private void HideHearts() {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].color = Color.white;
    }

    private void SetHearts() {
        if (hearts != null) return;
        hearts = new Image[container.childCount];
        for (int i = 0; i < container.childCount; ++i)
        {
            hearts[i] = container.GetChild(i).GetComponent<Image>();
        }
    }
}