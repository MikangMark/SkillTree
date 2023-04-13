using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNodeLine : MonoBehaviour
{
    public GameObject target;//연결할 오브젝트 
    public LineRenderer lineRenderer; // Line Renderer 컴포넌트

    void Start()
    {
        // Line Renderer 컴포넌트 불러오기
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Line Renderer 속성 설정
        lineRenderer.startWidth = 1.0f;
        lineRenderer.endWidth = 1.0f;
        lineRenderer.useWorldSpace = false;

        // Line Renderer에 두 개의 UI 오브젝트 연결
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);
    }
}
