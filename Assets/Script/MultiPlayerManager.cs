using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class MultiPlayerManager : MonoBehaviourPunCallbacks
{
    bool Connected;
   
    public Button Connect;
    public Button RestartGame;
    [SerializeField] TextMeshProUGUI MessagText;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject gameWinPanel;
    [SerializeField] TMP_Text gameWinText;
    public GameObject cameraPrefab;
  
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
      //  OnConnectedToMaster();
       
        

    }
    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        Connect.onClick.AddListener(SearchRoom);
    }
    // Update is called once per frame
    void Update()
    {
       
      
      //  Player ss = PhotonNetwork.PlayerList[0];
       // ExitGames.Client.Photon.Hashtable hashtabl = ss.CustomProperties;
      //  Debug.Log(hashtabl["Score"]);
     //   GetResult();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connected");
        Connected = true;

    }

    public void SearchRoom()
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
        
        PhotonNetwork.CreateRoom("Room", ro, TypedLobbyInfo.Default);
    }

    public override void OnCreatedRoom()
    {
        Connect.gameObject.SetActive(false);
    //    MessagText.gameObject.SetActive(true);
       // MessagText.text = "Joined Room Waiting for Player";

       // int randomNumber = Random.Range(130, 150);
     //   GameObject player =  PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomNumber, 0f, randomNumber), Quaternion.identity);
        
    }

    public override void OnJoinedRoom()
    {
        Connect.gameObject.SetActive(false);
      //  MessagText.gameObject.SetActive(true);
        //   MessagText.text = "Joined Room Waiting for Player";
        ExitGames.Client.Photon.Hashtable myhashtable = PhotonNetwork.CurrentRoom.CustomProperties;

        int randomNumber = 0;
        while (PhotonNetwork.CurrentRoom.CustomProperties["player" + PhotonNetwork.LocalPlayer.ActorNumber] == null)
        {
            randomNumber = Random.Range(130, 150);
            bool found = false;
            foreach (DictionaryEntry kvp in myhashtable)
            {
                if ((int)kvp.Value == randomNumber)
                {
                    found = true;
                }
            }
            if (!found)
            {
                myhashtable = PhotonNetwork.CurrentRoom.CustomProperties;
                myhashtable["player" + PhotonNetwork.LocalPlayer.ActorNumber] = randomNumber;
                PhotonNetwork.CurrentRoom.SetCustomProperties(myhashtable);
            }
        }
        player =  PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomNumber, 0f, randomNumber), Quaternion.identity);
        
        player.GetComponent<PlayerMovement>().enabled = true;
        RestartGame.onClick.AddListener(player.GetComponent<PlayerMovement>().RestartGame);
        player.GetComponent<PlayerMovement>().gameoverPanel = gameoverPanel;
        player.GetComponent<PlayerMovement>().gameWinPanel = gameWinPanel;
        player.GetComponent<PlayerMovement>().gameWinText = gameWinText;
        player.GetComponent<PlayerMovement>().ReloadButton = RestartGame;


        if(player.GetComponent<PhotonView>().IsMine)
        {
            GameObject go = Instantiate(cameraPrefab, player.transform);
            go.transform.localPosition = new Vector3(0, 2, -1.6f);
            go.transform.rotation = Quaternion.Euler(24.88f, 0, 0);
            
        }
        player.name = "srs";
        PhotonNetwork.LocalPlayer.NickName = "SRS";

    }

  
}
