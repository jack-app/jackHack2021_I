using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteCollider : MonoBehaviour, IPointerClickHandler
{
    public LineDrawer lineDrawer;

    private void Start()
    {
        var lineRenderer = GetComponent<LineRenderer>();
        var child = transform.GetChild(0);
        var collider = child.GetComponent<BoxCollider2D>();
        Vector2 startPos = lineRenderer.GetPosition(0);
        Vector2 endPos = lineRenderer.GetPosition(1);

        Vector2 lineVec = endPos - startPos;
        float theta = Mathf.Atan2(lineVec.y, lineVec.x);

        child.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * theta);

        collider.offset = new Vector2(lineVec.magnitude / 2, 0f);
        collider.size = new Vector2(lineVec.magnitude, 0.17f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        lineDrawer.DeleteNewLine(GetComponent<LineRenderer>());
    }
}
