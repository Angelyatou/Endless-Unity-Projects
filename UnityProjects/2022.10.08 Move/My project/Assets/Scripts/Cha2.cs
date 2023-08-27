using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cha2 : MonoBehaviour
{
    Animator animator;
    Rigidbody rigid;

    public float speed = 3;
    Vector3 move;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //��ȡ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, 0, v);
        //���¶���
        UpdateAnim();
    }

    void Move(float x, float y, float z)
    {
        //��������
        move = new Vector3(x, y, z);
    }
    //�����ƶ���Ҫ��FixedUpdate��д
    public void FixedUpdate()
    {
        //����move��ֱ�Ӹı�����ٶ�
        Vector3 v = move * speed;
        v.y = rigid.velocity.y;
        rigid.velocity = new Vector3(move.x,rigid.velocity.y,move.z)* speed;

        //�ø��峯��Ŀ��
        Quaternion q = Quaternion.LookRotation(move);  //���� ת�� ����
        rigid.MoveRotation(q);
    }

    void UpdateAnim()
    {
        float forward = move.magnitude;
        animator.SetFloat("Forward", forward);
    }
}

