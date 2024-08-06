using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class VideoPlayer : MonoBehaviour
{
    SetOutline setOutline;

    Outline outline;

    [SerializeField]
    TapePickUp tapePickUp;

    private void Start()
    {
        setOutline = GetComponent<SetOutline>();
        outline = GetComponent<Outline>();
    }

    public void InsertTape(VideoTapeSO tapeData)
    {
        Debug.Log("Put in video tape: " + tapeData.gameName);

        GameLauncher.Instance.LaunchGame(tapeData);
    }

    private void Update()
    {
        setOutline.Show(tapePickUp.isHold);
    }

    private void OnMouseEnter()
    {
        if (tapePickUp.isHold)
            outline.OutlineColor = Color.green;
    }

    private void OnMouseExit()
    {
        outline.OutlineColor = Color.cyan;
    }
}
