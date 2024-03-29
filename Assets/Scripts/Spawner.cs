using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObjPrefab;

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

    public bool isActive = false;

    [SerializeField]
    private Transform barrel;

    [SerializeField]
    private float fireForce = 1f;

    [SerializeField]
    private float lookSpeed = 1;

    [SerializeField]
    private bool isWater = false;

    [SerializeField]
    private Transform liquidContainer;

    private void Update()
    {
        if(!isActive)
        {
            return;
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            spawnerWaitTimer -= Time.deltaTime;

            if(spawnerWaitTimer <= 0)
            {
                if(isWater && liquidContainer.GetComponent<MCBlob>().BlobObjectsLocations.Count > 6)
                {
                    return;
                }
                spawnerWaitTimer = spawnerWaitTime;
                GameObject newMarsh = Instantiate(spawnObjPrefab, barrel.position, Quaternion.Euler(Random.Range(0,180), Random.Range(0, 180), Random.Range(0, 180))); ;

                if (isWater)
                {
                    newMarsh.transform.parent = liquidContainer;
                }
                newMarsh.GetComponent<Rigidbody>().AddForce(barrel.transform.forward * fireForce, ForceMode.Impulse);
                marshmelows.Add(newMarsh);
                audioSource.clip = marshSpawn;
                audioSource.Play(); 
                if(isWater)
                {
                    liquidContainer.GetComponent<MCBlob>().BlobObjectsLocations.Add(newMarsh.GetComponent<SphereCollider>());
                    newMarsh.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }
        Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Quaternion rotation = Quaternion.LookRotation(lookDir, Vector3.right);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Clamp(transform.eulerAngles.y, -88, -91), -180);
        
    }
}
