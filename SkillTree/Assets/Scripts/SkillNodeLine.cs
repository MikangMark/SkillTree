using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNodeLine : MonoBehaviour
{
    public List<GameObject> target;//연결할 오브젝트 
    public LineRenderer lineRenderer; // Line Renderer 컴포넌트
    public string sortingLayerName = "MySortingLayer";
    public int sortingOrder = 0;
    private void Start()
    {
        if (target.Count <= 0)
        {
            return;
        }
        // Canvas에 추가된 GameObject에서 RectTransform 컴포넌트 가져오기
        Vector3 rootPos = gameObject.GetComponent<RectTransform>().position;
        List<Vector3> childrens = new List<Vector3>();
        for(int i=0;i< target.Count; i++)
        {
            childrens.Add(target[i].GetComponent<RectTransform>().position);
        }
        
        // LineRenderer 추가
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = target.Count + 1;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        // RectTransform을 사용하여 UI 좌표계에서 스크린 좌표계로 변환
        RectTransform canvasRectTransform = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 screenrootPos = RectTransformUtility.WorldToScreenPoint(null, rootPos);
        List<Vector2> screenchildrens = new List<Vector2>();
        for(int i = 0; i < childrens.Count; i++)
        {
            screenchildrens.Add(RectTransformUtility.WorldToScreenPoint(null, childrens[i]));
        }
        
        // LineRenderer에 좌표 설정
        lineRenderer.SetPosition(0, screenrootPos);
        for(int i = 0; i < childrens.Count; i++)
        {
            lineRenderer.SetPosition(1 + i, screenchildrens[i]);
        }
        

        // LineRenderer 머티리얼 설정
        lineRenderer.material = new Material(Resources.Load<Material>("LineMaterial"));

    }

}
