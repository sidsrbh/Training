using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
  
    [SerializeField]
    int health;

    
   public GameObject gameoverPanel;
   public GameObject gameWinPanel;
   public TMP_Text gameWinText;

    [SerializeField]
    private AudioClip audioClipWalking;
    private AudioSource audioSource;
    Animator animator;
    public PhotonView pv;
    RpcTarget PhotonTargets;
    
    Vector3 position = new Vector3(0, 0, 0);
    EnemyManager enemyManager;
    bool EnemyConnected;
    public Button ReloadButton;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClipWalking;
        audioSource.loop = true;
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        // panel = GameObject.FindGameObjectWithTag("GameOverPanel");
        pv = GetComponent<PhotonView>();
     //   ReloadButton.onClick.AddListener(RestartGame);
        //bt=   gameoverPanel.GetComponentInChildren<Button>();
        // photonView = GetComponent<PhotonView>();
        //  PhotonTargets = GetComponent<RpcTarget>();
    }
    private void Start()
    {
        enemyManager.EnemyInstantiation();
      
    }
    // Update is called once per frame
    void Update()
    {
        //  string name = bt.name;
        if (EnemyConnected)
        {
           // enemyManager.EnemyInstantiation();
            EnemyConnected = false;
        }
        PhotonNetwork.LocalPlayer.SetScore(GameMangerr.points);
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable["Score"] = PhotonNetwork.LocalPlayer.GetScore().ToString();
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);

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


            //    }
            // if (GameMangerr.bearCount >= 2)
            EnemyConnected = true;
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
            Player q = PhotonNetwork.CurrentRoom.GetPlayer(p.ActorNumber);
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
        if (int.Parse(playerList[playerList.Count - 1].Item1) >= 40)
        {
            Invoke("EndGame",2);
            
            // gameWinText.gameObject.SetActive(true);
           // ReloadButton.onClick.AddListener(RestartGame);
           
            gameWinText.text = "Player Won: " + playerList[playerList.Count - 1].Item2;
           
            Debug.Log("Player Won" + playerList[0].Item2);

            // PhotonNetwork.LoadLevel(0);
            //   PhotonNetwork.LeaveRoom(true);
            //  SceneManager.LoadScene(0);
            //    PhotonNetwork.RejoinRoom("Room");
            //PhotonNetwork.ConnectUsingSettings();
            // RestartGame();
        }

    }

    public void EndGame()
    {
        GameMangerr.instance.StartScore();
        PhotonNetwork.LocalPlayer.SetScore(0);
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable["Score"] = PhotonNetwork.LocalPlayer.GetScore().ToString();
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);


        foreach (GameObject enemy in enemyManager.enemies)
            Destroy(enemy);
        enemyManager.enemies = null;
       // ReloadButton.onClick.AddListener(RestartGame);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerWeapon>().enabled = false;
        Invoke("OpenGamePanel", 2);
    }
    void OpenGamePanel()
    {
        gameWinPanel.SetActive(true);
    }
    public void RestartGame()
    {
        enemyManager.EnemyInstantiation();
        gameWinPanel.SetActive(false);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerWeapon>().enabled = true;
        transform.gameObject.SetActive(true);
    }

    public void OnPhotonSerialzeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            position = (Vector3)stream.ReceiveNext();
            transform.position = position;
            return;
        }
        if (stream.IsWriting)
        {
            position = transform.position;
            stream.SendNext(position);
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
        EndGame();
      //  ReloadButton.onClick.AddListener(RestartGame);
        yield return new WaitForSeconds(2);
        //  panel = GameObject.FindWithTag("GameOverPanel");

        transform.gameObject.SetActive(false);
         //gameoverPanel.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);
        gameWinText.text = "GameOver, Do want to restart game.";
        gameWinPanel.SetActive(true);
       

    }
}
