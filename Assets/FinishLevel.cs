using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {       
        GameManagerLevel.instance.win();
        if (other.gameObject.name == "P_LPSP_FP_CH")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


            }
    }
}
