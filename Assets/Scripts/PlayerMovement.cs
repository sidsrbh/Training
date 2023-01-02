using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
  
    [SerializeField]
    int health;

    
   public GameObject gameoverPanel;
   public TMP_Text gameWinText;

    [SerializeField]
    private AudioClip audioClipWalking;
    private AudioSource audioSource;
    Animator animator;
    public PhotonView pv;
    RpcTarget PhotonTargets;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClipWalking;
        audioSource.loop = true;
       // panel = GameObject.FindGameObjectWithTag("GameOverPanel");
        pv = GetComponent<PhotonView>();
        // photonView = GetComponent<PhotonView>();
        //  PhotonTargets = GetComponent<RpcTarget>();
    }
    // Update is called once per frame
    void Update()
    {
       
        if (pv.IsMine)
        {
            var horizonatal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizonatal, 0f, vertical);
            movement *= Time.deltaTime * _speed;

            animator.SetFloat("VelocityX", horizonatal, 0.1f, Time.deltaTime);
            animator.SetFloat("VelocityZ", vertical, 0.1f, Time.deltaTime);
            if (audioSource.isPlaying)
                audioSource.Pause();
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
                audioSource.clip = audioClipWalking;
                //Play it!
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += -transform.forward * _speed * Time.deltaTime;
                audioSource.clip = audioClipWalking;
                //Play it!
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }

            if (Input.GetKey("d"))
            {
                transform.position += transform.right * _speed * Time.deltaTime;
                audioSource.clip = audioClipWalking;
                //Play it!
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            if (Input.GetKey("a"))
            {
                transform.position += -transform.right * _speed * Time.deltaTime;
                audioSource.clip = audioClipWalking;
                //Play it!
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }

            //  if (GameMangerr.bearCount >= 2)
           // GameObject p = GameObject.FindGameObjectWithTag("Player");
            //  if (PhotonNetwork.LocalPlayer != null)
         //   if (p.GetComponent<PlayerMovement>().pv.IsMine)
         //   {
                //PhotonNetwork.LocalPlayer.SetScore(GameMangerr.points);
                //ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                //hashtable["Score"] = PhotonNetwork.LocalPlayer.GetScore().ToString();
                //PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
                //Player ss = PhotonNetwork.PlayerList[0];
                //ExitGames.Client.Photon.Hashtable hashtabl = ss.CustomProperties;
                //Debug.Log(hashtabl["Score"]);

            //    }
           // if (GameMangerr.bearCount >= 2)
                GetResult();

            // Debug.Log(PhotonNetwork.LocalPlayer.GetScore().ToString());
        }
        //if (photonView.IsMine)
        //{

        //    PhotonNetwork.LocalPlayer.AddScore(GameMangerr.points);
        //    Debug.Log(PhotonNetwork.LocalPlayer.GetScore().ToString());
        //    // scoretext.text = score.ToString();
        //    //    GetComponent<PhotonView>().RPC("UpdateScores", PhotonTargets, GameMangerr.points);
        //}
        //Destroy(col.gameObject);
        // GetComponent<PhotonView>().RPC("UpdateScores", PhotonTargets);
    }

    void GetResult()
    {
        
        //  resultDeclared = true;
        List<(string, string)> playerList = new List<(string, string)>();
        
        int i = 0;
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            Player q =  PhotonNetwork.CurrentRoom.GetPlayer(p.ActorNumber);
            ExitGames.Client.Photon.Hashtable hashtable = p.CustomProperties;
            
            playerList.Add(((string)hashtable["Score"], p.NickName));
            i++;

            Debug.Log(p.NickName);
        }
        playerList.Sort((t1, t2) =>
        {
            return t1.Item1.CompareTo(t2.Item1);
        });
       // string score = playerList[0].Item1;
       // int a = int.Parse(score)
        if (int.Parse(playerList[playerList.Count -1].Item1) >= 40)
        {
            gameWinText.gameObject.SetActive(true);
            gameWinText.text = "Player Won: " + playerList[playerList.Count -1].Item2;
            Debug.Log("Player Won" + playerList[0].Item2);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Enemy>())
        {
            health -= 3;
            if (health <= 0)
            {
               
                animator.SetTrigger("Death");
                StartCoroutine(PlayerDeath());

            }
        }
    }
    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(3);
      //  panel = GameObject.FindWithTag("GameOverPanel");
        
        
        gameoverPanel.SetActive(true);
        transform.gameObject.SetActive(false);
    }
}
