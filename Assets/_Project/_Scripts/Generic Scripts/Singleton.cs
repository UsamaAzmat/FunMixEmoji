using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// The instance
    private static T _instance;

    /// The instance property
    public static T Instance => _instance;

    /// Cast this to the instance
    public void Awake() => _instance = (T)(object)this;

}
