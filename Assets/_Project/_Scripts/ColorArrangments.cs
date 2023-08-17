using UnityEngine.UI;
using UnityEngine;
using Sirenix.OdinInspector;
public class ColorArrangments : MonoBehaviour
{
    public bool isCorrect;
    [HideIf("isCorrect")] public Color[] randomColors;
    [ShowIf("isCorrect")] public Color[] actualColors;
    int currentColor;
    public Image imageColor;
    [SerializeField] float waitTime;
    [SerializeField] Color colorToShow;
    private void OnEnable()
    {
        if (isCorrect)
            DisplayCorrectColor();
        else
            Invoke("DisplayNextColor", waitTime);


        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(UpdateColor);
    }

    private void UpdateColor()
    {
        UIManager.Instance.UpdateClicks();
        if (isCorrect)
        {
            PlayerPrefs.SetString("RewardType", "ColorClick");
            RemoteValues.Instance.Show_MAX_RewardedVideo();
            DrawManager.ColortoShow = colorToShow;
            return;
        }
        EventsManager.ColorChange(colorToShow);
    }

    private void DisplayNextColor()
    {
        if (randomColors.Length == 0)
        {
            Debug.LogError("Color list is empty!");
            return;
        }

        int randomIndex = GetRandomColorIndex();

        // Check if all colors have been displayed, reset if needed
        if (DrawManager.displayedColors.Count == randomColors.Length)
        {
            DrawManager.displayedColors.Clear();
        }

        while (DrawManager.displayedColors.Contains(randomIndex))
        {
            randomIndex = GetRandomColorIndex();
        }

        DrawManager.displayedColors.Add(randomIndex);
        colorToShow = randomColors[randomIndex];
        imageColor.color = colorToShow;
    }


    public void DisplayCorrectColor()
    {
        /// show Rewarded Ad here
        if (currentColor < actualColors.Length)
        {
            imageColor.color = actualColors[currentColor];
            colorToShow = actualColors[currentColor];
            currentColor++;
        }
        else
        {
            currentColor = 0;
        }
    }
    private int GetRandomColorIndex()
    {
        return Random.Range(0, randomColors.Length);
    }



    [Button]
    void addAlphaValue()
    {
        for (int i = 0; i < randomColors.Length; i++)
        {
            Color sspColor = randomColors[i];
            sspColor.a = 1;
            randomColors[i] = sspColor;
        }
        if (isCorrect)
        {
            for (int i = 0; i < actualColors.Length; i++)
            {
                Color sspColor = actualColors[i];
                sspColor.a = 1;
                actualColors[i] = sspColor;
            }
        }
    }
}
