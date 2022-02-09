using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{

    [SerializeField] private RoomlistElement roomListElementPrefab;

    [SerializeField] private Transform roomElementContent;

    [SerializeField] private List<RoomlistElement> roomListArray = new List<RoomlistElement>();

    private void Start()
    {
        if (!PhotonNetwork.InLobby)
        {
            TypedLobby newLobby = new TypedLobby("Standart", LobbyType.Default);

            PhotonNetwork.JoinLobby(newLobby);
        }
        else
        {
            Debug.Log(PhotonNetwork.CurrentLobby.Name);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room list updated");
        ClearRoomList();
        foreach(RoomInfo roomInfo in roomList)
        {
            RoomlistElement roomElementInstance = Instantiate(roomListElementPrefab, roomElementContent);
            roomElementInstance.SetRoomListElement(roomInfo);
            roomListArray.Add(roomElementInstance);
        }
    }


    private void ClearRoomList()
    {
        foreach(RoomlistElement roomElement in roomListArray)
        {
            Destroy(roomElement.gameObject);
        }

        roomListArray.Clear();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.CurrentLobby.Name);
    }
}
