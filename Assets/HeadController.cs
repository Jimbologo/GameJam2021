using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    [SerializeField]
    private GameObject headPrefab;
    

    private List<Transform> heads = new List<Transform>();
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

    private void Start()
    {
        for (int i = 0; i < numberOfHeads; ++i)
        {
            GameObject newHead = Instantiate(headPrefab, transform);
            heads.Add(newHead.transform);
            headIndexs.Add(i);
            newHead.GetComponent<Head>().Setup(numberOfHeads, i, speed, headDistancing, waitingTime, Random.Range(2,10)); ;
        }
    }



}
