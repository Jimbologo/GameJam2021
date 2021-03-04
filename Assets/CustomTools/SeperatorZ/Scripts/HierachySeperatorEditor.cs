
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Text;

[CustomEditor(typeof(HierachySeperator))]
[CanEditMultipleObjects]
public class HierachySeperatorEditor : Editor
{
    private const int headerNameMaxLength = 40;
    public char headerPrefix = '-';

    private string[] prefixOptions = new string[] { "-", "━", "*", "_", "¬", "." };

    [SerializeField]
    private SeperatorzSettings seperatorzSettings;

    void handleItemClicked(object parameter)
    {
        DebugZ.instance.Log("");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        HierachySeperator myTarget = (HierachySeperator)target;

        //Create a text input for header name
        string newHeaderName = EditorGUILayout.TextField(myTarget.headerName).ToUpper();

        //Create prefix selection for header
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Prefix/Postfix Char", GUILayout.Width(Screen.width * 0.3f));
        myTarget.selectedPrefix = (SELECTED_PREFIX)EditorGUILayout.Popup((int)myTarget.selectedPrefix, prefixOptions, GUILayout.Width(Screen.width  * 0.3f));
        EditorGUILayout.EndHorizontal();
        float newPrefixLength = EditorGUILayout.Slider(myTarget.prefixLength,0.1f,0.9f);

        //Get the header prefix from the dropdown
        char newheaderPrefix = prefixOptions[(int)myTarget.selectedPrefix].ToCharArray()[0];

        if (myTarget.headerName != newHeaderName || headerPrefix != newheaderPrefix || myTarget.prefixLength != newPrefixLength)
        {
            myTarget.headerName = newHeaderName;
            headerPrefix = newheaderPrefix;
            myTarget.prefixLength = newPrefixLength;

            myTarget.gameObject.name = GetHeaderName(myTarget.headerName, myTarget.prefixLength);
        }
    }

    [MenuItem("GameObject/Create Header", false, 0)]
    private static void CreateHeader()
    {
        GameObject newHeaderObj = new GameObject();

        //Makes sure the header is not saved in the build and the transform component is not visable
        newHeaderObj.hideFlags = HideFlags.DontSaveInBuild;
        newHeaderObj.transform.hideFlags = HideFlags.DontSaveInBuild | HideFlags.NotEditable | HideFlags.HideInInspector;

        //Set the new name of the header
        HierachySeperator newHeader = newHeaderObj.AddComponent(typeof(HierachySeperator)) as HierachySeperator;

        newHeader.name = GetStaticHeaderName("New Header", 0.5f);

        Selection.SetActiveObjectWithContext(newHeaderObj, newHeaderObj);
    }

    public static GameObject CreateHeaderReturns()
    {
        GameObject newHeaderObj = new GameObject();

        //Makes sure the header is not saved in the build and the transform component is not visable
        newHeaderObj.hideFlags = HideFlags.DontSaveInBuild;
        newHeaderObj.transform.hideFlags = HideFlags.DontSaveInBuild | HideFlags.NotEditable | HideFlags.HideInInspector;

        //Set the new name of the header
        HierachySeperator newHeader = newHeaderObj.AddComponent(typeof(HierachySeperator)) as HierachySeperator;

        newHeader.name = GetStaticHeaderName("New Header", 0.5f);

        Selection.SetActiveObjectWithContext(newHeaderObj, newHeaderObj);

        return newHeaderObj;
    }

    private static void RenameHeader()
    {
        EditorApplication.ExecuteMenuItem("Window/General/Hierarchy"); // focus on the hierarchy window
        EditorWindow.focusedWindow.SendEvent(EditorGUIUtility.CommandEvent("Rename"));
    }    


    private static string GetStaticHeaderName()
    {
        int prefixLength = (int)(headerNameMaxLength * 0.5f);

        char thisHeaderPrerfix = '━';

        string prefixString = new string(thisHeaderPrerfix, prefixLength);

        StringBuilder newbuilder = new StringBuilder();
        newbuilder.Append(new string(thisHeaderPrerfix, prefixLength));
        newbuilder.Append(" New_Header ");
        newbuilder.Append(new string(thisHeaderPrerfix, prefixLength));

        return newbuilder.ToString();
    }

