using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SplashScreen : MonoBehaviour
{

    [SerializeField] private GameObject loading;
    [SerializeField] private Slider slider;
    void Start()
    {
        //PlayerPrefsManager.tutorialPrefs = 4;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Invoke("Load", 0.1f);
    }
    public void Load()
    {
        StartCoroutine(LoadScene("GamePlay"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        loading.SetActive(true);

        // Start the slider animation coroutine
        yield return StartCoroutine(AnimateSlider(3f));
        RemoteValues.Instance.LogCustomEvent("Loading_Event");

        // Load the scene after the animation has completed
        AsyncOperation sync = SceneManager.LoadSceneAsync(sceneName);
        yield return sync;
    }

    IEnumerator AnimateSlider(float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(timer / duration);
            slider.value = progress;

            yield return null;
        }
    }


}