using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectionable : MonoBehaviour
{
    [SerializeField] private UnityEvent OnCollect;

    public void Collect() {
        OnCollect?.Invoke();
        Destroy(gameObject);
    }
}
