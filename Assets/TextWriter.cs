using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    [SerializeField]
    private string msg = "";
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float lagTime = 0.2f;

    private int index = 0;

    private void Start()
    {
        StartCoroutine(Write());
    }

    private IEnumerator Write()
    {
        yield return new WaitForSeconds(2.0f);

        while(text.text.Length < msg.Length)
        {
            index += 1;
            index = Mathf.Clamp(index,0, msg.Length);
            text.text = msg.Substring(0,index);

            yield return new WaitForSeconds(lagTime);
        }

        yield return new WaitForSeconds(2.0f);

        SceneManagment.instance.LoadScene(CustomScenes.MainMenu);

        yield return null;
    }
}
