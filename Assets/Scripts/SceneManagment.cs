using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum CustomScenes
{
    MainMenu = 0,
    GameScene,
    DeathScreen
}

public class SceneManagment : MonoBehaviour
{
    public static SceneManagment instance;

    [SerializeField]
    private Animation blackoutAnim;
    [SerializeField]
    private AnimationClip blackoutFadeOutAnimClip;
    [SerializeField]
    private AnimationClip blackoutFadeInAnimClip;

    private CustomScenes currentLoadedLevel;

    private bool isSceneLoading = false;

    public UnityEvent onSceneChangedEvent;

    private bool isHolted = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //Instance Setup
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            DebugZ.instance.Log("Instance already exists, destroying object!");
            Destroy(this.gameObject);
        }

        blackoutAnim = GetComponentInChildren<Animation>();

    }

    public void SceneChanged()
    {
        onSceneChangedEvent.Invoke();
    }

    public CustomScenes GetScene()
    {
        return (CustomScenes)SceneManager.GetActiveScene().buildIndex;
    }

    public List<CustomScenes> GetScenes()
    {
        List<CustomScenes> sceneIndexs = new List<CustomScenes>();

        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            sceneIndexs.Add((CustomScenes)SceneManager.GetSceneAt(i).buildIndex);
        }

        return sceneIndexs;
    }

    public bool IsSceneLoading()
    {
        return isSceneLoading;
    }

    //loads a specified scene
    public void LoadScene(CustomScenes a_customScene, bool a_holdfade = false)
    {
        StartCoroutine(LoadSceneWithLoading(a_customScene, LoadSceneMode.Single, a_holdfade));
    }

    //We call this to load a scene with a loading screen
    private IEnumerator LoadSceneWithLoading(CustomScenes a_customScene,LoadSceneMode a_loadSceneMode = LoadSceneMode.Single, bool a_holdfade = false)
    {
        isSceneLoading = true;

        blackoutAnim.Play(blackoutFadeOutAnimClip.name);

        // Wait until the screen goes to black
        while (blackoutAnim.isPlaying)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync((int)currentLoadedLevel);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)a_customScene, a_loadSceneMode);
        asyncLoad.allowSceneActivation = false;
        

        // Wait until the asynchronous scene fully loads
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
        currentLoadedLevel = a_customScene;

        yield return new WaitForSeconds(1.5f);

        if (!a_holdfade)
        {
            blackoutAnim.Play(blackoutFadeInAnimClip.name);
        }

        isHolted = !a_holdfade;


        isSceneLoading = false;
        SceneChanged();
    }

    public void ContinueLoading()
    {
        if(isHolted)
        {
            blackoutAnim.Play(blackoutFadeInAnimClip.name);
            isHolted = false;
        }
    }

    //We call this to load two scene with a loading screen
    private IEnumerator LoadSceneWithLoading(CustomScenes a_customScene01, CustomScenes a_customScene02,
        LoadSceneMode a_loadSceneMode01 = LoadSceneMode.Single, LoadSceneMode a_loadSceneMode02 = LoadSceneMode.Single)
    {
        DebugZ.instance.LogWarning("Loading scenes: " + a_customScene01 + " | " + a_customScene02);

        isSceneLoading = true;

        blackoutAnim.Play(blackoutFadeOutAnimClip.name);

        // Wait until the screen goes to black
        while (blackoutAnim.isPlaying)
        {
            yield return null;
        }


        

        AsyncOperation asyncLoad01 = SceneManager.LoadSceneAsync((int)a_customScene01, a_loadSceneMode01);
        AsyncOperation asyncLoad02 = SceneManager.LoadSceneAsync((int)a_customScene02, a_loadSceneMode02);
        asyncLoad01.allowSceneActivation = false;
        asyncLoad02.allowSceneActivation = false;
        
        // Wait until the asynchronous scene fully loads
        while (asyncLoad01.progress < 0.9f && asyncLoad02.progress < 0.9f)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync((int)currentLoadedLevel);

        asyncLoad01.allowSceneActivation = true;
        asyncLoad02.allowSceneActivation = true;
        currentLoadedLevel = a_customScene02;

        yield return new WaitForSeconds(1.5f);

        blackoutAnim.Play(blackoutFadeInAnimClip.name);

        isSceneLoading = false;
        SceneChanged();
    }

}
