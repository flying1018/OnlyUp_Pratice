
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void ShowObjectInfo(string name)
    {
        nameText.text = name;
    }
}