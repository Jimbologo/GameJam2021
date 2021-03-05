using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthController : MonoBehaviour
{
    List<int> objects = new List<int>();
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
        objects.Add(0);

        if(objects.Count >= headController.maxmarshCount && !isDrowning)
        {
            isDrowning = true;
            anim.Play(shakeHeadAnim.name);
            audioSource.clip = mouthShake;
            audioSource.Play();
        }

        Destroy(other.gameObject);
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
        objects.Clear();
    }
}
