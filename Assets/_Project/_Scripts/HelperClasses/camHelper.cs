using Sirenix.OdinInspector;
using UnityEngine;

public class camHelper : MonoBehaviour
{
    [Button]
    void addCamera()
    {
        Camera cam;
        this.gameObject.AddComponent<Camera>();
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.Depth;
        //cam.cullingMask = 3;
        cam.cullingMask = -1;
    }

    [Button]
    void removeCam()
    {
        DestroyImmediate(GetComponent<Camera>());
    }
}
