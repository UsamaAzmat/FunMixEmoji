using UnityEngine;
using Sirenix.OdinInspector;

public class Vector2IntPrefsManager : MonoBehaviour
{
    public void SaveVector2IntToPrefs(Vector2Int vector, string addressKey)
    {
        string serializedVector = JsonUtility.ToJson(vector);
        PlayerPrefs.SetString(PlayerPrefsManager.favCount.ToString(), serializedVector);
    }

    public Vector2Int LoadVector2IntFromPrefs(string addressKey)
    {
        string savedSerializedVector = PlayerPrefs.GetString(addressKey);
        return JsonUtility.FromJson<Vector2Int>(savedSerializedVector);
    }

    //void Start()
    //{
    //    Vector2Int myVector = new Vector2Int(3, 4);
    //    string addressKey = "Address1";

    //    SaveVector2IntToPrefs(myVector, addressKey);

    //    Vector2Int loadedVector = LoadVector2IntFromPrefs(addressKey);
    //    Debug.Log("Loaded Vector: " + loadedVector);
    //}
}
