using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class VideoPlayer : MonoBehaviour
{ 
    public void InsertTape(VideoTapeSO tapeData)
    {
        Debug.Log("Put in video tape: " + tapeData.gameName);

        GameLauncher.Instance.LaunchGame(tapeData);
    }
}
