//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;
using UnityEngine.UI;

public class CreateRoomPanelController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int maxPlayerCount;

    public TMP_InputField roomNameInputField;

    public TMP_InputField playerCountInputField;

    public TMP_InputField roomPassInputField;

    public TMP_Dropdown levelChoiceDropDown;

    public Button createRoomButton;

    //These are properties for only players in the room
    private Hashtable customRoomProperties=new Hashtable();

    private string[] customRoomPropertiesForLobby = new string[2];
    private void Start()
    {
        roomNameInputField.onValueChanged.AddListener(onRoomNameInputFieldChanged);
        playerCountInputField.onValueChanged.AddListener(onPlayerCountInputFieldChanged);
        playerCountInputField.onDeselect.AddListener(CheckPlayerCount);
        createRoomButton.onClick.AddListener(onCreateButtonPressed);
    }

    private void OnDisable()
    {
        ResetAll();
    }
    private void onRoomNameInputFieldChanged(string roomName)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            createRoomButton.interactable = false;
            return;
        }
        if (string.IsNullOrEmpty(playerCountInputField.text))
        {
            createRoomButton.interactable = false;
            return;
        }

        createRoomButton.interactable = true;
        
    }

    //This is for not exceeding or lower the number of players.
    private void CheckPlayerCount(string playerNumber)
    {
        if (string.IsNullOrEmpty(playerNumber))
        {
            return;
        }
        if (int.Parse(playerNumber) < 2)
        {
            playerCountInputField.text = 2.ToString();
        }
        else if (int.Parse(playerNumber) > maxPlayerCount)
        {
            playerCountInputField.text = maxPlayerCount.ToString();
        }


    }


    private void onPlayerCountInputFieldChanged(string playerNumber)
    {

        if (string.IsNullOrEmpty(playerNumber))
        {
            createRoomButton.interactable = false;
            return;
        }

        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            createRoomButton.interactable = false;
            return;
        }

        

        createRoomButton.interactable = true;
    }

    private void onCreateButtonPressed()
    {
        //First we get properties of room from input fields and dropdown menu.
        string levelName = levelChoiceDropDown.captionText.text;

        string roomName = roomNameInputField.text;

        byte playerCount = byte.Parse(playerCountInputField.text);

        string roomPass = roomPassInputField.text;


        //RoomPass and LevelName is special properties for rooms so we set it into custom room properties and must be visible in the lobby.
        customRoomPropertiesForLobby[0] = "levName";
        customRoomPropertiesForLobby[1] = "Pass";

        customRoomProperties = new Hashtable { {"levName",levelName}, {"Pass",roomPass}};

        //Create the room options and create room.
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = playerCount,IsVisible=true,IsOpen=true,CustomRoomProperties=customRoomProperties,CustomRoomPropertiesForLobby=customRoomPropertiesForLobby};

        PhotonNetwork.CreateRoom(roomName, roomOptions,TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {

        //if we joined as as master client we need to load the selected level.
        //but if we join as a client automaticallysynchscene will handle scene load process.
        if (PhotonNetwork.IsMasterClient)
        {
            //We need to join exatly the same room as mentioned in level choice dropdown.
            PhotonNetwork.LoadLevel(levelChoiceDropDown.captionText.text);
        }
    }

    private void ResetAll()
    {
        roomNameInputField.text = "";
        roomPassInputField.text = "";
        playerCountInputField.text = "";

        createRoomButton.interactable = false;
    }
}
