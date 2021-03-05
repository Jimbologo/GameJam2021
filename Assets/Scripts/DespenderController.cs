using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespenderController : MonoBehaviour
{
    private bool isWater = true;

    private Vector3 waterPos = new Vector3(9.92f, 3.47f,0.62f);
    private Vector3 marshPos = new Vector3(5.55f, 3.47f, 0.62f);

    private float speed = 1f;

    [SerializeField]
    private Spawner waterSpawner;
    [SerializeField]
    private Spawner marshSpawner;

    private void Update()
    {
        if(isWater)
        {
            transform.position = Vector3.Lerp(transform.position, waterPos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, marshPos, speed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            isWater = !isWater;
            if(isWater)
            {
                waterSpawner.isActive = true;
                marshSpawner.isActive = false;
            }
            else
            {
                waterSpawner.isActive = false;
                marshSpawner.isActive = true;
            }
        }
    }
}
