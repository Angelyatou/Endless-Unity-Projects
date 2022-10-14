using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cha3 : MonoBehaviour
{
    Animator animator;
    CharacterController cc;

    public float speed = 3;
    Vector3 move;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
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
        move = new Vector3(x, 0, z);
        //��һ֡�ƶ�����������С
        Vector3 m = move * speed * Time.deltaTime;

        //�����ƶ�����
        transform.LookAt(transform.position + m);

        //ͨ��ccȥ�ƶ�
        cc.Move(m);
    }

    void UpdateAnim()
    {
        //���ڸ����ٶȲ��Ŷ���
        animator.SetFloat("Forward",cc.velocity.magnitude / speed);
    }
}

