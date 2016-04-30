using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] spawnLocation;
    public GameObject[] whatToSpawnPrefabBig;
    public GameObject[] whatToSpawnCloneBig;
    public GameObject[] whatToSpawnPrefabBoss;
    public GameObject[] whatToSpawnCloneBoss;
    public GameObject[] whatToSpawnPrefabFast;
    public GameObject[] whatToSpawnCloneFast;
    public GameObject[] whatToSpawnPrefabFast2;
    public GameObject[] whatToSpawnCloneFast2;
    private float timeSpawnerBig;
    public float timeSpawnerBigAux;
    private int numWaveBig;
    public int numMaxWaveBig;
    private float timeSpawnerBoss;
    public float timeSpawnerBossAux;
    private int numWaveBoss;
    public int numMaxWaveBoss;
    private float timeSpawnerFast;
    public float timeSpawnerFastAux;
    private int numWaveFast;
    public int numMaxWaveFast;
    private float timeSpawnerFast2;
    public float timeSpawnerFast2Aux;
    private int numWaveFast2;
    public int numMaxWaveFast2;

    void Start()
    {
        timeSpawnerBig = timeSpawnerBigAux;
        timeSpawnerBoss = timeSpawnerBossAux;
        timeSpawnerFast = timeSpawnerFastAux;
        timeSpawnerFast2 = timeSpawnerFast2Aux;
    }

    void Update()
    {
        if(numWaveBig < numMaxWaveBig)
        {
            if (timeSpawnerBig >= 0) timeSpawnerBig -= 1 * Time.deltaTime;
            else
            {
                numWaveBig++;
                SpawnEnemieBig();
            }
        }

        if (numWaveBoss < numMaxWaveBoss)
        {
            if (timeSpawnerBoss >= 0) timeSpawnerBoss -= 1 * Time.deltaTime;
            else
            {
                numWaveBoss++;
                SpawnEnemieBoss();
            }
        }

        if (numWaveFast < numMaxWaveFast)
        {
            if (timeSpawnerFast >= 0) timeSpawnerFast -= 1 * Time.deltaTime;
            else
            {
                numWaveFast++;
                SpawnEnemieFast();
            }
        }

        if (numWaveFast2 < numMaxWaveFast2)
        {
            if (timeSpawnerFast2 >= 0) timeSpawnerFast2 -= 1 * Time.deltaTime;
            else
            {
                numWaveFast2++;
                SpawnEnemieFast2();
            }
        }
    }

    void SpawnEnemieBig()
    {
        for (int i = 0; i < whatToSpawnCloneBig.Length; i++)
        {
            whatToSpawnCloneBig[i] = Instantiate(whatToSpawnPrefabBig[0], spawnLocation[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            timeSpawnerBig = timeSpawnerBigAux;
        }
    }

    void SpawnEnemieBoss()
    {
        for (int i = 0; i < whatToSpawnCloneBoss.Length; i++)
        {
            whatToSpawnCloneBoss[i] = Instantiate(whatToSpawnPrefabBoss[0], spawnLocation[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            timeSpawnerBoss = timeSpawnerBossAux;
        }
    }

    void SpawnEnemieFast()
    {
        for (int i = 0; i < whatToSpawnCloneFast.Length; i++)
        {
            whatToSpawnCloneFast[i] = Instantiate(whatToSpawnPrefabFast[0], spawnLocation[2].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            timeSpawnerFast = timeSpawnerFastAux;
        }
    }

    void SpawnEnemieFast2()
    {
        for (int i = 0; i < whatToSpawnCloneFast.Length; i++)
        {
            whatToSpawnCloneFast[i] = Instantiate(whatToSpawnPrefabFast[0], spawnLocation[3].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            timeSpawnerFast2 = timeSpawnerFast2Aux;
        }
    }
}
