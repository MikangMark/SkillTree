using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType { PASSIVE = 0, ACTIVE }
[System.Serializable]
public class SkillNode//루트 스킬
{
    public int code;
    public string skillName; // 스킬 이름
    public List<SkillNode> children; // 자식 스킬 리스트
    public int depth;//스킬 심도
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

    // 자식 스킬 추가
    public void AddChild(SkillNode child)
    {
        children.Add(child);
    }

    // 자식 스킬 삭제
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
public class SkillTree//루트스킬관리
{
    public SkillNode root; // 루트 스킬

    public SkillTree(TestData.SkillData rootSkill)
    {
        root = new SkillNode(rootSkill);
    }

    // 스킬 추가
    public void AddSkill(TestData.SkillData parentSkillData, TestData.SkillData newSkillData)
    {
        SkillNode parentNode = FindSkillNode(root, parentSkillData.code);

        if (parentNode != null)
        {
            SkillNode newSkill = new SkillNode(newSkillData);
            parentNode.AddChild(newSkill);
        }
    }

    // 스킬 삭제
    public void RemoveSkill(int skillCode)
    {
        SkillNode parentNode = FindParentNode(root, skillCode);

        if (parentNode != null)
        {
            SkillNode skillNode = FindSkillNode(parentNode, skillCode);
            parentNode.RemoveChild(skillNode);
        }
    }

    // 스킬 노드 검색
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

    // 부모 노드 검색
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
