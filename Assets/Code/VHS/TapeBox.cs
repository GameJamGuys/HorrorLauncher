using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeBox : MonoBehaviour
{
    [SerializeField] VideoTapeSO _tapeData;

    public VideoTapeSO TapeData => _tapeData;

    SetOutline setOutline;

    [SerializeField]
    TapePickUp tapePickUp;

    private void Start()
    {
        setOutline = GetComponent<SetOutline>();
    }

    private void OnMouseEnter()
    {
        if(!tapePickUp.isHold)
            setOutline.Show(true);
    }

    private void OnMouseExit()
    {
        setOutline.Show(false);
    }
}
