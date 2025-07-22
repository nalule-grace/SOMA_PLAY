using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;

    private bool isVisible = true;

    public void ToggleVisibility()
    {
        isVisible = !isVisible;
        panel.SetActive(isVisible);
    }
}