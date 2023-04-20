using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillNode thisSkill;
    public PlayerManager player;
    public GameObject skillInfo;
    // Start is called before the first frame update
    void Start()
    {
        skillInfo = GameObject.Find("Skill_Info_Panel");
        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    public void SetSkill(SkillTree _data)
    {
        thisSkill.InputData(_data.root);
    }

    public void LearnSkill()
    {
        int count = 0;
        for (int i = 0; i < player.learnSkillList.Count; i++)
        {
            for (int k = 0; k < thisSkill.needSkillCode.Count; k++)
            {
                if (player.learnSkillList[i].code == thisSkill.needSkillCode[k])
                {
                    count++;
                }
            }
        }
        if(count != thisSkill.needSkillCode.Count)
        {
            return;
        }
        
        if(thisSkill.rq_LV<= player.level && thisSkill.rq_SP<=player.skillPoint)
        {
            player.skillPoint -= thisSkill.rq_SP;
            GetComponent<Image>().color = Color.green;
            if (thisSkill.children.Count > 0)
            {
                for(int i = 4;i< transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<LineRenderer>().startColor = Color.green;
                    transform.GetChild(i).GetComponent<LineRenderer>().endColor = Color.green;
                    
                }
            }
            player.learnSkillList.Add(thisSkill);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        skillInfo.SetActive(true);
        skillInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name:" + thisSkill.skillName;
        skillInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Type:" + thisSkill.type.ToString();
        skillInfo.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "필요레벨:" + thisSkill.rq_LV;
        skillInfo.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "필요Sp:" + thisSkill.rq_SP;
        string temp = "필요스킬:";
        for (int i = 0; i < thisSkill.needSkill.Count; i++)
        {
            temp += thisSkill.needSkill[i];
        }
        skillInfo.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = temp;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        skillInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name:";
        skillInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Type:";
        skillInfo.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "필요레벨:";
        skillInfo.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "필요Sp:";
        skillInfo.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "필요스킬:";
    }
}
