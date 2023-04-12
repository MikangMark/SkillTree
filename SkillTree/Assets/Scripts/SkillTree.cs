using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType { PASSIVE = 0, ACTIVE }
[System.Serializable]
public class SkillNode
{
    public string skillName; // ��ų �̸�
    public List<SkillNode> children; // �ڽ� ��ų ����Ʈ
    public int depth;//��ų �ɵ�
    public int request_level;
    public int request_sp;
    public SkillType type;
    public Image skill_Img;


    
    public SkillNode(string name)
    {
        skillName = name;
        children = new List<SkillNode>();
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
}
public class SkillTree : MonoBehaviour
{
    public SkillNode root; // ��Ʈ ��ų

    public SkillTree(string rootSkillName)
    {
        root = new SkillNode(rootSkillName);
    }

    // ��ų �߰�
    public void AddSkill(string parentSkillName, string newSkillName)
    {
        SkillNode parentNode = FindSkillNode(root, parentSkillName);

        if (parentNode != null)
        {
            SkillNode newSkill = new SkillNode(newSkillName);
            parentNode.AddChild(newSkill);
        }
    }

    // ��ų ����
    public void RemoveSkill(string skillName)
    {
        SkillNode parentNode = FindParentNode(root, skillName);

        if (parentNode != null)
        {
            SkillNode skillNode = FindSkillNode(parentNode, skillName);
            parentNode.RemoveChild(skillNode);
        }
    }

    // ��ų ��� �˻�
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

    // �θ� ��� �˻�
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
