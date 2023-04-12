using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType { PASSIVE = 0, ACTIVE }
[System.Serializable]
public class SkillNode
{
    public string skillName; // 스킬 이름
    public List<SkillNode> children; // 자식 스킬 리스트
    public int depth;//스킬 심도
    public int request_level;
    public int request_sp;
    public SkillType type;
    public Image skill_Img;


    
    public SkillNode(string name)
    {
        skillName = name;
        children = new List<SkillNode>();
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
}
public class SkillTree : MonoBehaviour
{
    public SkillNode root; // 루트 스킬

    public SkillTree(string rootSkillName)
    {
        root = new SkillNode(rootSkillName);
    }

    // 스킬 추가
    public void AddSkill(string parentSkillName, string newSkillName)
    {
        SkillNode parentNode = FindSkillNode(root, parentSkillName);

        if (parentNode != null)
        {
            SkillNode newSkill = new SkillNode(newSkillName);
            parentNode.AddChild(newSkill);
        }
    }

    // 스킬 삭제
    public void RemoveSkill(string skillName)
    {
        SkillNode parentNode = FindParentNode(root, skillName);

        if (parentNode != null)
        {
            SkillNode skillNode = FindSkillNode(parentNode, skillName);
            parentNode.RemoveChild(skillNode);
        }
    }

    // 스킬 노드 검색
    private SkillNode FindSkillNode(SkillNode node, string skillName)
    {
        if (node.skillName == skillName)
        {
            return node;
        }

        foreach (SkillNode child in node.children)
        {
            SkillNode result = FindSkillNode(child, skillName);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    // 부모 노드 검색
    private SkillNode FindParentNode(SkillNode node, string skillName)
    {
        foreach (SkillNode child in node.children)
        {
            if (child.skillName == skillName)
            {
                return node;
            }

            SkillNode result = FindParentNode(child, skillName);

            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
