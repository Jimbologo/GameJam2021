using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject marshmellow;

    private List<GameObject> marshmelows = new List<GameObject>();

    [SerializeField]
    private float spawnerWaitTime = 0.1f;
    private float spawnerWaitTimer = 0.1f;

    [SerializeField]
    private float spawnRange = 2f;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip marshSpawn;


    private void Update()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            spawnerWaitTimer -= Time.deltaTime;

            if(spawnerWaitTimer <= 0)
            {
                spawnerWaitTimer = spawnerWaitTime;
                GameObject newMarsh = Instantiate(marshmellow, transform.position + new Vector3(Random.Range(-spawnRange, spawnRange),0,0), Quaternion.Euler(Random.Range(0,180), Random.Range(0, 180), Random.Range(0, 180))); ;
                newMarsh.transform.parent = transform;
                marshmelows.Add(newMarsh);
                audioSource.clip = marshSpawn;
                audioSource.Play(); 

            }
        }
    }
}
