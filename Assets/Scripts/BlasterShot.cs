using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterShot : MonoBehaviour
{
    [SerializeField] float _speed = 15f;
    [SerializeField]
    private AudioClip audioClipfire;
    private AudioSource audioSource;
    public void Launch(Vector3 direction)
    {
        direction.Normalize();
        transform.up = direction;
        GetComponent<Rigidbody>().velocity = direction * _speed;
        audioSource.clip = audioClipfire;
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.transform.tag == "Enemy")
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            //  if (PhotonNetwork.LocalPlayer != null)
            if (p.GetComponent<PlayerMovement>().pv.IsMine)
            {
                PhotonNetwork.LocalPlayer.SetScore(GameMangerr.points);
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                hashtable["Score"] = PhotonNetwork.LocalPlayer.GetScore().ToString();
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
                Player ss = PhotonNetwork.PlayerList[0];
                ExitGames.Client.Photon.Hashtable hashtabl = ss.CustomProperties;
                Debug.Log(hashtabl["Score"]);

            }
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClipfire;
    }
}
