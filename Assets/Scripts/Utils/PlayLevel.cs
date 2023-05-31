using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SaveManager;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;
    
    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;
    }

    public void OnLoad(SaveSetup setup)
    {
        uiTextName.text = "Play " + setup.lastLevel + 1;
    }

    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
}
