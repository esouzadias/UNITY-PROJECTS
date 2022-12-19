using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] NPCS;
    public float spawnTime;
    public int Spawned = 0;
    public int maxSpawn = 0;

    void Start(){
        InvokeRepeating ("SpawnNPC", spawnTime, spawnTime);
    }

    void SpawnNPC(){
        if(Spawned < maxSpawn){
            int randomIndex = Random.Range(0, NPCS.Length);
            Instantiate(NPCS[randomIndex], transform.position, transform.rotation);
            Spawned ++;
        } else CancelInvoke("SpawnNPC");
    }
}
