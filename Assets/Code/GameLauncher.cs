using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using UnityEngine.SceneManagement;

public class GameLauncher : PersistentSingleton<GameLauncher>
{
    public bool isPause;
    public bool isGamePlay;

    private void Start()
    {
        isGamePlay = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete)) ExitLauncher();
    }

    private void OnApplicationFocus(bool focus)
    {
        CheckForPause(focus);
        if (focus && isGamePlay)
            RestartMenu();
    }

    private void OnApplicationPause(bool pause)
    {
        CheckForPause(!pause);
    }

    private void CheckForPause(bool pause)
    {
        isPause = pause;

        AudioListener.pause = !isPause;
        Time.timeScale = isPause ? 1 : 0;
    }

    public void LaunchGame(VideoTapeSO data)
    {
        Process.Start(Environment.CurrentDirectory + @"\Games\" + data.gamePath);
        
        isGamePlay = true;
    }

    public void RestartMenu() => SceneManager.LoadScene(0);

    public void ExitLauncher() => Application.Quit();
}
