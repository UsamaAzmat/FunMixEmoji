using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using _Scripts.Panels;


public class ShowAndHidePanels : MonoBehaviour
{
    [EnumPaging] public PanelShowOrHide panelType;
    public string PanelId;
    [EnumPaging] public PanelShowBehaviour Behaviour;
    private PanelManager _panelManager;
    private void Start()
    {
        // Cache the manager
        _panelManager = PanelManager.Instance;
        //GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(DoShowPanel);
    }

    public void DoShowPanel()
    {
        SoundsManager.instSound?.PlayTheSound(soundType.click);
        UIManager.Instance.UpdateClicks();
        switch (panelType)
        {
            case PanelShowOrHide.Show:
                _panelManager.ShowPanel(PanelId, Behaviour);

                break;
            case PanelShowOrHide.Hide:
                SoundsManager.instSound?.StopLoopedSound();
                _panelManager.HideLastPanel();
                break;
        }


        // Show the panel
    }
}
