using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    int damage = 10;
    [SerializeField] private HealtSystem health;

    private void OnTriggerEnter(Collider col)
    {
        var collectible = col.GetComponent<Collectionable>();
        if (col.gameObject.tag == "Enemy")
        {
            health.EditHealth(-damage);
        }
        if (collectible!=null) {
            collectible.Collect();
        }
    }
}
