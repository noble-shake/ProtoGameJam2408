using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject StartScreen;
    [SerializeField] GameObject StartScreenBG;
    [SerializeField] GameObject StartScreenText;
    [SerializeField] GameObject[] TutorialScreens;
    [SerializeField] Button BtnNext;
    [SerializeField] TMP_Text BtnText;
    int count;

    private void Start()
    {
        StartScreen.SetActive(true);
        count = 0;
        BtnNext.onClick.AddListener(OnClickTutorialNext);

        StartCoroutine(TutorialStart());
    }

    public void OnClickTutorialNext()
    {
        if (count == TutorialScreens.Length-1)
        {
            GameManager.instance.TutorialCheck = false;
            Destroy(gameObject);
            return;
        }

        TutorialScreens[count++].SetActive(false);
        TutorialScreens[count].SetActive(true);
        if (count == TutorialScreens.Length-1) BtnText.text = "게임 시작";
    }

    public IEnumerator TutorialStart()
    {

        StartScreen.GetComponent<Image>();

        yield return new WaitForSeconds(1.5f);

        float curTime = 2.5f;
        float alphaValue = 0f;
        while (curTime > 0f)
        { 
            curTime -= Time.deltaTime;
            alphaValue += (1f - (curTime / 2.5f));

            StartScreenBG.GetComponent<Image>().color = new Color(0f, 0f, 0f, alphaValue);
            StartScreenText.GetComponent<TMP_Text>().color = new Color(0f, 0f, 255f, alphaValue);

            yield return null;
        }

        yield return null;
        StartScreen.SetActive(false);
        TutorialScreens[0].SetActive(true);
        BtnNext.gameObject.SetActive(true);
    }

}
