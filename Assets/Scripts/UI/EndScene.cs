using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum EndingScene
{ 
    GOOD,
    BAD,
}


public class EndScene : MonoBehaviour
{

    string endingComment;
    [SerializeField] TMP_Text EndingComment;

    string endingDialog;
    [SerializeField] TMP_Text EndingDialog;
    [SerializeField] Image Background;
    [SerializeField] GameObject ResultDisplay;


    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text MoneyText;
    [SerializeField] TMP_Text MakeJuiceText;
    [SerializeField] TMP_Text GuestCountText;

    [SerializeField] Button ReturnMenu;

    private void Start()
    {
        ReturnMenu.onClick.AddListener(OnClickToScene);
        ReturnMenu.gameObject.SetActive(false);
        EndingComment.text = "";
        EndingDialog.text = "";
        EndingComment.gameObject.SetActive(false);
        EndingDialog.gameObject.SetActive(false);
        ResultDisplay.gameObject.SetActive(false);


        //Test
        // SetEnding(EndingScene.GOOD, 50, 30000, 55, 17);
    }

    public void OnClickToScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public IEnumerator EndingSequence()
    {
        // Display Game
        float alphaValue = 0f;
        Color alphaChange = new Color(0f, 0f, 0f, alphaValue);
        while (alphaValue < 1f)
        {
            alphaValue += (Time.unscaledDeltaTime);
            if (alphaValue > 1f)
            {
                alphaValue = 1f;
            }
            alphaChange = new Color(0f, 0f, 0f, alphaValue);
            Background.color = alphaChange;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        int count = 0;
        int maxcount = endingComment.Length;
        char[] bytes = endingComment.ToCharArray();
        EndingComment.gameObject.SetActive(true);

        while (count < maxcount)
        {
            EndingComment.text += bytes[count];

            yield return new WaitForSecondsRealtime(0.05f);
            count++;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        count = 0;
        maxcount = endingDialog.Length;
        bytes = endingDialog.ToCharArray();
        EndingDialog.gameObject.SetActive(true);

        while (count < maxcount)
        {
            EndingDialog.text += bytes[count];

            yield return new WaitForSecondsRealtime(0.1f);
            count++;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        ResultDisplay.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);
        ReturnMenu.gameObject.SetActive(true);
    }


    public void SetEnding(EndingScene _end, int Score, int Money, int Juice, int Guest)
    {
        switch (_end)
        { 
            case EndingScene.GOOD:
                endingComment = "더위, 이상 없음. (GOOD)";
                endingDialog = "평소보다 뜨거운 여름인 남극에서의 카페 첫 시작은 성황리에 마치게 되었다.";
                EndingComment.color = new Color(77f / 255f, 153f/ 255f, 161f/ 255f, 1f);
                break;
            case EndingScene.BAD:
                endingComment = "더위, 이상 있음.. (BAD)";
                endingDialog = "손님들은 더움을 이겨내지 못하고 불평불만을 쏟아내며 자리를 떠나게 됐다.";
                EndingComment.color = new Color(207f / 255f, 60f / 255f, 64f / 255f, 1f);
                break;
        }

        ScoreText.text = Score.ToString("D8");
        MoneyText.text = Money.ToString("D8");
        MakeJuiceText.text = Juice.ToString("D8");
        GuestCountText.text = Guest.ToString("D8");

        StartCoroutine(EndingSequence());
    }


}
