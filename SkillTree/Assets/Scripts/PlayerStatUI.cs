using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI spTxt;
    public Button lvUpBtn;
    public PlayerManager player;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        levelTxt.text = "LV:" + player.level;
        spTxt.text = "SP:" + player.skillPoint;
    }

    public void OnClickLvUpBtn()
    {
        player.level++;
        player.skillPoint++;
    }
}
