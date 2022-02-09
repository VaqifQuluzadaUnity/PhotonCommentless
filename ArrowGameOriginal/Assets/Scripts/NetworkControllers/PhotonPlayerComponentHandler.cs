using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// This component is used to enable/disable the components of player across network.
/// </summary>
public class PhotonPlayerComponentHandler : MonoBehaviourPunCallbacks
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Canvas playerCanvas;
    private void Start()
    {
        if (!photonView.IsMine&&PhotonNetwork.IsConnected)
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            playerCamera.enabled = false;
            playerCanvas.enabled = false;
        }
    }
}
