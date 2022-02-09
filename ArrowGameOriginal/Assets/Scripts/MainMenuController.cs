using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviourPunCallbacks
{
    public static MainMenuController instance;

    public Button joinRandomButton;

    public Button CreateOrLobbyButton;

    public TextMeshProUGUI mainMenuInfoText;

    public GameObject CreateRoomPanel;

    public Button returnFromCreateRoomToMainMenuButton;

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
        joinRandomButton.onClick.AddListener(onJoinRandomButtonPressed);

        CreateOrLobbyButton.onClick.AddListener(onCreateOrLobbyButtonPressed);

        returnFromCreateRoomToMainMenuButton.onClick.AddListener(onReturnToMainMenuButtonPressed);
    }

    private void onReturnToMainMenuButtonPressed()
    {
        CreateRoomPanel.SetActive(false);
    }

    public TextMeshProUGUI userNameText;

    public void SetUserName(string userName)
    {
        userNameText.text = userName;
    }

    private void onJoinRandomButtonPressed()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private void onCreateOrLobbyButtonPressed()
    {
        CreateRoomPanel.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        ShowInfoNotificationText("There is no room in the lobby");

        CreateRoomPanel.SetActive(true);
    }


    public void ShowInfoNotificationText(string infoOrNotification)
    {
        mainMenuInfoText.text = infoOrNotification;
        mainMenuInfoText.GetComponent<Animation>().Stop();
        mainMenuInfoText.GetComponent<Animation>().Play();
    }   
}
