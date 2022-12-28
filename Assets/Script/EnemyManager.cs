using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> enemytransforms;
    [SerializeField] GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnemyInstantiation()
    {
     //   PhotonNetwork.Instantiate(enemyPrefab.name,new Vector3( enemytransforms[0].,0,), Quaternion.identity);
    }
}
