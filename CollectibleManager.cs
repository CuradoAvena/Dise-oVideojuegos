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
        // Obtén todos los puntos de aparición de objetos coleccionables
        foreach (Transform child in transform)
        {
            collectibleSpawnPoints.Add(child);
        }

        registry = new Dictionary<CollectibleType, CollectibleRegistry>();
        foreach (var data in collectibles_data)
            registry.Add(data.type, data);

        SpawnCollectible(CollectibleType.Moneda, 1);
        UpdateCollectibleCounterText();
    }

    public static CollectibleManager Instance { get; private set; }
    public GameObject collectiblePrefab;
    public Text collectibleCounterText;

    public List<Transform> collectibleSpawnPoints = new List<Transform>();
    public CollectibleRegistry []collectibles_data;
    private Dictionary<CollectibleType, CollectibleRegistry> registry;

    [Serializable]
    public class CollectibleRegistry {
        public CollectibleType type;
        public int amount = 0;
        public int MaxAmount = 10;
        public CollectibleData data;

        public bool isCapped() { return amount >= MaxAmount; }
        public void Clear() { amount = 0; }
        public void Add(int val = 1) { amount =Mathf.Clamp(amount+val, 0, MaxAmount); }
    }
  

    private void SpawnCollectible(CollectibleType type,int count)
    {

        for (int i = 0; i < count; i++)
        {
            if (collectibleSpawnPoints.Count == 0)
            {
                break; // Detiene la generación si no quedan puntos de aparición
            }

            // Elige un punto de aparición aleatorio
            Transform spawnPoint = GetRandomSpawnPoint();

            GameObject collectible = Instantiate(collectiblePrefab, spawnPoint.position, Quaternion.identity);
            collectible.GetComponent<Collectible>().collectibleType = type;
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        int randomIndex = UnityEngine.Random.Range(0, collectibleSpawnPoints.Count);
        Transform spawnPoint = collectibleSpawnPoints[randomIndex];
        collectibleSpawnPoints.RemoveAt(randomIndex);
        return spawnPoint;
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
            //el coleccionable solo conoce su tipo, pero el que almacena la info para su presentacion en interfaz es el REGISTRO que se tiene guardado solo 1 vez
            CollectibleSingleton.Instance.Open(registry[type].data);
            Debug.Log(collectible.gameObject.name); 
            Debug.Log(collectible.transform.gameObject.name);
            //destruimos el actual
            //no recomiendo destruir, mejor usar un pool object
            Destroy(collectible.transform.gameObject);
            //collectible.gameObject.SetActive(false);
            UpdateCollectibleCounterText();
        }
    }

    private bool isCollectibleRegistered(CollectibleType type) {
        return registry.ContainsKey(type);
    }
    private void UpdateCollectibleCounterText()
    {
        collectibleCounterText.text = "Monedas: " + registry[CollectibleType.Moneda].amount + "/" + registry[CollectibleType.Moneda].MaxAmount;
    }
}

/*
 * hasta este punto todo deberia funcionar igual que la ultma vez, se cuentan las monedas y ya
 * y solo se spawnean monedas
 * ok?
 * ok
 * EXCELENTE
 * 
 * Pos ya quedó, que mas quieres? creo que por con eso, debo estudiar que fue lo que hiciste
 * ...como uso el pool
 * tu dices lo que hice del diccionario? aja
 * 
 * Prestale atencion chavo
 * Un diccionario es como una tabla. Es como un array de objetos de algun tipo. Solo que en lugar de acceder al array con un
 * int para la posición en la que es´ta dentro del array, lo haces con una llave
 * La llave puede ser cualquier tipo, un int, un string, un enum, lo que sea, igual los valores
 * 
 * Supongamos que tenemos un diccionario de armas. Las llaves van a ser el nombre las armas, y los valores su daño de ataque
 *   llaves (id)  |   valores (objetos)
 *   
 *   espada             3 dmg
 *   espada larga       4 dmg
 *   arco               2 dmg
 *   ropera             3 dmg
 *   daga               2 dmg
 *   
 *   como puedes ver, los valores se pueden repetir, pero no las llaves. Entonces que pasa si agregas 2 llaves iguales??
 *   
 *   armas.Add("arco", 120);
 *   armas.Add("arco", 3);
 *   
 *   pues no añade, sino que cambia el valor
 *   entonces si la llave existe, simplemente cambia el valor para esa llave
 *   
 *   La ventaja es que no tienes elementos o llaves duplicadas y que puedes acceder a ellos de manera mas directa a diferencia
 *   de un arreglo
 *   
 *   var rightHandWeapon = armas["daga"];
 *   var leftHandWeapon = arregloDeArmas[3]; 
 *   
 *   y a saber si el espacio 3 si es la daga, no te lo garantiza
 *   
 *   que pasa si la llave no existe? pues te regresa un valor default, pero siempre es buena costumbre checar antes la llave
 *   
 *   if(armas.ContainsKey("Ballesta")){...}
 *   
 *   y asi??
 *   dudas? no chavo
 *   va pos ya quedó entonces, o que mas? por ahora es todo
 *   va, pos suerte chavo, me avias cualquier otro problema, gracias chavo!
 * y ahora como lo oculto?
 
 */