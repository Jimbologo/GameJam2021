#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildScript : MonoBehaviour
{
    [MenuItem("CustomTools/Building/Build For Windows")]
    public static void BuildForWindows()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = "./Builds/Windows/ProjectSkyscraper_x64.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        List<string> settingsScenes = new List<string>();

        for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
        {
            settingsScenes.Add(EditorBuildSettings.scenes[i].path);
        }

        buildPlayerOptions.scenes = settingsScenes.ToArray();
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    [MenuItem("CustomTools/Building/Build For Linux")]
    public static void BuildForLinux()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = "./Builds/Linux/ProjectSkyscraper_x64.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
        List<string> settingsScenes = new List<string>();

        for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
        {
            settingsScenes.Add(EditorBuildSettings.scenes[i].path);
        }

        buildPlayerOptions.scenes = settingsScenes.ToArray();
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}
#endif