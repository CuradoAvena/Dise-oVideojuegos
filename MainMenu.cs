using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [SerializeField]  private GameObject comfirmationPrompt =null;
    [SerializeField] private float defaultVolume = 1.0f;


    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Se cerro el juego");
    
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SetVolume(float volume)
    { 
       AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    { 
      PlayerPrefs.SetFloat("masterVolume",AudioListener.volume);
        StartCoroutine(ComfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio") {

            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
           
        }
    }
    public IEnumerator ComfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }
}