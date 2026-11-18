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

    bool ToInfo;


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

        Camera.main.transform.position = InfoCamera;

    }


    private void Update()
    {

        if (ToInfo == true)
        {
            if (Vector3.Distance(Camera.main.transform.position, InfoCamera) < 0.005f) return;

            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, InfoCamera, Time.deltaTime * 5f);
        }
        else
        {
            if (Vector3.Distance(Camera.main.transform.position, DefaultCamera) < 0.005f) return;

            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, DefaultCamera, Time.deltaTime * 5f);
        }

    }


    public void OnClickStart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnClickInfo()
    {
        ToInfo = true;
        StartBtn.gameObject.SetActive(false);
        InfoBtn.gameObject.SetActive(false);
        ExitBtn.gameObject.SetActive(false);
        BackBtn.gameObject.SetActive(true);
        InfoDisplay.gameObject.SetActive(true);
    }

    public void OnClickExit()
    {
        Application.Quit(); 
    }

    public void OnClickBack()
    {
        ToInfo = false;
        StartBtn.gameObject.SetActive(true);
        InfoBtn.gameObject.SetActive(true);
        ExitBtn.gameObject.SetActive(true);
        BackBtn.gameObject.SetActive(false);
        InfoDisplay.gameObject.SetActive(false); 
    }

}
