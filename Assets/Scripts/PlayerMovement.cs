using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
  
    [SerializeField]
    int health;

    
   public GameObject panel;

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
        panel = GameObject.FindGameObjectWithTag("GameOverPanel");
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
            Debug.Log(PhotonNetwork.LocalPlayer.GetScore().ToString());
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
        
        
        panel.SetActive(true);
        transform.gameObject.SetActive(false);
    }
}
