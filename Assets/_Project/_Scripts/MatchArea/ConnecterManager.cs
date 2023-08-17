using System.Collections.Generic;
using UnityEngine;

public class ConnecterManager : MonoBehaviour
{
    public Material RedLine, GreenLine;
    [HideInInspector] public List<GameObject> ConnectorLines;
    [HideInInspector] public List<Vector3> ConnectingPointsLeft;
    [HideInInspector] public List<Vector3> ConnectingPointsRight;
    [HideInInspector] public List<GameObject> LeftObjects;
    [HideInInspector] public List<GameObject> RightObjects;
    [HideInInspector] public bool AllCorrectMatch;

    GameObject ConnectorContainer;
    Vector3 mousPos;
    Touch touch;
    public GameObject myLine;
    Color connectorColor;
    LineRenderer lr;
    Vector3 lineStartPos;
    Vector3 lineEndPos;
    bool LineSetup = false;
    GameObject currentGameobject;


    void Start()
    {
        ConnectorContainer = new GameObject("ConnectorHolder");
        ConnectorContainer.transform.parent = LevelManager.Instance._CurrentLevel.transform;
        AllCorrectMatch = true;
    }

    void Update()
    {
        if (currentGameobject != null)
        {
            if (currentGameobject.GetComponent<SelectOnClick>().startLine && LineSetup)
            {
                TraceConnector();
            }
            if (!currentGameobject.GetComponent<SelectOnClick>().startLine && LineSetup)
            {
                StartConnector(currentGameobject);
            }
        }
    }

    private void SetUpLine()
    {
        GameObject lineRefObject;
        ColorUtility.TryParseHtmlString("#06ff00", out connectorColor);
        lineRefObject = (GameObject)Instantiate(myLine);
        lineRefObject.transform.position = Vector3.zero;
        lr = lineRefObject.GetComponent<LineRenderer>();
        lr.startColor = connectorColor;
        lr.endColor = connectorColor;
    }

    public void StartConnector(GameObject go)
    {
        currentGameobject = go;
        if (!LineSetup)
        {
            SetUpLine();
        }
        lr.SetPosition(0, go.GetComponent<SelectOnClick>().connectingPoint);
        lr.SetPosition(1, go.GetComponent<SelectOnClick>().connectingPoint);
        LineSetup = true;
    }

    public void EndConnector(GameObject go)
    {
        go.GetComponent<SelectOnClick>().startLine = false;
        LineSetup = false;
        Debug.Log(lr.GetPosition(1));
        lr.SetPosition(1, go.GetComponent<SelectOnClick>().connectingPoint);
        Debug.Log(lr.GetPosition(1));
        lr.gameObject.transform.name = "ConnectorLine";
        lr.gameObject.transform.SetParent(ConnectorContainer.transform);
        ConnectorLines.Add(lr.gameObject);
        if (ConnectorLines.Count == ConnectingPointsRight.Count)
        {
            SetConnectorColor();
        }

    }

    public void TraceConnector()
    {
        float DistToObject;
        GetMousePositionToWorldPoint();
        if (currentGameobject.GetComponent<SelectOnClick>().objectInRight)
        {
            if (mousPos.x > currentGameobject.GetComponent<SelectOnClick>().connectingPoint.x)
            {
                mousPos = new Vector3(currentGameobject.GetComponent<SelectOnClick>().connectingPoint.x + 0.1f, mousPos.y, 0);
            }

            lr.SetPosition(1, new Vector3(mousPos.x, mousPos.y, 0));
            for (int i = 0; i < ConnectingPointsLeft.Count; i++)
            {
                DistToObject = Vector3.Distance(mousPos, ConnectingPointsLeft[i]);
                if (DistToObject <= 0.2f)
                {
                    SoundsManager.instSound.HepticPlay(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                    LevelManager.Instance._MatchObject.AddToObjList(LeftObjects[i]);
                }
            }

        }
        else
        {
            if (mousPos.x < currentGameobject.GetComponent<SelectOnClick>().connectingPoint.x)
            {
                mousPos = new Vector3(currentGameobject.GetComponent<SelectOnClick>().connectingPoint.x - 0.1f, mousPos.y, 0);
            }

            lr.SetPosition(1, new Vector3(mousPos.x, mousPos.y, 0));
            for (int i = 0; i < ConnectingPointsRight.Count; i++)
            {
                DistToObject = Vector3.Distance(mousPos, ConnectingPointsRight[i]);
                if (DistToObject <= 0.2f)
                {
                    SoundsManager.instSound.HepticPlay(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                    LevelManager.Instance._MatchObject.AddToObjList(RightObjects[i]);
                }
            }
        }
    }

    public void DrawConnector(int index)
    {
        if (!LineSetup)
        {
            SetUpLine();
        }

        lr.gameObject.transform.name = "ConnectorLine";
        lr.gameObject.transform.SetParent(ConnectorContainer.transform);
        lr.SetPosition(0, LevelManager.Instance._MatchObject.connectedObj[index].obj1.GetComponent<SelectOnClick>().connectingPoint);

        lr.SetPosition(1, LevelManager.Instance._MatchObject.connectedObj[index].obj2.GetComponent<SelectOnClick>().connectingPoint);
        lr.GetPosition(1);
        ConnectorLines.Add(lr.gameObject);
        if (ConnectorLines.Count == ConnectingPointsRight.Count)
        {
            SetConnectorColor();
        }
        LineSetup = false;
    }

    public void SetConnectorColor()
    {
        for (int i = 0; i < ConnectorLines.Count; i++)
        {

            if (!LevelManager.Instance._MatchObject.connectedObj[i].match)
            {
                AllCorrectMatch = false;
                LineRenderer lr = ConnectorLines[i].GetComponent<LineRenderer>();
                lr.material = RedLine;
            }
            else
            {
                LineRenderer lr = ConnectorLines[i].GetComponent<LineRenderer>();
                lr.material = GreenLine;
            }

        }
        LevelManager.Instance._CurrentLevel.AllObjectsMatched(AllCorrectMatch);
    }

    private void GetMousePositionToWorldPoint()
    {
        mousPos = Input.mousePosition;
        mousPos = Camera.main.ScreenToWorldPoint(mousPos);
        mousPos = new Vector3(Mathf.Clamp(mousPos.x, ConnectingPointsLeft[0].x, ConnectingPointsRight[0].x), mousPos.y, 0);
    }
    public void RemoveLine(int index)
    {
        if (ConnectorLines.Count > 0 && index < ConnectorLines.Count)
        {
            if (ConnectorLines[index] != null)
            {
                Destroy(ConnectorLines[index]);
                ConnectorLines.RemoveAt(index);
            }
        }
    }
}
