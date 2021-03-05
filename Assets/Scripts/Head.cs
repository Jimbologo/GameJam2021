using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Head : MonoBehaviour
{
    [SerializeField]
    private MouthController mouthController;

    private HeadController head;
    public int maxmarshCount = 5;
    public int maxWaterCount = 5;
    private int maxHeads = 5;
    private int index = 0;
    private Vector3 targetPos = Vector3.zero;
    private float speed = 1f;
    private float headDistancing = 1f;
    [SerializeField]
    private float waitTime = 1f;

    public bool active = false;

    [SerializeField]
    private Animation anim;
    [SerializeField]
    private AnimationClip headOpen;
    [SerializeField]
    private AnimationClip headClose;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip mouthOpen;
    [SerializeField]
    private AudioClip mouthClose;
    [SerializeField]
    public AudioClip mouthChew;
    [SerializeField]
    private AudioClip deadJim;

    private float shiftTimer = 0f;

    private bool speedUp = false;

    public bool paused = false;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMesh;
    [SerializeField]
    private Color skinDeadColor;

    private bool dead = false;

    public void Setup(HeadController headController, int a_maxHeads, int a_index, float a_speed, float a_headDist, float a_waitTime, int maxObjs, int maxObjs2)
    {
        head = headController;
        maxHeads = a_maxHeads;
        index = a_index;
        speed = a_speed;
        headDistancing = a_headDist;
        waitTime = a_waitTime;
        maxmarshCount = maxObjs;
        maxWaterCount = maxObjs2;
        transform.localPosition = new Vector3(index * headDistancing, -5, 0);

        mouthController.ResetObj();

        StartCoroutine(MoveHeads());
    }

    public void UpdateTime(float tim)
    {
        waitTime = tim;

        if (waitTime < 1 && !speedUp)
        {
            waitTime = 1;
            speed *= 2;
            speedUp = true;
        }

        
    }

    private IEnumerator MoveHeads()
    {
        while (active)
        {
            if (!paused)
            {
                if(dead)
                {
                    targetPos = new Vector3(index * headDistancing, -5, 0);
                }
                else
                {
                    targetPos = new Vector3(index * headDistancing, 0, 0);
                }
                
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);

                if (Vector3.Distance(transform.localPosition, targetPos) <= 0.1f)
                {
                    if (index == 2 && !dead)
                    {
                        audioSource.clip = mouthOpen;
                        audioSource.Play();
                        anim.Play(headOpen.name);
                    }
                    else if (index == 3 && !dead)
                    {
                        audioSource.clip = mouthClose;
                        audioSource.Play();
                        anim.Play(headClose.name);
                    }

                    while (shiftTimer >= 0)
                    {
                        shiftTimer -= Time.deltaTime;
                        head.UpdateShiftTimer(Mathf.Round(shiftTimer));
                        yield return null;
                    }
                    if (waitTime > 0)
                    {
                        shiftTimer = waitTime;
                    }
                    else
                    {
                        shiftTimer = 0;
                        
                        mouthController.ResetObj();
                    }

                    

                    head.UpdateShiftTimer(Mathf.Round(shiftTimer));

                    index++;
                    if (index >= maxHeads)
                    {
                        index = 0;
                        transform.localPosition = new Vector3(0, -5, 0);
                        mouthController.ClearObjs();
                        mouthController.ResetObj();
                    }


                }
            }
            yield return null;
        }
        yield return null;
    }


    public IEnumerator RemovePerson()
    {
        dead = true;
        for (int i = 0; i < head.heads.Count; ++i)
        {
            head.heads[i].GetComponent<Head>().paused = true;
        }
        //Turn Blue
        float timer = 1f;
        while(timer >= 0)
        {
            timer -= Time.deltaTime;
            skinnedMesh.materials[0].color = Color.Lerp(skinnedMesh.materials[0].color, skinDeadColor, Time.deltaTime * 0.5f);
            yield return null;
        }
        audioSource.clip = deadJim;
        audioSource.Play();

        //Move down
        //targetPos = transform.localPosition - new Vector3(0, 5, 0);
        //while (Vector3.Distance(transform.localPosition, targetPos) > 0.1f)
        //{
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, speed * Time.deltaTime);
        //    yield return null;
        //}
        active = true;
        StartCoroutine(MoveHeads());

        for (int i = 0; i < head.heads.Count; ++i)
        {
            head.heads[i].GetComponent<Head>().paused = false;
        }

        
       

        head.AddDead();

        yield return null;
    }
}

