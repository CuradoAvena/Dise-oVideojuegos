using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    public GameObject lantern, onLantern, offLantern;
    public float maxBattery = 100f;
    private float currentBattery;
    public float batteryDrainRate = 5f;
    public Image batteryImage; // Referencia al objeto de UI para la bater�a
    public Sprite batteryFull, battery90, battery50, battery10; // Im�genes de la bater�a
    private bool isLanternOn = false;

    void Start()
    {
        lantern.SetActive(false);
        onLantern.SetActive(false);
        offLantern.SetActive(true);
        currentBattery = maxBattery; // Inicia con bater�a llena
        UpdateBatteryImage();
    }

    void Update()
    {
        // Alternar linterna con "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isLanternOn)
            {
                TurnOffLantern();
            }
            else if (currentBattery > 0) // Solo encender si hay bater�a
            {
                TurnOnLantern();
            }
        }

        // Consumir bater�a si la linterna est� encendida
        if (isLanternOn)
        {
            currentBattery -= batteryDrainRate * Time.deltaTime;
            if (currentBattery <= 0)
            {
                currentBattery = 0;
                TurnOffLantern();
            }
            UpdateBatteryImage();
        }
    }

    private void TurnOnLantern()
    {
        isLanternOn = true;
        lantern.SetActive(true);
        onLantern.SetActive(true);
        offLantern.SetActive(false);
    }

    private void TurnOffLantern()
    {
        isLanternOn = false;
        lantern.SetActive(false);
        onLantern.SetActive(false);
        offLantern.SetActive(true);
    }

    private void UpdateBatteryImage()
    {
        if (batteryImage != null)
        {
            if (currentBattery > 75)
            {
                batteryImage.sprite = batteryFull;
            }
            else if (currentBattery > 50)
            {
                batteryImage.sprite = battery90;
            }
            else if (currentBattery > 25)
            {
                batteryImage.sprite = battery50;
            }
            else
            {
                batteryImage.sprite = battery10;
            }
        }
    }

    // Recargar bater�a
    public void RechargeBattery(float amount)
    {
        currentBattery = Mathf.Min(currentBattery + amount, maxBattery);
        UpdateBatteryImage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery")) // Aseg�rate de que las pilas tengan esta etiqueta
        {
            RechargeBattery(20f); // Recarga un 20% de la bater�a
            Destroy(other.gameObject); // Elimina la pila
        }
    }
}
