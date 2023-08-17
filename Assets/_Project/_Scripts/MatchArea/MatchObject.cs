using System.Collections.Generic;
using UnityEngine;

public class MatchObject : MonoBehaviour
{
    public struct LRObject
    {
        public GameObject obj1, obj2;
        public bool match;
    }
    public List<LRObject> connectedObj = new List<LRObject>();
    [HideInInspector] public int ConnectedObjCount = 0;



    void Start()
    {
        ConnectedObjCount = 0;
    }

    public void AddToObjList(GameObject go)
    {
        int PreExistingIndex = CheckIfGameobjectPreExists(go);

        if (PreExistingIndex != -1)
        {
            var tempStruct = connectedObj[PreExistingIndex];
            connectedObj.RemoveAt(PreExistingIndex);
            LevelManager.Instance._Connector.RemoveLine(PreExistingIndex);
            if (ConnectedObjCount > 0 && tempStruct.obj2 != null)
            {
                ConnectedObjCount--;
            }
        }


        if (!CheckForExistingIncompleteList())
        {
            var tempStruct = new LRObject();
            tempStruct.obj1 = go;
            tempStruct.obj2 = null;
            tempStruct.match = false;
            connectedObj.Add(tempStruct);
            LevelManager.Instance._Connector.StartConnector(go);
            go.GetComponent<SelectOnClick>().EnableOutline();
        }
        else
        {
            if (CheckIfDiffSideObject(go))
            {
                var tempStruct = connectedObj[ConnectedObjCount];
                connectedObj.Remove(tempStruct);
                tempStruct.obj2 = go;
                go.GetComponent<SelectOnClick>().EnableOutline();
                tempStruct.match = CheckForMatch(tempStruct) ? true : false;
                connectedObj.Add(tempStruct);
                LevelManager.Instance._Connector.DrawConnector(ConnectedObjCount);
                ConnectedObjCount++;
            }
            else
            {
                var tempStruct = connectedObj[ConnectedObjCount];
                if (connectedObj[ConnectedObjCount].obj1 != go)
                    connectedObj[ConnectedObjCount].obj1.GetComponent<SelectOnClick>().DisableOutline();
                connectedObj.Remove(tempStruct);
                tempStruct = new LRObject();
                tempStruct.obj1 = go;
                tempStruct.obj2 = null;
                tempStruct.match = false;
                connectedObj.Add(tempStruct);
                LevelManager.Instance._Connector.StartConnector(go);
            }
        }
    }

    public int CheckIfGameobjectPreExists(GameObject go)
    {
        for (int i = 0; i < ConnectedObjCount; i++)
        {
            if (connectedObj[i].obj1 == go)
            {
                go.GetComponent<SelectOnClick>().DisableOutline();
                if (connectedObj[i].obj2 != null)
                    connectedObj[i].obj2.GetComponent<SelectOnClick>().DisableOutline();
                return i;
            }
            else if (connectedObj[i].obj2 == go)
            {
                if (connectedObj[i].obj1 != null)
                    connectedObj[i].obj1.GetComponent<SelectOnClick>().DisableOutline();
                go.GetComponent<SelectOnClick>().DisableOutline();
                return i;
            }
        }
        return -1;
    }

    public bool CheckForExistingIncompleteList()
    {
        if (connectedObj.Count > 0 && connectedObj[connectedObj.Count - 1].obj1 != null && connectedObj.Count > ConnectedObjCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfDiffSideObject(GameObject go)
    {
        if ((connectedObj[ConnectedObjCount].obj1.GetComponent<SelectOnClick>().objectInRight && !go.GetComponent<SelectOnClick>().objectInRight) ||
            (!connectedObj[ConnectedObjCount].obj1.GetComponent<SelectOnClick>().objectInRight && go.GetComponent<SelectOnClick>().objectInRight))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckForMatch(LRObject co)
    {
        if (co.obj1.name == co.obj2.name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
