using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultigameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] GameObject playerPrefab;
    void Start()
    {
        connect();


    }


    public void connect()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            int randomNumber = Random.Range(130, 150);
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomNumber, 0f, randomNumber), Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
