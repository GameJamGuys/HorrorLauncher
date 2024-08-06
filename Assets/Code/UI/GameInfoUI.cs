using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoUI : MonoBehaviour
{
    [SerializeField] GameObject holder;
    [SerializeField] TMP_Text gameTitle;
    [SerializeField] TMP_Text gameDesc;

    void Start()
    {
        HideGameInfo();
    }

    public void SetGameInfo(VideoTapeSO data)
    {
        holder.SetActive(true);
        gameTitle.text = data.gameName;
        gameDesc.text = data.gameDescription;
    }

    public void HideGameInfo() => holder.SetActive(false);
}
