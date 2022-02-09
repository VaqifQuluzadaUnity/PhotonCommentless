using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class RoomlistElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomNameText;

    [SerializeField] private TextMeshProUGUI roomPlayerCountText;

    [SerializeField] private TextMeshProUGUI levelNameText;

    [SerializeField] private Button enterRoomButton;

    [SerializeField] private Image lockedUnlockedImage;

    [SerializeField] private Sprite lockedImageSprite;

    [SerializeField] private Sprite unlockedImageSprite;


    public void SetRoomListElement(RoomInfo roomInfo)
    {
        roomNameText.text = roomInfo.Name;

        roomPlayerCountText.text = roomInfo.PlayerCount.ToString() + "/" + roomInfo.MaxPlayers.ToString();
        
        //We use this string to get room name and room password from hastables of Custom Room Properties.

        string levelName = (string)roomInfo.CustomProperties["levName"];

        levelNameText.text =levelName;

        
        //Here we apply the password check. If room has a password, we need to show password 
        //authentication panel(it will be added)
        string roomPass =(string)roomInfo.CustomProperties["Pass"];

        if (string.IsNullOrEmpty(roomPass))
        {
            lockedUnlockedImage.sprite = unlockedImageSprite;
        }
        else
        {
            lockedUnlockedImage.sprite = lockedImageSprite;
        }

        //if the number of room players is maximum we need to disable room enter button.
        if (roomInfo.PlayerCount == roomInfo.MaxPlayers)
        {
            enterRoomButton.interactable = false;
        }
        else
        {
            enterRoomButton.interactable = true;
        }

        enterRoomButton.onClick.AddListener(delegate { onEnterRoomButtonPressed(roomInfo.Name); });
    }

    private void onEnterRoomButtonPressed(string levelName)
    {
        PhotonNetwork.JoinRoom(levelName);
    }
}
