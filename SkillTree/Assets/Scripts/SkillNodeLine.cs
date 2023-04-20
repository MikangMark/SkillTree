using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNodeLine : MonoBehaviour
{
	public List<GameObject> target;//연결할 오브젝트 
	public List<LineRenderer> lineObj;//라인렌더러 오브젝트
	public LineRenderer linePfab;
	private void Start()
	{
		StartCoroutine(CreateLine());
	}

	IEnumerator CreateLine()
	{
		yield return null;

		if (target.Count <= 0)
		{
			yield break;
		}

		lineObj = new List<LineRenderer>();

		for (int i = 0; i < target.Count; i++)
		{
			LineRenderer line = Instantiate<LineRenderer>(linePfab, transform);
			line.name = "[" + GetComponent<Skill>().thisSkill.code + "]2Line[" + target[i].GetComponent<Skill>().thisSkill.code + "]";
			lineObj.Add(line);

			line.useWorldSpace = true;
			line.SetPosition(0, transform.position);
			line.SetPosition(1, target[i].transform.position);
		}
	}
}
