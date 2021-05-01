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
        if (Input.GetMouseButtonDown(0)) // �}�E�X���N���b�N�����Ƃ��̏���
        {
            initialMousePos = Input.mousePosition;
            initialMousePos = Camera.main.ScreenToWorldPoint(initialMousePos);
            transform.position = (Vector2)initialMousePos;
            lineRenderer.enabled = true;
        }
        if (Input.GetMouseButton(0)) // �}�E�X�������ςȂ��̏���
        {
            // ��_�}�[�J�[�̃��Z�b�g
            crossMarkers[0].SetActive(false);
            crossMarkers[1].SetActive(false);

            var mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos); // ��ʍ��W���烏�[���h���W�֕ϊ�
            var linePos = mousePos - initialMousePos; // �������W����}�E�X���W�ւ̃x�N�g��
            lineRenderer.SetPosition(1, linePos);

            float lineVecSign = Mathf.Sign(linePos.x); // �����擾(�������E�̂ǂ���Ɉ�����Ă邩)

            // �������������ɂ���ő�2�{�̂��݂����C�����擾
            var nearLineList = amidaRendererList.Where(l => lineVecSign * l.transform.position.x >= lineVecSign * initialMousePos.x).
                                                Where(l => lineVecSign * l.transform.position.x <= lineVecSign * mousePos.x).
                                                OrderBy(l => Mathf.Abs(l.transform.position.x - initialMousePos.x)).Take(2);

            // ���ׂĂ̌�_�ɂ��Č�_�}�[�J�[��z�u
            for (int i = 0; i < nearLineList.Count(); i++)
            {
                var l = nearLineList.ToList()[i];
                var amidaX = l.transform.position.x;
                var amidaMaxY = l.GetPosition(0).y;
                var amidaMinY = l.GetPosition(1).y;
                var crossY = linePos.y / linePos.x * (amidaX - initialMousePos.x) + initialMousePos.y; // ��_��y���W�擾
                if(crossY >= amidaMinY && crossY <= amidaMaxY)
                {
                    crossMarkers[i].SetActive(true);
                    crossMarkers[i].transform.position = new Vector3(amidaX, crossY, -1);
                }
            }

        }
        else if (Input.GetMouseButtonUp(0)) // �}�E�X�𗣂����Ƃ��̏���
        {
            lineRenderer.enabled = false; // �������폜
            if(crossMarkers[0].activeInHierarchy && crossMarkers[1].activeInHierarchy)
            {
                // �V�������������̕`��
                GameObject newLine = Instantiate(newDrawedLine);
                newLine.transform.position = (Vector2)crossMarkers[0].transform.position;
                var l = newLine.GetComponent<LineRenderer>();
                l.SetPositions(new Vector3[2] { Vector3.zero, (Vector2)(crossMarkers[1].transform.position - crossMarkers[0].transform.position) });
            }

            // ��_�}�[�J�[�̃��Z�b�g
            crossMarkers[0].SetActive(false);
            crossMarkers[1].SetActive(false);
        }
    }
}
