using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNodeLine : MonoBehaviour
{
    public List<GameObject> target;//������ ������Ʈ 
    public LineRenderer lineRenderer; // Line Renderer ������Ʈ
    public string sortingLayerName = "MySortingLayer";
    public int sortingOrder = 0;
    private void Start()
    {
        if (target.Count <= 0)
        {
            return;
        }
        // Canvas�� �߰��� GameObject���� RectTransform ������Ʈ ��������
        Vector3 rootPos = gameObject.GetComponent<RectTransform>().position;
        List<Vector3> childrens = new List<Vector3>();
        for(int i=0;i< target.Count; i++)
        {
            childrens.Add(target[i].GetComponent<RectTransform>().position);
        }
        
        // LineRenderer �߰�
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = target.Count + 1;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        // RectTransform�� ����Ͽ� UI ��ǥ�迡�� ��ũ�� ��ǥ��� ��ȯ
        RectTransform canvasRectTransform = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        Vector2 screenrootPos = RectTransformUtility.WorldToScreenPoint(null, rootPos);
        List<Vector2> screenchildrens = new List<Vector2>();
        for(int i = 0; i < childrens.Count; i++)
        {
            screenchildrens.Add(RectTransformUtility.WorldToScreenPoint(null, childrens[i]));
        }
        
        // LineRenderer�� ��ǥ ����
        lineRenderer.SetPosition(0, screenrootPos);
        for(int i = 0; i < childrens.Count; i++)
        {
            lineRenderer.SetPosition(1 + i, screenchildrens[i]);
        }
        

        // LineRenderer ��Ƽ���� ����
        lineRenderer.material = new Material(Resources.Load<Material>("LineMaterial"));

    }

}
