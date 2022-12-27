using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float _spawnDelay = 12f;
    [SerializeField] MummyMovement[] _mummyPrefabs;

    private float _nextSpawnTime;
    int _spawnCount;
    // Update is called once per frame
    void Update()
    {
        if(ReadyToSpwan())
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        float delay = _spawnDelay - _spawnCount;
        delay = MathF.Max(1, delay);

        _nextSpawnTime = Time.time + _spawnDelay;
        int randomIndex = UnityEngine.Random.Range(0, _mummyPrefabs.Length);
        var mummyPrefab = _mummyPrefabs[randomIndex];
        var mummy = Instantiate(mummyPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        mummy.StartWalking();
    }

    bool ReadyToSpwan() => Time.time >= _nextSpawnTime;
   
}
