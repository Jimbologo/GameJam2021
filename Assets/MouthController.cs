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

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
        objects.Add(other.transform);

        if(objects.Count >= headController.maxmarshCount)
        {
            anim.Play(shakeHeadAnim.name);
        }
    }

    public void ClearObjs()
    {
        for (int i = 0; i < objects.Count; ++i)
        {
            Destroy(objects[i].gameObject);
        }
        objects.Clear();
    }
}
