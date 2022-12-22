using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using InfimaGames.LowPolyShooterPack;

public class GameManager : MonoBehaviour
{
   
    [SerializeField]
    Button quitButton;

    [SerializeField]
    Transform player;
    private float maximumDistance = 5f;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    int health;

    [SerializeField]
    GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
      //  player = GetComponent<Transform>();
        quitButton.onClick.AddListener(StopGame);
    }

    public void StopGame()
    {
        Application.Quit();
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(new Ray(player.position, player.forward),
               out RaycastHit hit, maximumDistance, mask))

        {
            Transform door;
            if (hit.transform.name == "Door")
            {
                door = hit.transform.transform;
                if (GameMangerr.bearCount >= 5)
                {
                    door.GetComponent<DoorManager>().OpenDoor();
                    door.GetComponent<BoxCollider>().isTrigger = true;
                }
                // Transform tra = transform1;
            }
           // else
             //   door.GetComponent<DoorManager>().CloseDoor();
        }
        Debug.Log("saurabh");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.GetComponent<Enemy>())
        {
            health -= 3;
            if(health <= 0)
            {
                panel.SetActive(true);
                GameObject game = GameObject.Find("P_LPSP_UI_Canvas(Clone)");
                if (game != null)
                    game.SetActive(false);
                GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");
                bullet.SetActive(false);
                GetComponentInChildren<Weapon>().enabled = false;
                GetComponentInChildren<CameraLook>().enabled = false;
                GetComponent<Movement>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
