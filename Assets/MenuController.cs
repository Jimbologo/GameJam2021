using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Transform eye1;
    [SerializeField]
    private Transform eye2;

    [SerializeField]
    private float lookSpeed = 1;
    // Update is called once per frame
    void Update()
    {
        Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(eye1.position);
        Quaternion rotation = Quaternion.LookRotation(lookDir, Vector3.up);
        Vector3 rots = rotation.eulerAngles;
        rots.x *= -1;
        rots.y *= 1;
        rots.z *= -1;
        rotation.eulerAngles = rots;
        eye1.rotation = Quaternion.Lerp(eye1.rotation, rotation, lookSpeed * Time.deltaTime);
        eye2.rotation = Quaternion.Lerp(eye2.rotation, rotation, lookSpeed * Time.deltaTime);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.GetComponent<MarshmellowDrop>() && Input.GetKey(KeyCode.Mouse0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 2.2f;
                Vector3 newMPos = Camera.main.ScreenToWorldPoint(mousePos);

                Vector3 newPos = new Vector3(newMPos.x, newMPos.y, newMPos.z);
                hit.transform.position = newPos;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hmm");
        if(other.GetComponent<MarshmellowDrop>())
        {
            SceneManagment.instance.LoadScene(CustomScenes.GameScene);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hmm");
    }
}
