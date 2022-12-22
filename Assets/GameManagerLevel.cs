using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManagerLevel instance = null;
    public GameObject movelevelText;
    //public TextMeshProUGUI movelevelText;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void win()
    {

        movelevelText.SetActive(true);
    }
}
