using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Transform> enemytransforms;
    [SerializeField] GameObject enemyPrefab;
    public List<GameObject> enemies;
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
        enemies = new List<GameObject>();
        //   PhotonNetwork.Instantiate(enemyPrefab.name,new Vector3( enemytransforms[0].,0,), Quaternion.identity);
        foreach (Transform enemyTransform in enemytransforms)
        {
           enemies.Add(Instantiate(enemyPrefab, enemyTransform.position, Quaternion.identity));
        }
    }
}
