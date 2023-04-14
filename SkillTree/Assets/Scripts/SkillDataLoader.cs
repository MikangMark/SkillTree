using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataLoader : MonoBehaviour
{
    [SerializeField]
    private TestData data;
    private SkillTree skillTree;//이거하나당 루트 스킬 하나(기본스킬)
    

    public Dictionary<int, TestData.SkillData> skillList;//모든 스킬코드, 스킬정보 
    public Dictionary<int, List<TestData.SkillData>> depthList;//심도에 따른 스킬리스트 같은심도끼리 리스트화되어있음 심도0은 루트스킬

    int maxDepth = 0;
    SkillDataLoader()
    {
        skillList = new Dictionary<int, TestData.SkillData>();
        depthList = new Dictionary<int, List<TestData.SkillData>>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < data.items.Count; i++)
        {
            if (maxDepth < data.items[i].depth)
            {
                maxDepth = data.items[i].depth;
            }
            skillList.Add(data.items[i].code, data.items[i]);
        }

        for (int i = 0; i < maxDepth; i++)
        {
            depthList.Add(i, new List<TestData.SkillData>());
        }
        List<TestData.SkillData> temp;
        temp = new List<TestData.SkillData>();
        for (int i = 0; i < maxDepth + 1; i++)
        {
            for(int j = 0; j < data.items.Count; j++)
            {
                if (data.items[j].depth == i)
                {
                    temp.Add(data.items[j]);
                }
            }
            depthList[i] = temp;
        }
        Debug.Log(skillList.Count);
        Debug.Log(depthList.Count);
    }
}
