using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeadController : MonoBehaviour
{
    [SerializeField]
    private GameObject headPrefab;
    

    public List<Transform> heads = new List<Transform>();
    private List<int> headIndexs = new List<int>();

    [SerializeField]
    private int numberOfHeads = 5;

    [SerializeField]
    private float speed = 1f;

    private bool active = true;

    [SerializeField]
    private float headDistancing = 1f;

    [SerializeField]
    private float waitingTime = 1f;

    [SerializeField]
    private TextMeshPro shiftTimer;

    [SerializeField]
    private float decreaseTimeSpeed = 0.1f;

    public int deadCount = 0;

    private void Start()
    {
        for (int i = 0; i < numberOfHeads; ++i)
        {
            GameObject newHead = Instantiate(headPrefab, transform);
            heads.Add(newHead.transform);
            headIndexs.Add(i);
            newHead.GetComponent<Head>().Setup(this, numberOfHeads, i, speed, headDistancing, waitingTime, Random.Range(2,10), Random.Range(2, 10)); ;
        }

        //StartCoroutine(decreaseWaitingTime());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManagment.instance.LoadScene(CustomScenes.MainMenu);
        }
    }

    public IEnumerator decreaseWaitingTime()
    {
        while(active && waitingTime > 1)
        {
            waitingTime -= Time.deltaTime * decreaseTimeSpeed;
            for (int i = 0; i < heads.Count; ++i)
            {
                heads[i].GetComponent<Head>().UpdateTime(waitingTime);
            }
            
            yield return null;
        }
        yield return null;
    }

    public void UpdateShiftTimer(float time)
    {
        shiftTimer.text = "" + time;
    }

    public void AddDead()
    {
        deadCount++;
        if(deadCount >= numberOfHeads)
        {
            //Show Failed screen
            SceneManagment.instance.LoadScene(CustomScenes.DeathScreen);
        }
    }


}
