using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleSingleton : MonoBehaviour
{
    public static CollectibleSingleton Instance;
    public Text text_Name;
    public Text text_Description;
    public Image Image_Collect;
  
    public Canvas collectibleCanvas;

    private CollectibleData currentData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Open(CollectibleData data)
    {
            text_Name.text = data.itemName;
        text_Description.text = data.itemDescription;
        Image_Collect.sprite = data.itemSprite;

        if (collectibleCanvas != null)
        {
            collectibleCanvas.gameObject.SetActive(true);
        }
        currentData = data;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    /*
     quieres ocultarlo por boton o por tiempo?
        por boton
    no hay cursor
     */
    public void CloseMenu() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        collectibleCanvas.gameObject.SetActive(false);
    }

}
