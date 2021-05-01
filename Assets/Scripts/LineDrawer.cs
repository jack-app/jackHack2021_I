using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineDrawer : MonoBehaviour
{
    Vector3 initialMousePos;
    LineRenderer lineRenderer;

    [SerializeField]
    List<LineRenderer> amidaRendererList = new List<LineRenderer>();
    [SerializeField]
    List<GameObject> crossMarkers = new List<GameObject>();
    [SerializeField]
    GameObject newDrawedLine;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // マウスをクリックしたときの処理
        {
            initialMousePos = Input.mousePosition;
            initialMousePos = Camera.main.ScreenToWorldPoint(initialMousePos);
            transform.position = (Vector2)initialMousePos;
            lineRenderer.enabled = true;
        }
        if (Input.GetMouseButton(0)) // マウス押しっぱなしの処理
        {
            // 交点マーカーのリセット
            crossMarkers[0].SetActive(false);
            crossMarkers[1].SetActive(false);

            var mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos); // 画面座標からワールド座標へ変換
            var linePos = mousePos - initialMousePos; // 初期座標からマウス座標へのベクトル
            lineRenderer.SetPosition(1, linePos);

            float lineVecSign = Mathf.Sign(linePos.x); // 符号取得(線が左右のどちらに引かれてるか)

            // 引いた直線内にある最大2本のあみだラインを取得
            var nearLineList = amidaRendererList.Where(l => lineVecSign * l.transform.position.x >= lineVecSign * initialMousePos.x).
                                                Where(l => lineVecSign * l.transform.position.x <= lineVecSign * mousePos.x).
                                                OrderBy(l => Mathf.Abs(l.transform.position.x - initialMousePos.x)).Take(2);

            // すべての交点について交点マーカーを配置
            for (int i = 0; i < nearLineList.Count(); i++)
            {
                var l = nearLineList.ToList()[i];
                var amidaX = l.transform.position.x;
                var amidaMaxY = l.GetPosition(0).y;
                var amidaMinY = l.GetPosition(1).y;
                var crossY = linePos.y / linePos.x * (amidaX - initialMousePos.x) + initialMousePos.y; // 交点のy座標取得
                if(crossY >= amidaMinY && crossY <= amidaMaxY)
                {
                    crossMarkers[i].SetActive(true);
                    crossMarkers[i].transform.position = new Vector3(amidaX, crossY, -1);
                }
            }

        }
        else if (Input.GetMouseButtonUp(0)) // マウスを離したときの処理
        {
            lineRenderer.enabled = false; // 直線を削除
            if(crossMarkers[0].activeInHierarchy && crossMarkers[1].activeInHierarchy)
            {
                // 新しく引かれる線の描画
                GameObject newLine = Instantiate(newDrawedLine);
                newLine.transform.position = (Vector2)crossMarkers[0].transform.position;
                var l = newLine.GetComponent<LineRenderer>();
                l.SetPositions(new Vector3[2] { Vector3.zero, (Vector2)(crossMarkers[1].transform.position - crossMarkers[0].transform.position) });
            }

            // 交点マーカーのリセット
            crossMarkers[0].SetActive(false);
            crossMarkers[1].SetActive(false);
        }
    }
}
