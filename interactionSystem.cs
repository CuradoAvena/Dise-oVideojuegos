using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var _interactable = other.GetComponent<IInteractable>();
        if (_interactable == null) return;
        instruction.SetActive(true);
        interactable = _interactable;
    }
    private void OnTriggerExit(Collider other)
    {
        var _interactable = other.GetComponent<IInteractable>();
        if (_interactable == null) return;
        instruction.SetActive(false);
        other.GetComponent<IRestartable>().Restart();
        interactable = null;
    }

    private void Update()
    {
        if (interactable == null) return;
        if (Input.GetKeyDown(KeyCode.R)) { 
            interactable.Interact();
            instruction.SetActive(false);
        }
    }

    private IInteractable interactable;
    [SerializeField] private GameObject instruction;
}
