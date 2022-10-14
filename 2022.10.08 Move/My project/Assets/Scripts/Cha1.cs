using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cha1 : MonoBehaviour
{
    Animator animator;

    public float speed = 3;
    Vector3 move;

    private void Start()
    {
        animator = GetComponent<Animator>();
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

    void Move(float x,float y,float z)
    {
        //��������
        move = new Vector3(x, y, z);
        //Ҫ�����λ��
        Vector3 to = transform.position + move;
        transform.LookAt(to);
        transform.position += move * speed * Time.deltaTime;
    }

    void UpdateAnim()
    {
        float forward = move.magnitude;
        animator.SetFloat("Forward", forward);
    }
}
