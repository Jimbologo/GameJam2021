using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;

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

    [SerializeField]
    private TextMeshPro waterText;
    [SerializeField]
    private TextMeshPro marshText;

    public int waterLeft = 0;
    public int marshLeft = 0;

    bool chewed = false;

    [SerializeField]
    private VisualEffect visualEffect;

    private void Start()
    {
        visualEffect.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
        objects.Add(0);

        if (other.GetComponent<MarshmellowDrop>().isWater)
        {
            waterLeft--;
            
        }
        else
        {
            marshLeft--;
            
        }

        if ((waterLeft < 0 || marshLeft < 0) && !isDrowning)
        {
            isDrowning = true;
            anim.Play(shakeHeadAnim.name);
            audioSource.clip = mouthShake;
            audioSource.Play();
            visualEffect.Play();
            //headController.StopAllCoroutines();
            headController.active = false;
            StartCoroutine(headController.RemovePerson());
        }

        waterLeft = Mathf.Clamp(waterLeft, 0, 100);
        waterText.text = "" + waterLeft;
        marshLeft = Mathf.Clamp(marshLeft, 0, 100);
        marshText.text = "" + marshLeft;

        Destroy(other.gameObject);

        if (waterLeft <= 0 && marshLeft <= 0 && !chewed)
        {
            chewed = true;
            audioSource.clip = headController.mouthChew;
            audioSource.Play();
        }
        else if(chewed)
        {
            chewed = false;
        }
    }

    public void ResetObj()
    {
        waterLeft = headController.maxWaterCount;
        waterText.text = "" + waterLeft;
        marshLeft = headController.maxmarshCount;
        marshText.text = "" + marshLeft;
    }

    private void Update()
    {
        if(!anim.isPlaying & isDrowning)
        {
            isDrowning = false;
            visualEffect.Stop();
        }
    }

    public void ClearObjs()
    {
        objects.Clear();
    }
}
