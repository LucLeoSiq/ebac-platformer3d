using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGunUpdater : MonoBehaviour
{
    public Image uiImage;

    private void OnValidate()
    {
        if(uiImage == null) uiImage = GetComponent<Image>();
    }

    public void UpdateValue(float f)
    {
        uiImage.fillAmount = f;
    }

    public void UpdateValue(float max, float current)
    {
        uiImage.fillAmount = current / max;
    }
}
