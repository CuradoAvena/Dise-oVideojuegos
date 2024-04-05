using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private UnityEvent OnMenuDisplay, OnMenuHide;

    public void DisplayUI(GameObject ui) {
        ui.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        OnMenuDisplay?.Invoke();
    }
    public void HideUI(GameObject ui) {
        ui.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        OnMenuHide?.Invoke();
    }

    public void CargarEscena(int escenaACargar)
    {
        SceneManager.LoadScene(escenaACargar);
    }
}
