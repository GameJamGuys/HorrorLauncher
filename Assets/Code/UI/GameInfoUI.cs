using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoUI : MonoBehaviour
{
    [SerializeField] GameObject holder;
    [SerializeField] TMP_Text gameTitle;
    
    void Start()
    {
        HideGameInfo();
    }

    public void SetGameInfo(VideoTapeSO data)
    {
        holder.SetActive(true);
        gameTitle.text = data.gameName;
    }

    public void HideGameInfo() => holder.SetActive(false);
}
