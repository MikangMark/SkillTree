using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNodeLine : MonoBehaviour
{
    public GameObject target;//������ ������Ʈ 
    public LineRenderer lineRenderer; // Line Renderer ������Ʈ

    void Start()
    {
        // Line Renderer ������Ʈ �ҷ�����
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Line Renderer �Ӽ� ����
        lineRenderer.startWidth = 1.0f;
        lineRenderer.endWidth = 1.0f;
        lineRenderer.useWorldSpace = false;

        // Line Renderer�� �� ���� UI ������Ʈ ����
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);
    }
}
