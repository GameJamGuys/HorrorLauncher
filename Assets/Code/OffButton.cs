using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffButton : MonoBehaviour
{
    SetOutline setOutline;

    private void Start()
    {
        setOutline = GetComponent<SetOutline>();
    }

    private void OnMouseEnter()
    {
        setOutline.Show(true);
    }

    private void OnMouseExit()
    {
        setOutline.Show(false);
    }

    private void OnMouseDown()
    {
        GameLauncher.Instance.ExitLauncher();
    }
}
