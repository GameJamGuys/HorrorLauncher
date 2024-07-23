using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class GameLauncher : PersistentSingleton<GameLauncher>
{
    public void LaunchGame(VideoTapeSO data)
    {
        Process.Start(Environment.CurrentDirectory + @"\Games\" + data.gamePath);
    }
}
