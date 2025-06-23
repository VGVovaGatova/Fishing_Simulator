using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] GameObject MenuObject;
    [SerializeField] GameObject SettingObject;
    [SerializeField] GameObject Particle;
    [SerializeField] GameObject Particle2;
    [SerializeField] Slider SliderSpeedCamera;
    bool Debounce = false;
    bool MenuEnabled = false;
    public void continueFuntioin()
    {
        Particle.SetActive(false);
        Particle2.SetActive(false);
        StartCoroutine(DebounceTimer());
        GetComponent<PlayerController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void closeSetting()
    {
        SettingObject.SetActive(false);
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("SpeedCamera"))
        GetComponent<PlayerController>().SpeedCamera = PlayerPrefs.GetFloat("SpeedCamera");
        
    }
    public void Save()
    {
        GetComponent<PlayerController>().SpeedCamera = SliderSpeedCamera.value;
        PlayerPrefs.SetFloat("SpeedCamera",  GetComponent<PlayerController>().SpeedCamera);
    }
    public void OpenSetting()
    {
        SettingObject.SetActive(!SettingObject.activeSelf);
        SliderSpeedCamera.value = GetComponent<PlayerController>().SpeedCamera;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) && !Debounce)
        {
            if (!Debounce)
            {
            if (!MenuObject.activeSelf)
            {
                Particle.SetActive(false);
                Particle2.SetActive(false);
                StartCoroutine(DebounceTimer());
                GetComponent<PlayerController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (MenuObject.activeSelf)
            {
                Particle.SetActive(false);
                Particle2.SetActive(false);
                StartCoroutine(DebounceTimer());
                GetComponent<PlayerController>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            }
        }
    }
    IEnumerator DebounceTimer()
    {
        Particle.SetActive(true);
     //   Particle2.SetActive(true);
        Debounce = true;
        yield return new WaitForSeconds(1.15f);
        MenuObject.SetActive(!MenuObject.activeSelf);
        yield return new WaitForSeconds(0.15f);
        Debounce = false;
    }
}
