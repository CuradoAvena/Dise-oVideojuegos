using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleInterface : MonoBehaviour
{
    public static CollectibleInterface Instance;
    public Text text_Name;
    public Text text_Description;
    public Image Image_collect;

   public Canvas collectibleCanvas;


    private CollectibleData currenteData;

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
    private void Start()
    {
        CloseMenu();
    }

    public void Open(CollectibleData data)
    {
        text_Name.text = data.itemName;
        text_Description.text = data.itemDescription;
        Image_collect.sprite = data.itemSprite;

        if (collectibleCanvas != null)
        {
            collectibleCanvas.gameObject.SetActive(true);
        }
        currenteData = data;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CloseMenu()  {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        collectibleCanvas.gameObject.SetActive(false);
    }
}
