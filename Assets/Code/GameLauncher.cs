using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using UnityEngine.SceneManagement;

public class GameLauncher : PersistentSingleton<GameLauncher>
{
    public bool isPause;
    public bool isGamePlay;

    [SerializeField]
    Animator fadeAnim;

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

    public async void LaunchGame(VideoTapeSO data)
    {
        fadeAnim.SetTrigger("fade");
        isGamePlay = true;

        await System.Threading.Tasks.Task.Delay(300);
        Process.Start(Environment.CurrentDirectory + @"\Games\" + data.gamePath);
    }

    public void RestartMenu() => SceneManager.LoadScene(0);

    public void ExitLauncher() => Application.Quit();
}
