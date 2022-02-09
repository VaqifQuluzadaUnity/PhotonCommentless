using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PhotonErrorAndInfoHandler : MonoBehaviour
{
    public static PhotonErrorAndInfoHandler instance;

    public GameObject errorPanel;

    public TextMeshProUGUI errorMessageText;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ErrorInfoTextAnimation("Connecting"));
    }

    public void onErrorOccured(string errorMessage)
    {
        errorPanel.SetActive(true);

        ErrorInfoPanelLog(errorMessage);
    }

    public void onErrorSolved()
    {
        errorPanel.SetActive(false);
        errorMessageText.text = "";
        StopAllCoroutines();
    }

    public void ErrorInfoPanelLog(string errorPanelMessage)
    {
        StopAllCoroutines();
        StartCoroutine(ErrorInfoTextAnimation(errorPanelMessage));
    }


    IEnumerator ErrorInfoTextAnimation(string errorInfoText="")
    {
        errorMessageText.text = errorInfoText;
        for(int i = 0; i <3; i++)
        {
            errorMessageText.text = errorMessageText.text + ".";
            yield return new WaitForSecondsRealtime(0.5f);
        }
        errorMessageText.text = errorInfoText;
        StartCoroutine(ErrorInfoTextAnimation(errorInfoText));
    }
}
