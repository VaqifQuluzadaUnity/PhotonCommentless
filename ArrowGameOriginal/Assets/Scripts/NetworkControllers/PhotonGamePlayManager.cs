using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhotonGamePlayManager : MonoBehaviourPunCallbacks
{
    public static PhotonGamePlayManager instance;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Transform[] playerSpawnPoint;



    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.Log("The player prefab is null");
        }
        else
        {
            int spawnPointIndex = PhotonNetwork.CurrentRoom.PlayerCount - 1;

            PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPoint[spawnPointIndex].position, Quaternion.identity);
        }
    }
}
