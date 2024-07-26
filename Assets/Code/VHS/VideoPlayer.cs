using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class VideoPlayer : MonoBehaviour
{
    SetOutline setOutline;

    [SerializeField]
    TapePickUp tapePickUp;

    private void Start()
    {
        setOutline = GetComponent<SetOutline>();
    }

    public void InsertTape(VideoTapeSO tapeData)
    {
        Debug.Log("Put in video tape: " + tapeData.gameName);

        GameLauncher.Instance.LaunchGame(tapeData);
    }

    private void OnMouseEnter()
    {
        if (tapePickUp.isHold)
            setOutline.Show(true);
    }

    private void OnMouseExit()
    {
        setOutline.Show(false);
    }
}
