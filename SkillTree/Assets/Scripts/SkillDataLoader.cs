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
    public List<SkillTree> skillTree;//�̰��ϳ��� ��Ʈ ��ų �ϳ�(�⺻��ų)�ɵ� 0�ν�ų�� ����Ʈ ������ų�������ִ� ����Ʈ
    

    public Dictionary<int, SkillData> skillList;//��� ��ų�ڵ�, ��ų���� 
    public Dictionary<int, List<SkillData>> depthList;//�ɵ��� ���� ��ų����Ʈ �����ɵ����� ����Ʈȭ�Ǿ����� �ɵ�0�� ��Ʈ��ų
    public List<SkillData> rootSkill;//��Ʈ ��ų ����
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
        for (int i = 0; i < rootSkill.Count; i++)//��Ʈ��ų ����
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
