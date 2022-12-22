using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class MultiplayerManager :MonoBehaviourPunCallbacks
{
    bool Connected;
    public Button connect;
    [SerializeField] TextMeshProUGUI MessagText;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        Connected = true;
    }

    void SearchRoom()
    {

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        CreateRoom();
    }

    public void CreateRoom()
    {
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 2;
        PhotonNetwork.CreateRoom("Room",ro,TypedLobbyInfo.Default);
        connect.gameObject.SetActive(false);
        MessagText.gameObject.SetActive(true);
        MessagText.text = "Joined Room Waiting for Player";
    }

    public override void OnCreatedRoom()
    {
        
    }

    public override void OnJoinedRoom()
    {
        connect.gameObject.SetActive(false);
        MessagText.gameObject.SetActive(true);
        MessagText.text = "Joined Room Waiting for Player";
    }
}
