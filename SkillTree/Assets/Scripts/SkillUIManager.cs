using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillUIManager : MonoBehaviour
{
    public GameObject skillPfab;
    public GameObject depthPfab;
    public GameObject depthParent;

    public List<GameObject> depthList;

    [SerializeField]
    SkillDataLoader dataLoader;
    [SerializeField]
    List<GameObject> rootSkill;
    [SerializeField]
    List<GameObject> childSkill;

    public PlayerManager player;

    // Start is called before the first frame update
    void Start()
    {
        
        CreateDepthView();
        CreateRootSkill();
        CreateRootLine();
        AllSkillDisable();

    }
    private void Update()
    {
        if (player.level > 0)
        {
            AbleSkill(player.level);
        }
    }
    void CreateDepthView()
    {
        rootSkill = new List<GameObject>();
        depthList = new List<GameObject>();
        GameObject depthTemp = null;
        for (int i = 0; i <= dataLoader.maxDepth; i++)
        {
            depthTemp = Instantiate(depthPfab, depthParent.transform);
            depthTemp.name = "Skill_Depth[" + i + "]";
            depthList.Add(depthTemp);
        }
    }
    void CreateRootSkill()
    {
        childSkill = new List<GameObject>();
        for (int i = 0; i < dataLoader.skillTree.Count; i++)
        {
            SkillTree tree = dataLoader.skillTree[i];
            rootSkill.Add(Instantiate(skillPfab, depthList[tree.root.depth].transform));
            rootSkill[i].GetComponent<Skill>().SetSkill(tree);
            rootSkill[i].name = tree.root.code.ToString();

            for (int j = 0; j < tree.root.children.Count; j++)
            {
                SkillNode childNode = tree.root.children[j];

                PrintSkillTree(rootSkill[i], childNode);
            }
        }
    }
    /*
     ��� �� �����ڵ� ��ų������Ʈ�ϰ� �и���ų��
     */
    void PrintSkillTree(GameObject pobj,SkillNode node)//�ڽĳ�� ����
    {
        for(int i = 0; i < childSkill.Count; i++)//�ڽĳ�� ����
        {
            if(childSkill[i].GetComponent<Skill>().thisSkill.code == node.code)//�̹̻����� ��ų�ϰ��
            {
                //pobj.GetComponent<SkillNodeLine>().target.Add(childSkill[i]);
                return;
            }
        }
        childSkill.Add(Instantiate(skillPfab, depthList[node.depth].transform));
        childSkill[childSkill.Count - 1].GetComponent<Skill>().thisSkill = node;
        childSkill[childSkill.Count - 1].name = node.code.ToString();
        Debug.Log("������ �θ�:" + pobj.name + "������ �ڽ�:" + childSkill[childSkill.Count - 1].name);
        //pobj.GetComponent<SkillNodeLine>().target.Add(childSkill[childSkill.Count - 1]);//�� ������
        for (int i = 0; i < node.children.Count; i++)//�ڽ��� �ڽĳ�� ����
        {
            SkillNode childNode = node.children[i];
            for (int j = 0; j < childSkill.Count; j++)
            {
                if (childSkill[j].GetComponent<Skill>().thisSkill.code == childNode.code)//�̹̻����� ��ų�ϰ��
                {
                    //childSkill[j].GetComponent<SkillNodeLine>().target.Add(childSkill[j]);
                    return;
                }
            }
            childSkill.Add(Instantiate(skillPfab, depthList[childNode.depth].transform));
            childSkill[childSkill.Count - 1].GetComponent<Skill>().thisSkill = childNode;
            childSkill[childSkill.Count - 1].name = childNode.code.ToString();
            Debug.Log("������ �θ�:" + pobj.name + "������ �ڽ�:" + childSkill[childSkill.Count - 1].name);
            //childSkill[childSkill.Count - 1].GetComponent<SkillNodeLine>().target.Add(childSkill[childSkill.Count - 1]);
            PrintSkillTree(childSkill[childSkill.Count - 2], childNode);
        }
    }

    void CreateRootLine()
    {
        for (int i = 0; i < rootSkill.Count; i++)
        {
            for (int j = 0; j < rootSkill[i].GetComponent<Skill>().thisSkill.children.Count; j++)
            {
                GameObject childObj = GameObject.Find(rootSkill[i].GetComponent<Skill>().thisSkill.children[j].code.ToString());

                CreateChildLine(rootSkill[i], childObj);
            }
        }
    }
    void CreateChildLine(GameObject pobj, GameObject cobj)
    {
        pobj.GetComponent<SkillNodeLine>().target.Add(cobj);//�� ������
        for (int i = 0; i < cobj.GetComponent<Skill>().thisSkill.children.Count; i++)//�ڽ��� �ڽĳ�� ����
        {
            GameObject ccobj = GameObject.Find(cobj.GetComponent<Skill>().thisSkill.children[i].code.ToString());
            cobj.GetComponent<SkillNodeLine>().target.Add(ccobj);
            CreateChildLine(cobj, ccobj);
        }
    }

    void AllSkillDisable()
    {
        for(int i=0;i< rootSkill.Count; i++)
        {
            rootSkill[i].GetComponent<Button>().interactable = false;
        }
        for(int i = 0; i < childSkill.Count; i++)
        {
            childSkill[i].GetComponent<Button>().interactable = false;
        }
    }

    void AbleSkill(int level)
    {
        if(level> depthList.Count)
        {
            return;
        }
        for(int i = 0; i < level; i++)
        {
            for(int j = 0;j< depthList[i].transform.childCount; j++)
            {
                depthList[i].transform.GetChild(j).GetComponent<Button>().interactable = true;
            }
            
        }
    }

}
