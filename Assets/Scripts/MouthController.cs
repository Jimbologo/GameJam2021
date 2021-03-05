using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthController : MonoBehaviour
{
    List<Transform> objects = new List<Transform>();
    [SerializeField]
    private Head headController;

    [SerializeField]
    private Animation anim;
    [SerializeField]
    private AnimationClip shakeHeadAnim;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip mouthShake;

    private bool isDrowning = false;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
        objects.Add(other.transform);

        if(objects.Count >= headController.maxmarshCount && !isDrowning)
        {
            isDrowning = true;
            anim.Play(shakeHeadAnim.name);
            audioSource.clip = mouthShake;
            audioSource.Play();
        }
    }

    private void Update()
    {
        if(!anim.isPlaying & isDrowning)
        {
            isDrowning = false;
        }
    }

    public void ClearObjs()
    {
        for (int i = 0; i < objects.Count; ++i)
        {
            if(objects[i] != null)
                Destroy(objects[i].gameObject);
        }
        objects.Clear();
    }
}
