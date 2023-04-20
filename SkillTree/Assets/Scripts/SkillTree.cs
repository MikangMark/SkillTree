using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType { PASSIVE = 0, ACTIVE }
[System.Serializable]
public class SkillNode//��Ʈ ��ų
{
    public int code;
    public string skillName; // ��ų �̸�
    public List<SkillNode> children; // �ڽ� ��ų ����Ʈ
    public int depth;//��ų �ɵ�
    public int rq_LV;
    public int rq_SP;
    public SkillType type;
    public Image skill_Img;
    public bool isRoot;
    public List<string> needSkill;
    public List<int> needSkillCode;

    public SkillNode(TestData.SkillData _data)
    {
        code = _data.code;
        skillName = _data.name;
        children = new List<SkillNode>();
        depth = _data.depth;
        rq_LV = _data.rq_LV;
        rq_SP = _data.rq_SP;
        type = _data.type;
        isRoot = _data.isRoot;
        needSkill = new List<string>();
        needSkillCode = new List<int>();
        needSkill = _data.needSkill;
        needSkillCode = _data.needSkillCode;
        
    }

    // �ڽ� ��ų �߰�
    public void AddChild(SkillNode child)
    {
        children.Add(child);
    }

    // �ڽ� ��ų ����
    public void RemoveChild(SkillNode child)
    {
        children.Remove(child);
    }

    public void InputData(SkillNode _data)
    {
        code = _data.code;
        skillName = _data.skillName;
        children = _data.children;
        depth = _data.depth;
        rq_LV = _data.rq_LV;
        rq_SP = _data.rq_SP;
        type = _data.type;
        isRoot = _data.isRoot;
        needSkill = _data.needSkill;
        needSkillCode = _data.needSkillCode;
    }
}
public class SkillTree//��Ʈ��ų����
{
    public SkillNode root; // ��Ʈ ��ų

    public SkillTree(TestData.SkillData rootSkill)
    {
        root = new SkillNode(rootSkill);
    }

    // ��ų �߰�
    public void AddSkill(TestData.SkillData parentSkillData, TestData.SkillData newSkillData)
    {
        SkillNode parentNode = FindSkillNode(root, parentSkillData.code);

        if (parentNode != null)
        {
            SkillNode newSkill = new SkillNode(newSkillData);
            parentNode.AddChild(newSkill);
        }
    }

    // ��ų ����
    public void RemoveSkill(int skillCode)
    {
        SkillNode parentNode = FindParentNode(root, skillCode);

        if (parentNode != null)
        {
            SkillNode skillNode = FindSkillNode(parentNode, skillCode);
            parentNode.RemoveChild(skillNode);
        }
    }

    // ��ų ��� �˻�
    private SkillNode FindSkillNode(SkillNode node, int skillCode)
    {
        if (node.code == skillCode)
        {
            return node;
        }

        foreach (SkillNode child in node.children)
        {
            SkillNode result = FindSkillNode(child, skillCode);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    // �θ� ��� �˻�
    private SkillNode FindParentNode(SkillNode node, int skillCode)
    {
        foreach (SkillNode child in node.children)
        {
            if (child.code == skillCode)
            {
                return node;
            }

            SkillNode result = FindParentNode(child, skillCode);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
