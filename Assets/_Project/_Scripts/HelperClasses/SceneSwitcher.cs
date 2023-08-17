using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
public class SceneSwitcher : MonoBehaviour
{
    public List<string> sceneNames = new List<string>();
    public List<UnityEngine.Object> sceneReferences = new List<UnityEngine.Object>();
    int index;
    [Button]
    public void SwitchScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneNames[index]);
        index++;
    }

    [Button]
    public void getScenesName()
    {
        sceneNames.Clear();
        foreach (var item in sceneReferences)
        {
            sceneNames.Add(item.name);
        }
    }
}
