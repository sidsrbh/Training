using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultiPlayerManager : MonoBehaviour
{
    //public string roomName;
    //public Button Connect;
    //bool Connected;
    //[SerializeField] TextMeshProUGUI MessageText;
    //public static UnityEvent JoinedRoom;
    //// Start is called before the first frame update
    //void Start()
    //{

    //    PhotonNetwork.ConnectUsingSettings();
    //    if (JoinedRoom == null)
    //    {
    //        JoinedRoom = new UnityEvent();
    //    }
    //    Connect.onClick.AddListener(SearchRoom);
    //}

    //public override void OnConnectedToMaster()
    //{
    //    base.OnConnectedToMaster();
    //    Debug.Log("I am connected");
    //}
    //private void SearchRoom()
    //{
    //    PhotonNetwork.JoinRoom("Room");
    //    Debug.Log("Searching");
    //}
    //public override void OnJoinRoomFailed(short returnCode, string message)
    //{
    //    base.OnJoinRoomFailed(returnCode, message);
    //    CreateRoom();
    //}
    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    base.OnJoinRoomFailed(returnCode, message);
    //    CreateRoom();
    //}

    //private void CreateRoom()
    //{
    //    RoomOptions ro = new RoomOptions();
    //    ro.MaxPlayers = 1;
    //    PhotonNetwork.CreateRoom("Room", ro, TypedLobbyInfo.Default);
    //}

    //public override void OnCreatedRoom()
    //{
    //    Connect.gameObject.SetActive(false);
    //    MessageText.gameObject.SetActive(true);
    //    MessageText.text = "Create Room Waiting for Players..";
    //}

    //public override void OnJoinedRoom()
    //{
    //    Connect.gameObject.SetActive(false);
    //    MessageText.gameObject.SetActive(true);
    //    MessageText.text = "joined Room waiting for Players.." + PhotonNetwork.CurrentRoom.Name;

    //    JoinedRoom.Invoke();
    //}
}
