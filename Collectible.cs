using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Collectibles
{
    public class Collectible : MonoBehaviour
    {
        public CollectibleType collectibleType;

        // Acceder a la información del coleccionable

        // Método para establecer la información del coleccionable

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CollectibleManager.Instance.Collect(this);

            }
        }

        //private void OnTriggerEnter (Collectible collectible)
        //{
        //    // Acceder al CollectibleSingleton e invocar el método Open
        //    CollectibleSingleton.Instance.Open(CollectibleData);
        //}
    }
}
