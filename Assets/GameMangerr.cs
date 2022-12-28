using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class GameMangerr : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameMangerr instance;
    public TextMeshProUGUI scoreText;
    public static int bearCount = 0;
    public static int points = 0;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        points = 0;
        scoreText.text = points.ToString();
    }

    public void updateScore(int score)
    {

        points += score;
        
        scoreText.text = points.ToString();
        bearCount += 1;

        GameObject p =  GameObject.FindGameObjectWithTag("Player");
       if( p.GetComponent<PlayerMovement>().pv.IsMine)
        {
            PhotonNetwork.LocalPlayer.AddScore(score);
            
        }
    }

   
}
