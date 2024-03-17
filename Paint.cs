using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour, IInteractable, IRestartable
{
    [SerializeField] private GameObject description;

    public void Interact()
    {
        description.SetActive(true);
    }

    public void Restart()
    {
        description.SetActive(false);
    }
}
