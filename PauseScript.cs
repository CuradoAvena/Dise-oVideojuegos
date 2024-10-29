using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject canvasPause;
    public static bool GameIsPause = false;
    public int escenaACargar;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvasPause.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // ESTO SI FUNCIONA??????
        //si

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                SalirDePausa();
            }

            else
            {
                MandarAPausa();
            }
        }
    }

    public void MandarAPausa()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPause = true;
        canvasPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void SalirDePausa()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPause = false;
        canvasPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void CargarEscena(int escenaACargar)
    {
        SceneManager.LoadScene(escenaACargar);
    }

    public void CloseApp()
    {
        Debug.Log("Se cerró el juego");
        Application.Quit();
    }
}
