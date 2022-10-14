using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cha3Fall : MonoBehaviour
{
    Animator animator;
    CharacterController cc;

    public float speed = 3;
    [Range(0.0f, 1.0f)]
    public float testSpeed = 1;


    Vector3 move;

    bool isGround = true;
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

    //�����ٶ�
    float vy = 0;
    void Move(float x, float y, float z)
    {
        move = new Vector3(x, 0, z);
        //��һ֡�ƶ�����������С
        Vector3 m = move * speed * Time.deltaTime;

        if (isGround)
        {
            vy = 0;
        }
        else
        {
            //����ʽ��v = v0 + gt   (v0=0)
            vy += Physics.gravity.y * Time.deltaTime;
        }

        //��y = v * ��t
        m.y = vy * Time.deltaTime;

        //�����ƶ�����
        transform.LookAt(transform.position + move);

        //ͨ��ccȥ�ƶ�
        cc.Move(m);
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.2f, 0), Vector3.down);

        RaycastHit hit;
        Color c = Color.red;

        isGround = false;
        if(Physics.Raycast(ray,out hit, 0.35f))
        {
            c = Color.green;
            isGround = true;
        }
        //��ʽ
        Debug.DrawLine(transform.position + new Vector3(0, 0.2f, 0),transform.position - new Vector3(0,0.15f,0),c);
    }
    void UpdateAnim()
    {
        //���ڸ����ٶȲ��Ŷ���
        animator.SetFloat("Forward", cc.velocity.magnitude / speed * testSpeed);
    }
}

