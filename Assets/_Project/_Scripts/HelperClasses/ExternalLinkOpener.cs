using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExternalLinkOpener : MonoBehaviour
{
    private Button openerBtn;
    [SerializeField] string URL;
    void Start()
    {
        openerBtn = GetComponent<Button>();
        openerBtn.onClick.RemoveAllListeners();
        openerBtn.onClick.AddListener(OpenExternalLink);
    }

    public void OpenExternalLink()
    {
        Application.OpenURL(URL);
    }
}
