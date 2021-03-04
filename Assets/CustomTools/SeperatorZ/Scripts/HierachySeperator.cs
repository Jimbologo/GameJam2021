using UnityEngine;

public enum SELECTED_PREFIX
{
    dotted = 0,
    dash,
    star,
    underscore,
    enddotted,
    fullstop
}

public class HierachySeperator : MonoBehaviour
{
    [HideInInspector] public string headerName = "New Header";
    [HideInInspector] public SELECTED_PREFIX selectedPrefix = SELECTED_PREFIX.dash;
    [Range(0.1f,0.9f)] [HideInInspector] public float prefixLength = 0.5f;
}

