
#if UNITY_EDITOR
using UnityEngine;

[CreateAssetMenu()]
public class SeperatorzSettings : ScriptableObject
{
    public string[] defaultSeperatorzNames = new string[]
    {
        "Managers",
        "Game World",
        "User Interface"
    };
}

#endif