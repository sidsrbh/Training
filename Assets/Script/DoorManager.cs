using InfimaGames.LowPolyShooterPack;
using InfimaGames.LowPolyShooterPack.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDoor()
    {
        anim.SetTrigger("GateOpen");
    }

    public void CloseDoor()
    {
        anim.SetTrigger("GateClose");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GameObject game = GameObject.Find("P_LPSP_UI_Canvas(Clone)");
            if (game != null)
                game.SetActive(false);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Movement>().enabled = false;
            player.GetComponent<AudioSource>().enabled = false;
            player.GetComponentInChildren<Weapon>().enabled = false;
            player.GetComponentInChildren<CameraLook>().enabled = false;
            GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.SetActive(false);
            panel.SetActive(true);
        }
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
