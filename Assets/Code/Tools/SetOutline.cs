using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOutline : MonoBehaviour
{
    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Show(bool isShow)
    {
        outline.enabled = isShow;
    }
}
