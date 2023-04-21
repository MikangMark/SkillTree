using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TestData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<SkillDataLoader.SkillData> items = new List<SkillDataLoader.SkillData>();
    [HideInInspector]
    public SkillDataLoader dataLoader;
    public List<string> Names = new List<string>();

    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        SkillDataLoader.SkillData temp;
        temp = new SkillDataLoader.SkillData();
        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "SkillCode":
                    temp.code = int.Parse(list[i].value);
                    break;
                case "SkillName":
                    temp.name = list[i].value;
                    break;
                case "SkillType":
                    System.Enum.TryParse(list[i].value, out temp.type);
                    break;
                case "Request_Level":
                    temp.rq_LV = int.Parse(list[i].value);
                    break;
                case "Request_Sp":
                    temp.rq_SP = int.Parse(list[i].value);
                    break;
                case "Depth":
                    temp.depth = int.Parse(list[i].value);
                    break;
                case "NeedSkill":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    string[] text = list[i].value.Split('/');
                    temp.needSkill = new List<string>();
                    for(int j = 0; j < text.Length; j++)
                    {
                        temp.needSkill.Add(text[j]);
                    }
                    break;
                case "IsRoot":
                    switch (list[i].value)
                    {
                        case "TRUE":
                            temp.isRoot = true;
                            break;

                        case "FALSE":
                            temp.isRoot = false;
                            break;
                    }
                    break;
                case "NeedSkillCode":
                    if (list[i].value.Equals(""))
                    {
                        break;
                    }
                    string[] value = list[i].value.Split('/');
                    temp.needSkillCode = new List<int>();
                    for(int j = 0; j < value.Length; j++)
                    {
                        temp.needSkillCode.Add(int.Parse(value[j]));
                    }
                    break;
            }
        }
        items.Add(temp);
    }

}

[CustomEditor(typeof(TestData))]
public class DataEditor : Editor
{
    TestData data;

    void OnEnable()
    {
        data = (TestData)target;
        data.dataLoader = GameObject.Find("DataLoader").GetComponent<SkillDataLoader>();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data Examples");

        if (GUILayout.Button("Pull Data Method One"))
        {
            UpdateStats(UpdateMethodOne);
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        data.items.Clear();
        foreach (string dataName in data.Names)
        {
            data.UpdateStats(ss.rows[dataName], dataName);
        }
        data.dataLoader.items = data.items;
        EditorUtility.SetDirty(target);
        
    }
    
}