    private static string GetStaticHeaderName(string a_newName, float a_multiplyer)
    {
        int prefixLength = (int)(headerNameMaxLength * a_multiplyer);

        char thisHeaderPrerfix = '━';

        //a_newName = a_newName.Replace(" ", "_");

        StringBuilder newbuilder = new StringBuilder();
        newbuilder.Append(new string(thisHeaderPrerfix, prefixLength));
        newbuilder.Append(" ");
        newbuilder.Append(a_newName.ToUpper());
        newbuilder.Append(" ");
        newbuilder.Append(new string(thisHeaderPrerfix, prefixLength));

        return newbuilder.ToString();
    }

    public string GetHeaderName(string a_newName, float a_multiplyer)
    {
        int nameLength = headerNameMaxLength - a_newName.Length;
        int prefixLength = (int)(nameLength * a_multiplyer);

        string prefixString = new string(headerPrefix, prefixLength);

        StringBuilder newbuilder = new StringBuilder();
        newbuilder.Append(prefixString);
        newbuilder.Append(" ");
        newbuilder.Append(a_newName.ToUpper());
        newbuilder.Append(" ");
        newbuilder.Append(prefixString);

        return newbuilder.ToString();
    }


#if UNITY_PRO_LICENSE

    [MenuItem("Assets/Create/Scene %9")]
    private static void CreateNewScene()
    {
        Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        newScene.name = "New Seperatorz Scene";
        

        string[] allPaths = AssetDatabase.FindAssets("SeperatorzObjSettings");

        for (int i = 0; i < allPaths.Length; i++)
        {
            allPaths[i] = AssetDatabase.GUIDToAssetPath(allPaths[i]);
        }

        SeperatorzSettings seperatorzSettings = AssetDatabase.LoadAssetAtPath(allPaths[0], typeof(SeperatorzSettings)) as SeperatorzSettings;
        if (seperatorzSettings)
        {
            for (int i = 0; i < seperatorzSettings.defaultSeperatorzNames.Length; ++i)
            {
                GameObject newHeader = HierachySeperatorEditor.CreateHeaderReturns();
                newHeader.name = GetStaticHeaderName(seperatorzSettings.defaultSeperatorzNames[i], 0.5f);
                newHeader.GetComponent<HierachySeperator>().headerName = seperatorzSettings.defaultSeperatorzNames[i];
            }
        }
        else
        {
            DebugZ.instance.LogError("There is no SeperatorzSettings Object in your project, add one for true customisation");
        }

        string scenePath = AssetDatabase.GenerateUniqueAssetPath("Assets/" + newScene.name + ".unity");
        EditorSceneManager.SaveScene(newScene, scenePath);
        EditorSceneManager.OpenScene(scenePath);
    }


#else //endif UNITY_PRO_LICENSE

    [MenuItem("Assets/Create/Scene (Seperatorz)", false, 5)]
    private static void CreateNewScene()
    {
        Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        newScene.name = "New Seperatorz Scene";
        

        string[] allPaths = AssetDatabase.FindAssets("SeperatorzObjSettings");

        for (int i = 0; i < allPaths.Length; i++)
        {
            allPaths[i] = AssetDatabase.GUIDToAssetPath(allPaths[i]);
        }

        SeperatorzSettings seperatorzSettings = AssetDatabase.LoadAssetAtPath(allPaths[0], typeof(SeperatorzSettings)) as SeperatorzSettings;
        if (seperatorzSettings)
        {
            for (int i = 0; i < seperatorzSettings.defaultSeperatorzNames.Length; ++i)
            {
                GameObject newHeader = HierachySeperatorEditor.CreateHeaderReturns();
                newHeader.name = GetStaticHeaderName(seperatorzSettings.defaultSeperatorzNames[i], 0.5f);
                newHeader.GetComponent<HierachySeperator>().headerName = seperatorzSettings.defaultSeperatorzNames[i];
            }
        }
        else
        {
            DebugZ.instance.LogError("There is no SeperatorzSettings Object in your project, add one for true customisation");
        }

        string scenePath = AssetDatabase.GenerateUniqueAssetPath("Assets/" + newScene.name + ".unity");
        EditorSceneManager.SaveScene(newScene, scenePath);
        EditorSceneManager.OpenScene(scenePath);
    }

#endif //UNITY_FREE_LICENSE


}

#endif //UNITY_EDITOR