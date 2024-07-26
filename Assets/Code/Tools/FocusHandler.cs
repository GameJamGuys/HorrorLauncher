using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusHandler : MonoBehaviour
{
    public bool isPause;

    private void OnApplicationFocus(bool focus)
    {
        CheckForPause(focus);
    }

    private void OnApplicationPause(bool pause)
    {
        CheckForPause(pause);
    }

    private void CheckForPause(bool pause)
    {
        isPause = pause;

        AudioListener.pause = !isPause;
        Time.timeScale = isPause ? 1 : 0;
    }

}
