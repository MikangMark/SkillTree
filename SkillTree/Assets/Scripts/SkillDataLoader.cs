using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataLoader : MonoBehaviour
{
    [System.Serializable]
    public struct SkillData
    {
        public int code;
        public string name;
        public SkillType type;
        public int rq_LV;
        public int rq_SP;
        public int depth;
        public List<string> needSkill;
        public List<int> needSkillCode;
        public bool isRoot;
    }
    [SerializeField]
    //private TestData data;
    public List<SkillData> items = new List<SkillData>();
    [SerializeField]
    public List<SkillTree> skillTree;//이거하나당 루트 스킬 하나(기본스킬)심도 0인스킬의 리스트 하위스킬연동되있는 리스트
    

    public Dictionary<int, SkillData> skillList;//모든 스킬코드, 스킬정보 
    public Dictionary<int, List<SkillData>> depthList;//심도에 따른 스킬리스트 같은심도끼리 리스트화되어있음 심도0은 루트스킬
    public List<SkillData> rootSkill;//루트 스킬 모음
    public int maxDepth = 0;
    SkillDataLoader()
    {
        skillList = new Dictionary<int, SkillData>();
        depthList = new Dictionary<int, List<SkillData>>();
        rootSkill = new List<SkillData>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetAllSkill();
        SetDepthList();
        SetRootSkill();
    }

    void SetAllSkill()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (maxDepth < items[i].depth)
            {
                maxDepth = items[i].depth;
            }
            skillList.Add(items[i].code, items[i]);
        }
    }
    void SetDepthList()
    {
        for (int i = 0; i <= maxDepth; i++)
        {
            depthList.Add(i, new List<SkillData>());
        }

        for (int i = 10001; i < 10001 + skillList.Count; i++)
        {
            for (int j = 0; j <= maxDepth; j++)
            {
                if (skillList[i].depth == j)
                {
                    depthList[j].Add(skillList[i]);
                    break;
                }
            }
            if (skillList[i].isRoot == true)
            {
                rootSkill.Add(skillList[i]);
            }
        }
    }
    void SetRootSkill()
    {
        skillTree = new List<SkillTree>();
        for (int i = 0; i < rootSkill.Count; i++)//루트스킬 생성
        {
            skillTree.Add(new SkillTree(rootSkill[i]));
        }

        for (int i = 10001; i < 10001 + skillList.Count; i++)
        {
            if (skillList[i].isRoot == false)
            {
                for (int j = 0; j < skillList[i].needSkillCode.Count; j++)
                {
                    for (int k = 10001; k < 10001 + skillList.Count; k++)
                    {
                        if (skillList[i].needSkillCode[j] == skillList[k].code)
                        {
                            for (int l = 0; l < skillTree.Count; l++)
                            {
                                skillTree[l].AddSkill(skillList[k], skillList[i]);
                            }
                        }
                    }
                }
            }
        }
    }
}
