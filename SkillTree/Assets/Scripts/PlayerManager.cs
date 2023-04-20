using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int level;
    public int skillPoint;

    public List<SkillNode> learnSkillList;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        skillPoint = 0;
        learnSkillList = new List<SkillNode>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
