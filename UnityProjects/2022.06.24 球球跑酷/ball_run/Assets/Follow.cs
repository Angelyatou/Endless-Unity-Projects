using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform t;//�����˷ŷ��ݵ���
    Vector3 o;//�����˷�����
    // Start is called before the first frame update
    void Start()
    {
        o = transform.position - t.position;//����Ϸ�����е�ʱ��ȷ���÷����ߵĳ��ȡ��������Ϣ
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = t.position + o;//��ÿʱÿ�̷��ݶ����˺ͷ�����������
                                            // �������ת
        float h = Input.GetAxis("Horizontal");
        float angle = 15 * h;
        Quaternion to = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, to, 0.05f);
    }
}
