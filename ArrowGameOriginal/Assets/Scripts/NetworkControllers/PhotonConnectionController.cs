using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonConnectionController : MonoBehaviourPunCallbacks
{
    public GameObject ErrorInfoPanel;

    public GameObject LoginPanel;

    private PhotonErrorAndInfoHandler errorHandler;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        errorHandler = PhotonErrorAndInfoHandler.instance;

        //We check our internet connection.
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            errorHandler.ErrorInfoPanelLog("Checking Internet Connection");
            InvokeRepeating("CheckInternetConnection", 0f, 0.1f);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        //For configuration of different versions we need to set Photon versions as app version.
        PhotonNetwork.GameVersion = Application.version;
    }

    ///When we connected to internet.
    public override void OnConnected()
    {
        errorHandler.ErrorInfoPanelLog("Internet Connection Established");
    }

    /// <summary>
    /// When we connected to master server.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        errorHandler.ErrorInfoPanelLog("Connected to Master Server");
        StartCoroutine(ConnectionDelay());
        Debug.Log(PhotonNetwork.CloudRegion);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (cause == DisconnectCause.ClientTimeout)
        {
            errorHandler.onErrorOccured("Client side disconnection");
        }
        else if (cause == DisconnectCause.ServerTimeout)
        {
            errorHandler.onErrorOccured("Server side disconnection");
        }

        InvokeRepeating("CheckInternetConnection", 0, 0.5f);
    }


    private void CheckInternetConnection()
    {
        //If we connected to internet
        if(Application.internetReachability != NetworkReachability.NotReachable)
        {
            //and joined the server
            if (PhotonNetwork.IsConnectedAndReady)
            {
                //We dont need to check connection.
                CancelInvoke("CheckInternetConnection");
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        
    }

    /// <summary>
    /// For making a slight delay after connection.
    /// </summary>
    /// <returns></returns>
    IEnumerator ConnectionDelay()
    {
        yield return new WaitForSeconds(1f);
        errorHandler.onErrorSolved();
        //After we will replace this with Playfab authentication.
        LoginPanel.SetActive(true);
    }
}
