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
    public Button Connect;
    [SerializeField] TextMeshProUGUI MessagText;
    [SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Connect.onClick.AddListener(SearchRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        Connected = true;
    }

    void SearchRoom()
    {

        PhotonNetwork.JoinRandomRoom();
        Debug.Log("searching for ");
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

       

    }

    public override void OnCreatedRoom()
    {
        Connect.gameObject.SetActive(false);
        MessagText.gameObject.SetActive(true);
        MessagText.text = "Joined Room Waiting for Player";

        int randomNumber = Random.Range(130, 150);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomNumber, 0f, randomNumber), Quaternion.identity);
    }

    public override void OnJoinedRoom()
    {
        Connect.gameObject.SetActive(false);
        MessagText.gameObject.SetActive(true);
        MessagText.text = "Joined Room Waiting for Player";
    }
}
