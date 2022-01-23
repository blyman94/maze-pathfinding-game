using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the scene flow of the application. Also allows the application to
/// be quit.
/// </summary>
public class ApplicationManager : MonoBehaviour
{
    /// <summary>
    /// UI Element used to block scene transisitions.
    /// </summary>
    [Tooltip("UI Element used to block scene transisitions.")]
    [SerializeField] private CanvasGroupRevealer sceneTransitioner;

    /// <summary>
    /// Visualizer of the algorithm in scene.
    /// </summary>
    [Tooltip("Visualizer of the algorithm in scene.")]
    [SerializeField] private AlgorithmVisualizer visualizer;

    /// <summary>
    /// The current scene of the application.
    /// </summary>
    private Scene currentScene;

    #region MonoBehaviorMethods
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
    #endregion

    /// <summary>
    /// Loads a new scene of the application based on a string.
    /// </summary>
    /// <param name="sceneName">String representing the name of the scene
    /// to be loaded.</param>
    public void LoadNewScene(string sceneName)
    {
        StartCoroutine(LoadNewSceneRoutine(sceneName));
    }

    /// <summary>
    /// Reloads the current scene.
    /// </summary>
    public void ReloadScene()
    {
        StartCoroutine(ReloadSceneRoutine());
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Coroutine to load a new scene. Waits for the transition to finish before
    /// loading the scene.
    /// </summary>
    /// <param name="sceneName">String representing the name of the scene
    /// to be loaded.</param>
    /// <returns>IEnumerator used to suspend method execution.</returns>
    private IEnumerator LoadNewSceneRoutine(string sceneName)
    {
        sceneTransitioner.ShowGroup();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(sceneName);
    }

    /// <summary>
    /// Starts a coroutine to load the scene.
    /// </summary>
    /// <param name="scene">Name of scene.</param>
    /// <param name="loadSceneMode">Load scene mode.</param>
    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(OnSceneLoadRoutine(scene));
    }

    /// <summary>
    /// Routine executed when the scene loads.
    /// </summary>
    /// <param name="scene">Loaded scene.</param>
    /// <returns>IEnumerator used to suspend method execution.</returns>
    private IEnumerator OnSceneLoadRoutine(Scene scene)
    {
        sceneTransitioner.HideGroup();
        currentScene = scene;
        yield return new WaitForSeconds(1.0f);
        visualizer.StartVisualizer();
    }

    /// <summary>
    /// Routine to reload the current scene.
    /// </summary>
    /// <returns>IEnumerator used to suspend method execution.</returns>
    private IEnumerator ReloadSceneRoutine()
    {
        sceneTransitioner.ShowGroup();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(currentScene.buildIndex);
    }
}
