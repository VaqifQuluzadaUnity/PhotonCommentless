using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class PhotonLoginController : MonoBehaviour
{
    public TMP_InputField userNickNameInputField;

    public Button loginButton;

    public GameObject loginPanel;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }
    private void Start()
    {
        loginButton.interactable = false;
        userNickNameInputField.onValueChanged.AddListener(onUserNickNameFieldChanged);

        loginButton.onClick.AddListener(onLoginButtonClicked);
    }

    private void onUserNickNameFieldChanged(string nickName)
    {
        if (string.IsNullOrEmpty(nickName))
        {
            loginButton.interactable = false;
            return;
        }
        loginButton.interactable = true;
    }

    private void onLoginButtonClicked()
    {
        PhotonNetwork.NickName = userNickNameInputField.text;
        MainMenuController.instance.SetUserName(userNickNameInputField.text);
        loginPanel.SetActive(false);
    }
}
