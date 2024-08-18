using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    [SerializeField] Button StartBtn;
    [SerializeField] Button InfoBtn;
    [SerializeField] Button ExitBtn;
    
    [SerializeField] Button BackBtn;
    [SerializeField] GameObject InfoDisplay;

    [SerializeField] Vector3 DefaultCamera;
    [SerializeField] Vector3 InfoCamera;


    private void Awake()
    {
        Screen.SetResolution(720, 1280, false);
    }

    private void Start()
    {
        StartBtn.onClick.AddListener(OnClickStart);
        InfoBtn.onClick.AddListener(OnClickInfo);
        ExitBtn.onClick.AddListener(OnClickExit);
        BackBtn.onClick.AddListener(OnClickBack);


    }


    public void OnClickStart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnClickInfo()
    {
        StartBtn.gameObject.SetActive(false);
        InfoBtn.gameObject.SetActive(false);
        ExitBtn.gameObject.SetActive(false);
        BackBtn.gameObject.SetActive(true);
        Camera.main.transform.position = InfoCamera;
        InfoDisplay.gameObject.SetActive(true);
    }

    public void OnClickExit()
    {
        Application.Quit(); 
    }

    public void OnClickBack()
    {
        StartBtn.gameObject.SetActive(true);
        InfoBtn.gameObject.SetActive(true);
        ExitBtn.gameObject.SetActive(true);
        BackBtn.gameObject.SetActive(false);
        Camera.main.transform.position = DefaultCamera;
        InfoDisplay.gameObject.SetActive(false); 
    }

}
