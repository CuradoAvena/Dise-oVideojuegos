using Collectibles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate CollectibleManager instance. Destroying the duplicate.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        registry = new Dictionary<CollectibleType, CollectibleRegistry>();
        foreach (var data in collectibles_data) {
            data.Initialize();
            registry.Add(data.type, data);
        }

        SpawnCollectible(CollectibleType.Lata, 1);
        collectibleCounterText.text = "";

    }

    public static CollectibleManager Instance { get; private set; }
    public Text collectibleCounterText;

    public CollectibleRegistry[] collectibles_data;
    private Dictionary<CollectibleType, CollectibleRegistry> registry;

 
    private void SpawnCollectible(CollectibleType type, int count)
    {
        registry[type].Spawn(count);
    }


    public void Collect(Collectible collectible)
    {
        var type = collectible.collectibleType;
        if (isCollectibleRegistered(type))
        {
            registry[type].Add();

            //si aun no llegas al maximo
            if (!registry[type].isCapped())
            {
                //genera otro
                SpawnCollectible(type, 1);
            }

            /*el coleccionable solo conoce su tipo, pero el que almacena la info
            //para su presentacion en interfaz es el REGISTRO que se tiene guardado solo 1 vez*/
            CollectibleInterface.Instance.Open(registry[type].data);

            //destruimos el actual
            //no recomiendo destruir, mejor usar un pool object
            Destroy(collectible.transform.gameObject);
            //collectible.gameObject.SetActive(false);
            UpdateCollectibleCounterText(type);
        }
    }

    private bool isCollectibleRegistered(CollectibleType type)
    {
        return registry.ContainsKey(type);
    }
    private void UpdateCollectibleCounterText(CollectibleType type)
    {
        //collectibleCounterText.text = "Moneda: " + registry[type].amount + "/" + registry[type].MaxAmount;
        collectibleCounterText.text = $"{type}: {registry[type].amount} / {registry[type].MaxAmount}";
    }
}
