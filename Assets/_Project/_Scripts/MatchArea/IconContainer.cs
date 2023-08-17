using UnityEngine;

public class IconContainer : MonoBehaviour
{
    public GameObject outlinePrefab;
    [Header("Check if the object is the left set")]
    public bool isLeftObjects, isFirst;

    [HideInInspector] public float outlineWidth;

    GameObject outline;
    GameObject outlineContainer, tempContainer;



    void Start()
    {
        Vector3 outlineSize = outlinePrefab.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 tempConnectingPoint = new Vector3();
        outlineWidth = outlineSize.x;
        Color tmpColor;
        outlineContainer = new GameObject("AllOutline");
        outlineContainer.transform.parent = LevelManager.Instance._CurrentLevel.transform;
        if (isLeftObjects)
        {
            tempContainer = new GameObject("LeftSide");
            tempContainer.transform.parent = LevelManager.Instance._CurrentLevel.transform;
        }
        else
        {
            tempContainer = new GameObject("RightSide");
            tempContainer.transform.parent = LevelManager.Instance._CurrentLevel.transform;
        }
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (isLeftObjects)
            {
                LevelManager.Instance._CurrentLevel.LeftIconPositions.Add(this.transform.GetChild(i).transform.position);
            }
            else
            {
                LevelManager.Instance._CurrentLevel.RightIconPositions.Add(this.transform.GetChild(i).transform.position);
            }

        }
        //if (!isFirst)
        if (PlayerPrefsManager.tutorialPrefs > 0)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (isLeftObjects)
                {
                    int randomIndex = Random.Range(0, LevelManager.Instance._CurrentLevel.LeftIconPositions.Count);
                    this.transform.GetChild(i).transform.position = LevelManager.Instance._CurrentLevel.LeftIconPositions[randomIndex];
                    LevelManager.Instance._CurrentLevel.LeftIconPositions.RemoveAt(randomIndex);
                }
                else
                {
                    int randomIndex = Random.Range(0, LevelManager.Instance._CurrentLevel.RightIconPositions.Count);
                    this.transform.GetChild(i).transform.position = LevelManager.Instance._CurrentLevel.RightIconPositions[randomIndex];
                    LevelManager.Instance._CurrentLevel.RightIconPositions.RemoveAt(randomIndex);
                }

            }
        }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (isLeftObjects)
            {
                tempConnectingPoint = new Vector3((this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().transform.position.x + outlineWidth / 2) - 0.07f, this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().transform.position.y, 0);
                LevelManager.Instance._Connector.LeftObjects.Add(this.transform.GetChild(i).gameObject);
                this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().objectInRight = false;
                this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().connectingPoint = tempConnectingPoint;
                LevelManager.Instance._Connector.ConnectingPointsLeft.Add(tempConnectingPoint);
                this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().minScale = outlinePrefab.transform.localScale;
            }
            else
            {
                tempConnectingPoint = new Vector3((this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().transform.position.x - outlineWidth / 2) + 0.07f, this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().transform.position.y, 0);
                LevelManager.Instance._Connector.RightObjects.Add(this.transform.GetChild(i).gameObject);
                this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().objectInRight = true;
                this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().connectingPoint = tempConnectingPoint;
                LevelManager.Instance._Connector.ConnectingPointsRight.Add(tempConnectingPoint);
                this.transform.GetChild(i).gameObject.GetComponent<SelectOnClick>().minScale = outlinePrefab.transform.localScale;
            }
            outline = (GameObject)Instantiate(outlinePrefab, this.transform.GetChild(i).gameObject.transform.position, Quaternion.identity);
            tmpColor = outline.GetComponent<SpriteRenderer>().color;
            tmpColor.a = 0f;
            outline.GetComponent<SpriteRenderer>().color = tmpColor;
            outline.transform.name = "Outline";
            outline.transform.SetParent(outlineContainer.transform);

        }

        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            this.transform.GetChild(i).gameObject.transform.SetParent(outlineContainer.transform.GetChild(i).gameObject.transform);
            outlineContainer.transform.GetChild(i).gameObject.transform.SetParent(tempContainer.transform);
        }
        Destroy(outlineContainer);
        Destroy(this.gameObject);
    }
}
