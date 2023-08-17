using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalRender : MonoBehaviour
{
    public GameObject galleryPrefab;
    public Transform content;
    public int poolSize = 10;  // Adjust the pool size as needed

    private List<GameObject> objectPool = new List<GameObject>();
    private int currentPoolIndex = 0; // Index to keep track of the next available object

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(galleryPrefab, content);
            obj.SetActive(false);  // Deactivate the object initially
            objectPool.Add(obj);
        }
    }

    private void OnEnable()
    {
        for (int i = 1; i <= PlayerPrefsManager.favCount; i++)
        {
            GameObject obj = GetNextAvailableObject();

            string concatenatedString = PlayerPrefs.GetString(i.ToString());

            string[] parts = concatenatedString.Split(',');

            if (parts.Length == 2)
            {
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);

                obj.GetComponent<EmojiInfo>().getFavSprite(x, y);
            }
        }
    }

    private GameObject GetNextAvailableObject()
    {
        GameObject obj = objectPool[PlayerPrefsManager.favCount];
        obj.SetActive(true);

        currentPoolIndex = (PlayerPrefsManager.favCount + 1) % poolSize;
        return obj;
    }

}







//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class finalRender : MonoBehaviour
//{
//    public GameObject galleryPrefab;
//    public Transform content;
//    List<EmojiInfo> temEmoji = new List<EmojiInfo>();
//    private void OnEnable()
//    {
//        for (int i = 1; i < PlayerPrefsManager.favCount; i++)
//        {
//            GameObject obj = Instantiate(galleryPrefab, content);

//            string concatenatedString = PlayerPrefs.GetString(i.ToString());

//            // Split the concatenated string using the comma separator
//            string[] parts = concatenatedString.Split(',');

//            if (parts.Length == 2)
//            {
//                int x = int.Parse(parts[0]);
//                int y = int.Parse(parts[1]);

//                // Now you have extracted the two integers from the concatenated string
//                Debug.LogError(x + " and " + y);
//                obj.GetComponent<EmojiInfo>().getFavSprite(x, y);
//            }
//        }
//    }
//}
