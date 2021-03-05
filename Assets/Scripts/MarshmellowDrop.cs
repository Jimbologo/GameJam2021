using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshmellowDrop : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip marshHit;

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.clip = marshHit;
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -10)
        {
            MCBlob blob = FindObjectOfType<MCBlob>();
            SphereCollider sphereCollider = GetComponent<SphereCollider>();
            if (blob && sphereCollider)
            {
                blob.BlobObjectsLocations.Remove(sphereCollider);
            }
            Destroy(this.gameObject);
        }
    }
}
