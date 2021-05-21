using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private void OnMouseEnter()
    {
        HoverTextBox.ShowTooltip_Static("tesT");
    }

    private void OnMouseExit()
    {
        HoverTextBox.HideTooltip_Static();
    }
}
