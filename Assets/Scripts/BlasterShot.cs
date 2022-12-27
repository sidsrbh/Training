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
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClipfire;
    }
}
