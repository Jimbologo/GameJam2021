using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Head : MonoBehaviour
{
    [SerializeField]
    private MouthController mouthController;
    public int maxmarshCount = 5;
    private int maxHeads = 5;
    private int index = 0;
    private Vector3 targetPos = Vector3.zero;
    private float speed = 1f;
    private float headDistancing = 1f;
    private float waitTime = 1f;

    public bool active = false;

    [SerializeField]
    private Animation anim;
    [SerializeField]
    private AnimationClip headOpen;
    [SerializeField]
    private AnimationClip headClose;

    [SerializeField]
    private TextMeshPro countText = null;

    public void Setup(int a_maxHeads, int a_index, float a_speed, float a_headDist, float a_waitTime, int maxObjs)
    {
        maxHeads = a_maxHeads;
        index = a_index;
        speed = a_speed;
        headDistancing = a_headDist;
        waitTime = a_waitTime;
        maxmarshCount = maxObjs;
        transform.localPosition = new Vector3(index * headDistancing, -5, 0);

        countText.text = "" + maxmarshCount;

        StartCoroutine(MoveHeads());
    }


    private IEnumerator MoveHeads()
    {
        while (active)
        {
            targetPos = new Vector3(index * headDistancing, 0, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, targetPos) <= 0.1f)
            {
                if (index == 2)
                {
                    anim.Play(headOpen.name);
                }
                else if (index == 3)
                {
                    anim.Play(headClose.name);
                }

                yield return new WaitForSeconds(waitTime);

                index++;
                if (index >= maxHeads)
                {
                    index = 0;
                    transform.localPosition = new Vector3(0, -5, 0);
                    mouthController.ClearObjs();
                }

                
            }
            yield return null;
        }
        yield return null;
    }
}

