using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataLoader : MonoBehaviour
{
    [SerializeField]
    private TestData data;
    [SerializeField]
    public List<SkillTree> skillTree;//�̰��ϳ��� ��Ʈ ��ų �ϳ�(�⺻��ų)�ɵ� 0�ν�ų�� ����Ʈ
    

    public Dictionary<int, TestData.SkillData> skillList;//��� ��ų�ڵ�, ��ų���� 
    public Dictionary<int, List<TestData.SkillData>> depthList;//�ɵ��� ���� ��ų����Ʈ �����ɵ����� ����Ʈȭ�Ǿ����� �ɵ�0�� ��Ʈ��ų
    public List<TestData.SkillData> rootSkill;//��Ʈ ��ų ����
    int maxDepth = 0;
    SkillDataLoader()
    {
        skillList = new Dictionary<int, TestData.SkillData>();
        depthList = new Dictionary<int, List<TestData.SkillData>>();
        rootSkill = new List<TestData.SkillData>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        SetAllSkill();
        SetDepthList();
        skillTree = new List<SkillTree>();
        for (int i = 0;i< rootSkill.Count; i++)//��Ʈ��ų ����
        {
            skillTree.Add(new SkillTree(rootSkill[i]));
        }

        for (int i = 10001; i < 10001 + skillList.Count; i++)
        {
            if (skillList[i].isRoot == false)
            {
                for(int j = 0; j < skillList[i].needSkillCode.Count; j++)
                {
                    for(int k = 10001; k < 10001 + skillList.Count; k++)
                    {
                        if (skillList[i].needSkillCode[j] == skillList[k].code)
                        {
                            for(int l = 0; l < skillTree.Count; l++)
                            {
                                skillTree[l].AddSkill(skillList[k], skillList[i]);
                            }
                        }
                    }
                }
            }
        }
    }

    void SetAllSkill()
    {
        for (int i = 0; i < data.items.Count; i++)
        {
            if (maxDepth < data.items[i].depth)
            {
                maxDepth = data.items[i].depth;
            }
            skillList.Add(data.items[i].code, data.items[i]);
        }
    }
    void SetDepthList()
    {
        for (int i = 0; i <= maxDepth; i++)
        {
            depthList.Add(i, new List<TestData.SkillData>());
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
}
