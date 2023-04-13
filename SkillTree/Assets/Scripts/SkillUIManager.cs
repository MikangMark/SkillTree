using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillUIManager : MonoBehaviour
{
    public GameObject skillDepth_Pfab;
    public GameObject skillDepth_Parent;
    public SkillDataLoader data;
    public List<GameObject> skillDepth_Pfab_List;
    // Start is called before the first frame update
    void Start()
    {
        skillDepth_Pfab_List = new List<GameObject>();
        /*for (int i = 0; i < data.endDepth; i++)
        {
            GameObject temp;
            temp = Instantiate(skillDepth_Pfab);
            temp.transform.parent = skillDepth_Parent.transform;
            temp.name = "Skill_Depth[" + i + "]";
            skillDepth_Pfab_List.Add(temp);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
